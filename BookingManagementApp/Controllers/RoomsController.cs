using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Room;
using BookingManagementApp.DTOs.University;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsRepository _roomRepository;
        private readonly IBookingsRepository _bookingRepository;
        private readonly IEmployeesRepository _employeesRepository;

        public RoomsController(IRoomsRepository roomRepository, IBookingsRepository bookingsRepository, IEmployeesRepository employeesRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingsRepository;
            _employeesRepository = employeesRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = result.Select(x => (RoomDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseOKHandler<RoomDto>((RoomDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateRoomDto createRoomDto)
        {
            try
            {
                var result = _roomRepository.Create(createRoomDto);

                return Ok(new ResponseOKHandler<RoomDto>((RoomDto)result));
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
        public IActionResult Update(RoomDto roomDto)
        {
            try
            {
                var entity = _roomRepository.GetByGuid(roomDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                Rooms toUpdate = roomDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                var result = _roomRepository.Update(toUpdate);

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
                var entity = _roomRepository.GetByGuid(guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                var result = _roomRepository.Delete(entity);

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

        [HttpGet("RoomDetails")]
        public IActionResult RoomDetail()
        {
            //Memperoleh hasil data dengan method GETAll
            var employees = _employeesRepository.GetAll();
            var bookings = _bookingRepository.GetAll();
            var rooms = _roomRepository.GetAll();

            // Pengondisian apabila data GETAll tidak ditemukan
            if (!(employees.Any() && bookings.Any() && rooms.Any()))
            {
                //Apabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            // Menampilkan data baru yang diambil dari tabel employees, bookings dan rooms
            var roomDetails = from emp in employees
                              join bk in bookings on emp.Guid equals bk.EmployeeGuid
                              join rm in rooms on bk.RoomGuid equals rm.Guid
                              select new RoomDetailDto
                              {
                                  BookingGuid = bk.Guid,
                                  RoomName = rm.Name,
                                  Status = bk.Status,
                                  Floor = rm.Floor,
                                  BookedBy = string.Concat(emp.FirstName + " " + emp.LastName)
                              };

            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<IEnumerable<RoomDetailDto>>(roomDetails));
        }

        [HttpGet("AvailableRoomsToday")]
        public IActionResult GetAvailableRoomsToday()
        {
            // Dapatkan daftar semua kamar
            var allRooms = _roomRepository.GetAll();

            // Dapatkan daftar semua pemesanan pada hari ini
            var today = DateTime.Today;
            var bookingsToday = _bookingRepository.GetBookingsForDate(today);

            // Temukan kamar yang tidak memiliki pemesanan pada hari ini
            var availableRooms = allRooms.Where(room =>
                !bookingsToday.Any(booking => booking.RoomGuid == room.Guid)
            ).Select(room => new RoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity
            });

            //Apabila Data Ditemukan, akan dikembalikan ke user dalam bentuk JSON API
            return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>("Available rooms today retrieved successfully")
            {
                Data = availableRooms
            });
        }
    }
}
