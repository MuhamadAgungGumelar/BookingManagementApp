using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Account;
using BookingManagementApp.DTOs.Booking;
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
    public class AccountsController : ControllerBase
    {
        //membuat variable dengan cara injeksi
        private readonly IAccountsRepository _accountRepository;

        //membuat constructor
        public AccountsController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
