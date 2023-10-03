using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using BookingManagementApp.DTOs.University;
using BookingManagementApp.DTOs.Employee;
using BookingManagementApp.Utilities.Handlers;
using System.Net;

namespace BookingManagementApp.Controllers
{
    //Membuat API Controller menggunakan Framework AspNetCore serta membuat rute atau url API 
    [ApiController]
    [Route("api/[controller]")]
    public class UniversitiesController : ControllerBase
    {
        //membuat variable dengan cara injeksi
        private readonly IUniversitiesRepository _universitiesRepository;

        //membuat constructor
        public UniversitiesController(IUniversitiesRepository universitiesRepository)
        {
            _universitiesRepository = universitiesRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Memperoleh hasil data dengan method GETAll
            var result = _universitiesRepository.GetAll();
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
            var data = result.Select(x => (UniversityDto)x);

            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<IEnumerable<UniversityDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            //Memperoleh hasil data dengan method GETById
            var result = _universitiesRepository.GetByGuid(guid);

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
            return Ok(new ResponseOKHandler<UniversityDto>((UniversityDto) result));
        }

        [HttpPost]
        public IActionResult Create(CreateUniversityDto universityDto)
        {
            try //Kondisi Try apabila Data Berhasil Ditambahkan
            {
                //Menambah hasil data dengan method POST
                var result = _universitiesRepository.Create(universityDto);

                //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
                return Ok(new ResponseOKHandler<UniversityDto>((UniversityDto)result));
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
        public IActionResult Update(UniversityDto universityDto)
        {
            try //Kondisi Try apabila Data Berhasil Diubah
            {
                //Menyeleksi data yang akan diubah berdasarkan GuId
                var entity = _universitiesRepository.GetByGuid(universityDto.Guid);
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
                //Mengubah data CreatedDate yang sudah ada sebelumnya
                Universities toUpdate = universityDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                //Mengubah data dalam database dengan method PUT
                var result = _universitiesRepository.Update(toUpdate);

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
                var entity = _universitiesRepository.GetByGuid(guid);
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
                var result = _universitiesRepository.Delete(entity);

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
