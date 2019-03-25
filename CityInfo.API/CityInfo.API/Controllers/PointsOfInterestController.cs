using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController: Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger
            , IMailService mailService, ICityInfoRepository cityInfoRepository)
        {
            _mailService = mailService;
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {

            if (!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"city with id {cityId} was not found");
                return NotFound();
            }
            var pointOfInterest = _cityInfoRepository.GetPointsOfInterestForCity(cityId);
            var POIResult = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterest);
                
            //    new List<PointOfInterestDto>();
            //foreach (var POI in pointOfInterest)
            //{
            //    POIResult.Add(new PointOfInterestDto
            //    {
            //        Name = POI.Name,
            //        Id = POI.Id,
            //        Description = POI.Description
            //    });
            //}
            return Ok(POIResult);          
        }
        [HttpGet("{cityId}/pointofinterest/{id}")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"city with id {cityId} was not found");
                return NotFound();
            }
            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId,id);
            var POIResult = Mapper.Map<PointOfInterestDto>(pointOfInterest);
                //new PointOfInterestDto()
                //{
                //    Name = pointOfInterest.Name,
                //    Id = pointOfInterest.Id,
                //    Description = pointOfInterest.Description
                //};            

            return Ok(POIResult);
        }
        [HttpPost("{cityId}/pointofinterest", Name ="getpointofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, 
            [FromBody] PointOfInterestForCreationDto PointOfInterest)
        {
            if(PointOfInterest == null)
            {
                return BadRequest();
            }
            if (PointOfInterest.Name == PointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Description and name cannot be same");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
               // var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            //var maxPointOfId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(PointOfInterest);
            //    new PointOfInterestDto()
            //{
            //    Id = ++maxPointOfId,
            //    Name = PointOfInterest.Name,
            //    Description = PointOfInterest.Description
            //};
            _cityInfoRepository.AddPointOfInterestForCIty(cityId, finalPointOfInterest);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Something wrong at the server side");
            }
            var createdPointOfInterest = Mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);
            
            return CreatedAtRoute("getpointofinterest",new {cityId = cityId, id = createdPointOfInterest.Id }, finalPointOfInterest);
        }
        [HttpPut("{cityId}/pointofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto PointOfInterest, int id)
        {

            if (PointOfInterest == null)
            {
                return BadRequest();
            }
            if (PointOfInterest.Name == PointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "Description and name cannot be same");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestFromStore = _cityInfoRepository.GetPointOfInterestForCity(cityId,id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            Mapper.Map(PointOfInterest, pointOfInterestFromStore);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Something wrong at the server side");
            }
            return NoContent();
        }
        [HttpPatch("{cityId}/pointofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestFromStore = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestFromStore);          
            patchDoc.ApplyTo(pointOfInterestPatch,ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
             if (pointOfInterestPatch.Name == pointOfInterestPatch.Description)
            {
                ModelState.AddModelError("Description", "Description and name cannot be same");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TryValidateModel(pointOfInterestPatch);
            Mapper.Map(pointOfInterestPatch, pointOfInterestFromStore);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Something wrong at the server side");
            }
            return NoContent();
        }
        [HttpDelete("{cityId}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestFromStore = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            _cityInfoRepository.DeletePointOfInterest(pointOfInterestFromStore);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Something wrong at the server side");
            }
            _mailService.send("This point of interest deleted",$"name {pointOfInterestFromStore.Name} and id {pointOfInterestFromStore.Id}");
            return NoContent();
        }
    }
}
