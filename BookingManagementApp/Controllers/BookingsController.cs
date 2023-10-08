using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Account;
using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.DTOs.Education;
using BookingManagementApp.DTOs.Room;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsRepository _bookingRepository;
        private readonly IRoomsRepository _roomsRepository;
        private readonly IEmployeesRepository _employeesRepository;

        public BookingsController(IBookingsRepository bookingRepository, IRoomsRepository roomsRepository, IEmployeesRepository employeesRepository)
        {
            _bookingRepository = bookingRepository;
            _roomsRepository = roomsRepository;
            _employeesRepository = employeesRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = result.Select(x => (BookingDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<BookingDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseOKHandler<BookingDto>((BookingDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateBookingDto createBookingDto) 
        {
            try
            {
                var result = _bookingRepository.Create(createBookingDto);

                return Ok(new ResponseOKHandler<BookingDto>((BookingDto) result));
            }
            catch (ExceptionHandler ex)
            {
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
        public IActionResult Update(BookingDto bookingDto)
        {
            try
            {
                var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                Bookings toUpdate = bookingDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                var result = _bookingRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (ExceptionHandler ex)
            {
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
            try
            {
                var entity = _bookingRepository.GetByGuid(guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                var result = _bookingRepository.Delete(entity);

                return Ok(new ResponseOKHandler<string>("Data Deleted"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("BookingDetailsCollection")]
        public IActionResult BookingDetailsCollection()
        {
            //Memperoleh hasil data dengan method GETAll
            var employees = _employeesRepository.GetAll();
            var bookings = _bookingRepository.GetAll();
            var rooms = _roomsRepository.GetAll();

            // Menampilkan data baru yang diambil dari tabel employees, bookings dan rooms
            var bookingDetails = from emp in employees
                                 join bk in bookings on emp.Guid equals bk.EmployeeGuid
                                 join rm in rooms on bk.RoomGuid equals rm.Guid
                                 select new BookingDetailDto
                                 {
                                     Guid = bk.Guid,
                                     NIK = emp.Nik,
                                     BookedBy = $"{emp.FirstName} {emp.LastName}",
                                     RoomName = rm.Name,
                                     StartDate = bk.StartDate,
                                     EndDate = bk.EndDate,
                                     Status = bk.Status,
                                     Remarks = bk.Remarks
                                 };

            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<IEnumerable<BookingDetailDto>>("Booking details collection retrieved successfully")
            {
                Data = bookingDetails
            });
        }

        [HttpGet("BookingDetailsByGuid/{guid}")]
        public IActionResult BookingDetailsByGuid(Guid guid)
        {
            //Memperoleh hasil data dengan method GETAll
            var employees = _employeesRepository.GetAll();
            var bookings = _bookingRepository.GetAll();
            var rooms = _roomsRepository.GetAll();

            // Menampilkan data baru yang diambil dari tabel employees, bookings dan rooms
            var bookingDetail = (from emp in employees
                                 join bk in bookings on emp.Guid equals bk.EmployeeGuid
                                 join rm in rooms on bk.RoomGuid equals rm.Guid
                                 where bk.Guid == guid
                                 select new BookingDetailDto
                                 {
                                     Guid = bk.Guid,
                                     NIK = emp.Nik,
                                     BookedBy = $"{emp.FirstName} {emp.LastName}",
                                     RoomName = rm.Name,
                                     StartDate = bk.StartDate,
                                     EndDate = bk.EndDate,
                                     Status = bk.Status,
                                     Remarks = bk.Remarks
                                 }).FirstOrDefault();

            //Apabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
            if (bookingDetail == null)
            {
                // Apabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Booking not found"
                });
            }

            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<BookingDetailDto>("Booking details retrieved successfully")
            {
                Data = bookingDetail
            });
        }

        [HttpGet("BookingLength")]
        public IActionResult BookingLength()
        {
            try
            {
                // Menghitung data jumlah hari yang digunakan untuk bookings
                var bookingLengths = _bookingRepository.GetBookingLengthOnWeekdays();

                // Jika data jumlah hari booking tidak ditemukan, akan menampilkan pesan data tidak ditemukan
                if (!bookingLengths.Any())
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
                return Ok(new ResponseOKHandler<IEnumerable<BookingLengthDto>>(bookingLengths));
            }
            catch (ExceptionHandler ex)
            {   //Apabila Server gagal untuk mengambil data jumlah hari booking, akan menampilkan pesan gagal mengambil data
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to retrieve booking lengths",
                    Error = ex.Message
                });
            }
        }
    }
}
