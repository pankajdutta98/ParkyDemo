using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyWebAPI.Models;
using ParkyWebAPI.Models.DTOs;
using ParkyWebAPI.Repository.IRepository;
using System.Collections.Generic;

namespace ParkyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        private INationalParkRepository _npRepo;

        public NationalParksController(INationalParkRepository npRepo)
        {
            _npRepo = npRepo;
        }

        [HttpGet]
        public IActionResult getNationalParks()
        {
            List<NationalParkDTOs> npDtoList = new List<NationalParkDTOs>();
            List<NationalPark> objList = _npRepo.GetNationalParks().ToList();
            if (objList != null)
            {
                foreach (NationalPark obj in objList)
                {
                    NationalParkDTOs npDto = new NationalParkDTOs();
                    npDto.Id = obj.Id;
                    npDto.Name = obj.Name;
                    npDto.State = obj.State;
                    npDto.Created = obj.Created ?? DateTime.Now;
                    npDto.Established = obj.Established ?? DateTime.Now;
                    npDtoList.Add(npDto);
                }
            }
            return Ok(npDtoList);
        }

        [HttpGet("{npId:int}")]
        [Authorize]
        public IActionResult getNationalPark(int npId)
        {
            NationalPark np = new NationalPark();
            NationalParkDTOs npDto = new NationalParkDTOs();

            np = _npRepo.GetNationalPark(npId);
            if (np != null)
            {
                npDto.Id = np.Id;
                npDto.Name = np.Name;
                npDto.State = np.State;
                npDto.Created = np.Created ?? DateTime.Now;
                npDto.Established = np.Established ?? DateTime.Now;
                return Ok(npDto);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult AddNationalPark([FromBody]NationalParkDTOs npDto)
        {
            if(npDto == null)
            {
                return BadRequest(ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_npRepo.NationalParkExists(npDto.Name))
            {
                ModelState.AddModelError("", "National Park already Exist!");
                return StatusCode(404, ModelState);
            }

            NationalPark np = new NationalPark();

            //np.Id = np.Id;
            np.Name = npDto.Name;
            np.State = npDto.State;
            np.Created = npDto.Created;
            np.Established = npDto.Established;

            _npRepo.CreateNationalPark(np);
            return Ok();
        }
    }
}
