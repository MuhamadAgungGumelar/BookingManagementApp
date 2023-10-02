﻿using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversitiesRepository _universitiesRepository;

        public UniversitiesController(IUniversitiesRepository universitiesRepository)
        {
            _universitiesRepository = universitiesRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universitiesRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _universitiesRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Universities universities)
        {
            var result = _universitiesRepository.Create(universities);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Universities universities)
        {
            var result = _universitiesRepository.Update(universities);
            if (result is false)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var universities = _universitiesRepository.GetByGuid(guid);

            if (universities is null)
            {
                return NotFound("Data Not Found");
            }

            _universitiesRepository.Delete(guid);

            return Ok("Data deleted successfully");
        }
    }
}
