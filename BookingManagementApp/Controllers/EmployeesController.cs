using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Employee;
using BookingManagementApp.DTOs.Role;
using BookingManagementApp.DTOs.Room;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookingManagementApp.Controllers
{
    //Membuat API Controller menggunakan Framework AspNetCore serta membuat rute atau url API 
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        //membuat variable dengan cara injeksi
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IEducationsRepository _educationsRepository;
        private readonly IUniversitiesRepository _universitiesRepository;

        //membuat constructor
        public EmployeesController(IEmployeesRepository employeeRepository, IEducationsRepository educationsRepository, IUniversitiesRepository universitiesRepository)
        {
            _employeeRepository = employeeRepository;
            _educationsRepository = educationsRepository;
            _universitiesRepository = universitiesRepository;
        }

        [HttpGet("details")]
        public IActionResult GetDetails() 
        {
            var employees = _employeeRepository.GetAll();
            var educations = _educationsRepository.GetAll();
            var universities = _universitiesRepository.GetAll();

            if (!(employees.Any() && educations.Any() && universities.Any()))
            {
                //Apabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var employeesDetails = from emp in employees
                                   join edu in educations on emp.Guid equals edu.Guid
                                   join uni in universities on edu.UniversityGuid equals uni.Guid
                                   select new EmployeeDetailDto
                                   {
                                       Guid = emp.Guid,
                                       Nik = emp.Nik,
                                       FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                       BirthDate = emp.BirthDate,
                                       Gender = emp.Gender,
                                       HiringDate = emp.HiringDate,
                                       Email = emp.Email,
                                       Phone = emp.PhoneNumber,
                                       Major = edu.Major,
                                       Degree = edu.Degree,
                                       Gpa = edu.Gpa,
                                       University = uni.Name
                                   };

            return Ok(new ResponseOKHandler<IEnumerable<EmployeeDetailDto>>(employeesDetails));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Memperoleh hasil data dengan method GETAll
            var result = _employeeRepository.GetAll();
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
            var data = result.Select(x => (EmployeeDto) x);

            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            //Memperoleh hasil data dengan method GETById
            var result = _employeeRepository.GetByGuid(guid);

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
            return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto) result));
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            try //Kondisi Try apabila Data Berhasil Ditambahkan
            {
                //Menambahkan data NIK dengan class GenerateHandler
                Employees toCreate = createEmployeeDto;
                toCreate.Nik = GenerateHandler.NIK(_employeeRepository.GetLastNik());

                //Menambah hasil data dengan method POST
                var result = _employeeRepository.Create(toCreate);
                //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
                return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
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
        public IActionResult Update(EmployeeDto employeeDto)
        {
            try //Kondisi Try apabila Data Berhasil Diubah
            {
                //Menyeleksi data yang akan diubah berdasarkan GuId
                var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
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
                //Mengubah data CreatedDate dan NIK yang sudah ada sebelumnya
                Employees toUpdate = employeeDto;
                toUpdate.CreatedDate = entity.CreatedDate;
                toUpdate.Nik = entity.Nik;

                //Mengubah data dalam database dengan method PUT
                var result = _employeeRepository.Update(toUpdate);

                //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (ExceptionHandler ex) //Kondisi Exception apabila data gagal untuk diubah
            {
                //Aoabila data gagal diubah, akan mengembalikan pesan gagal mengubah data
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try //Kondisi Try apabila Data Berhasil Dihapus
            {
                //Menyeleksi data yang akan dihapus berdasarkan GuId
                var entity = _employeeRepository.GetByGuid(guid);
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
                var result = _employeeRepository.Delete(entity);

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
