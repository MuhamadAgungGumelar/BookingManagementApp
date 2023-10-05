using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Account;
using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.DTOs.Employee;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using BookingManagementApp.Utilities.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookingManagementApp.Controllers
{
    //Membuat API Controller menggunakan Framework AspNetCore serta membuat rute atau url API 
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        //membuat variable dengan cara injeksi
        private readonly IAccountsRepository _accountRepository;
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IUniversitiesRepository _universitiesRepository;
        private readonly IEducationsRepository _educationsRepository;
        private readonly IEmailHandler _emailHandler;

        //membuat constructor
        public AccountsController(IAccountsRepository accountRepository, IEmployeesRepository employeeRepository, IUniversitiesRepository universitiesRepository, IEducationsRepository educationsRepository, IEmailHandler emailHandler)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _universitiesRepository = universitiesRepository;
            _educationsRepository = educationsRepository;
            _emailHandler = emailHandler;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Memperoleh hasil data dengan method GETAll
            var result = _accountRepository.GetAll();
            if (!result.Any()) // Pengondisian apabila data GETAll tidak ditemukan
            {
                //Apabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //memilih data dari DTO untuk ditampilkan ke user, menerapkan casting DTO secara implisit/explisit
            var data = result.Select(x => (AccountDto)x);

            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<IEnumerable<AccountDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            //Memperoleh hasil data dengan method GETById
            var result = _accountRepository.GetByGuid(guid);
             
            if (result is null) // Pengondisian apabila data GETById tidak ditemukan
            {
                //Aoabila data gagal ditemukan berdasarkan Id, akan menampilkan pesan data tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            try //Kondisi Try apabila Data Berhasil Ditambahkan
            {
                //Menambahkan data Password dengan class HasingHandler
                createAccountDto.Password = HasingHandler.HashPassword(createAccountDto.Password);
                //Menambah hasil data dengan method POST
                var result = _accountRepository.Create(createAccountDto);
                //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
                return Ok(new ResponseOKHandler<AccountDto>((AccountDto)result));
            }
            catch (ExceptionHandler ex) //Kondisi Exception apabila data gagal untuk ditambahkan
            {
                //Aoabila data gagal ditambahkan, akan mengembalikan pesan gagal membuat data
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            try //Kondisi Try apabila Data Berhasil Diubah
            {
                //Menyeleksi data yang akan diubah berdasarkan GuId
                var entity = _accountRepository.GetByGuid(accountDto.Guid);
                if (entity is null) // Pengondisian apabila data tidak ditemukan
                {
                    //Aoabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                //Mengubah data CreatedDate dan Password yang sudah ada sebelumnya
                Accounts toUpdate = accountDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                toUpdate.Password = HasingHandler.HashPassword(accountDto.Password);

                //Mengubah data dalam database dengan method PUT
                var result = _accountRepository.Update(toUpdate);
                //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (ExceptionHandler ex) //Kondisi Exception apabila data gagal untuk diubah
            {
                //Apabila data gagal diubah, akan mengembalikan pesan gagal mengubah data
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("ForgotPassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                // Mendapatkan Data Berdasarkan Email
                var employeesEmail = _accountRepository.GetByEmail(email);
                if (employeesEmail is null)
                {
                    //Aoabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Email Not Found"
                    });
                }

                // Generate OTP (6-digit nomer random)
                Random random = new Random();
                int otp = random.Next(100000, 999999);

                // Set OTP, IsUsed, dan ExpiredTime properties
                employeesEmail.OTP = otp;
                employeesEmail.IsUsed = false;
                employeesEmail.ExpiredTime = DateTime.Now.AddMinutes(5); // OTP kadaluarsa dalam 5 menit

                // Update database dengan OTP baru
                var result = _accountRepository.Update(employeesEmail);

                // Mengirimkan OTP baru ke email
                _emailHandler.Send("Forgot Password", $"Your OTP is {otp}", email);

                return Ok(new ResponseOKHandler<int>("New OTP has been Send to you email") { Data = otp });
            }
            catch (ExceptionHandler ex)  //Kondisi Exception apabila otp gagal dikirim
            {
                //Apabila otp gagal dikirim, akan mengembalikan pesan otp gagal dikirim
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create new OTP",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                // Langkah pertama: Dapatkan akun berdasarkan email
                var account = _accountRepository.GetByEmail(changePasswordDto.Email);
                    if (account is null)
                    {
                        return NotFound(new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status404NotFound,
                            Status = HttpStatusCode.NotFound.ToString(),
                            Message = "Account Not Found"
                        });
                    }

                    // Periksa apakah OTP sudah digunakan atau kadaluarsa
                    if (account.IsUsed || account.ExpiredTime <= DateTime.Now)
                    {
                        return BadRequest(new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "OTP is invalid or expired"
                        });
                    }

                    // Periksa apakah OTP yang dimasukkan sesuai
                    if (account.OTP != changePasswordDto.OTP)
                    {
                        return BadRequest(new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "Invalid OTP"
                        });
                    }

                    // Periksa apakah NewPassword dan ConfirmPassword sesuai
                    if (changePasswordDto.Password != changePasswordDto.ConfirmPassword)
                    {
                        return BadRequest(new ResponseErrorHandler
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "NewPassword and ConfirmPassword do not match"
                        });
                    }

                    // Set password baru setelah melewati semua verifikasi
                    account.Password = HasingHandler.HashPassword(changePasswordDto.Password);
                    account.IsUsed = true; // Menandai OTP sebagai digunakan
                    _accountRepository.Update(account);

                    return Ok(new ResponseOKHandler<string>("Password changed successfully"));
            }
            catch (ExceptionHandler ex) //Kondisi Exception apabila password gagal diubah
            {
                //Apabila password gagal diubah, akan mengembalikan pesan password gagal diubah
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to change password",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("DetailAccount")]
        public IActionResult DetailAccount()
        {
            var employees = _employeeRepository.GetAll();
            var educations = _educationsRepository.GetAll();
            var universities = _universitiesRepository.GetAll();
            var accounts = _accountRepository.GetAll();

            if (!(employees.Any() && educations.Any() && universities.Any() && accounts.Any()))
            {
                //Apabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var registerDetails = from emp in employees
                                  join edu in educations on emp.Guid equals edu.Guid
                                  join unv in universities on edu.UniversityGuid equals unv.Guid
                                  join acc in accounts on emp.Guid equals acc.Guid
                                  select new RegisterAccountDto
                                  {
                                      FirstName = emp.FirstName,
                                      LastName = emp.LastName,
                                      BirthDate = emp.BirthDate,
                                      Gender = emp.Gender,
                                      HiringDate = emp.HiringDate,
                                      Email = emp.Email,
                                      PhoneNumber = emp.PhoneNumber,
                                      Major = edu.Major,
                                      Degree = edu.Degree,
                                      Gpa = edu.Gpa,
                                      Code = unv.Code,
                                      Name = unv.Name,
                                      Password = acc.Password,
                                      ConfirmPassword = acc.Password
                                  };

            return Ok(new ResponseOKHandler<IEnumerable<RegisterAccountDto>>(registerDetails));
        }

        [HttpPost("Register")]
        public IActionResult Register (RegisterAccountDto registerAccountDto)
        {
            try 
            {
                // Memperoleh data berdasarkan Nama Universitas
                var universityName = _accountRepository.GetByUniversityName(registerAccountDto.Name);
                // Instansiasi ke Object Universitas
                var universityCreate = new Universities();

                // Apabila Object Universitas Kosong, Maka Buat Object Baru
                if (universityName is null) 
                {
                    universityCreate.Guid = Guid.NewGuid();
                    universityCreate.Code = registerAccountDto.Code;
                    universityCreate.Name = registerAccountDto.Name;
                    universityCreate.CreatedDate = DateTime.Now;
                    universityCreate.ModifiedDate = DateTime.Now;

                    // Mengirimkan Object University Baru Ke Database
                    var universityResult = _universitiesRepository.Create(universityCreate);
                }
                else // Apabila Object Universitas Ada, Maka Gunakan Object Yang Sudah Ada
                {
                    universityCreate = universityName;
                    universityCreate.Code = registerAccountDto.Code;
                    universityCreate.Name = registerAccountDto.Name;
                }

                // Membuat Object Employee Baru
                var newEmployee = new Employees
                {
                    Guid = Guid.NewGuid(),
                    Nik = GenerateHandler.NIK(_employeeRepository.GetLastNik()),
                    FirstName = registerAccountDto.FirstName,
                    LastName = registerAccountDto.LastName,
                    BirthDate = registerAccountDto.BirthDate,
                    Gender = registerAccountDto.Gender,
                    HiringDate = registerAccountDto.HiringDate,
                    Email = registerAccountDto.Email,
                    PhoneNumber = registerAccountDto.PhoneNumber,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                // Mengirimkan Object Employee Baru Ke Database
                var newEmployeeResult = _employeeRepository.Create(newEmployee);

                // Buat objek Educations baru
                var newEducation = new Educations
                {
                        Guid = newEmployee.Guid,
                        Major = registerAccountDto.Major,
                        Degree = registerAccountDto.Degree,
                        Gpa = registerAccountDto.Gpa,
                        UniversityGuid = universityName.Guid,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                };

                // Mengirimkan Object Educations Baru Ke Database
                var newEducatonsResult = _educationsRepository.Create(newEducation);

                // Jika Object Employee Tidak Kosong, Maka Buat Object Account Baru
                if (newEmployeeResult is not null)
                {
                    // Hashing Password Yang Di Inputkan
                    var newPassword = HasingHandler.HashPassword(registerAccountDto.Password);

                    // Membuat Object Accounts Baru
                    var newAccount = new Accounts
                    {
                        Guid = newEmployee.Guid,
                        Password = newPassword,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    // Mengirimkan Object Accounts Baru Ke Database
                    var newAccountResult = _accountRepository.Create(newAccount);
                }

                // Jika semua operasi berhasil, mengirimkan respons berhasil ke klien
                return Ok(new ResponseOKHandler<string>("Registration successfull"));

            }
            catch (ExceptionHandler ex)
            {
                //Aoabila register gagal ditambahkan, akan mengembalikan pesan register gagal dibuat
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to register",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginAccountDto loginAccountDto)
        {
            try
            {
                // Mendapatkan Data Account Berdasarkan Email
                var loginAccount = _accountRepository.GetByEmail(loginAccountDto.Email);
                //Apabila data Email gagal ditemukan, akan menampilkan pesan data Email tidak ditemukan
                if (loginAccount == null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Account Email Not Found"
                    });
                }

                // Melakukan validasi berdasarkan password
                if (!HasingHandler.VerifyPassword(loginAccountDto.Password, loginAccount.Password))
                {
                    return BadRequest(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Status = HttpStatusCode.BadRequest.ToString(),
                        Message = "Account or Password Invalid"
                    });
                }

                // Jika login sukses, Akan mengirimkan respons berhasil ke klien
                return Ok(new ResponseOKHandler<string>("Login successful"));
            }
            catch (ExceptionHandler ex)
            {
                //Apabila proses login gagal, akan mengembalikan pesan gagal login
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to login",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try  //Kondisi Try apabila Data Berhasil Dihapus
            {
                //Menyeleksi data yang akan dihapus berdasarkan GuId
                var entity = _accountRepository.GetByGuid(guid);
                if (entity is null) // Pengondisian apabila data tidak ditemukan
                {
                    //Aoabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }
                //Menghapus data dalam database dengan method DELETE
                var result = _accountRepository.Delete(entity);
                //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
                return Ok(new ResponseOKHandler<string>("Data Deleted"));
            }
            catch (ExceptionHandler ex) //Kondisi Exception apabila data gagal untuk dihapus
            {
                //Aoabila data gagal dihapus, akan mengembalikan pesan gagal menghapus data
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }
    }
}
