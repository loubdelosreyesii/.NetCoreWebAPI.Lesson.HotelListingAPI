using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.Models.Hotel;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Core.Models;

namespace HotelListing.API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _hotelsRepository;

        //initialize the database
        public HotelsController(IMapper mapper, IHotelsRepository hotelsRepository)
        {
            this._mapper = mapper;
            this._hotelsRepository = hotelsRepository;
        }

        // GET: api/Hotels
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync();
            var records = _mapper.Map<List<GetHotelDto>>(hotels);

            if (records == null)
            {
                return NotFound();
            }

            return Ok(records);
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetHotelDto>>> GetPagedHotels([FromQuery] QueryParameters queryParamaters)
        {
            var pagedHotelsResult  = await  _hotelsRepository.GetAllAsync<GetHotelDto>(queryParamaters);
            return Ok(pagedHotelsResult);
        }


        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return Ok(hotelDto);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto updateHotelDto)
        {
            if (id != updateHotelDto.Id)
            {
                return BadRequest();
            }

            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            _mapper.Map(updateHotelDto, hotel);

            try
            {
                await _hotelsRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotelDto)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDto);

            await _hotelsRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
