using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    public class CitiesController: Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("api/cities")]
        public IActionResult  GetCities() {
            var cityEntries = _cityInfoRepository.GetCities();
            var result = Mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntries);
            //foreach(var cityEntry in cityEntries)
            //{
            //    result.Add(new CityWithoutPointOfInterestDto
            //    {
            //        Id = cityEntry.Id,
            //        Name = cityEntry.Name,
            //        Description = cityEntry.Description
            //    });
            //}
            return Ok(result);
        }
        [HttpGet("api/cities/{id}")]
        public IActionResult GetCity(int id, Boolean IncludePointOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, IncludePointOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (IncludePointOfInterest)
            {
                var cityResult = Mapper.Map<CityDTO>(city);
                //        new CityDTO(){
                //        Name = city.Name,
                //        Id = city.Id,
                //        Description = city.Description
                //    };
                //foreach (var POI in city.PointOfInterest)
                //    {
                //        cityResult.PointOfInterest.Add(new PointOfInterestDto
                //        {
                //            Name=POI.Name,
                //            Id=POI.Id,
                //            Description=POI.Description
                //        });
                //    }

                return Ok(cityResult);
            }
            else
            {

                var cityResult = Mapper.Map<CityWithoutPointOfInterestDto>(city);
                //new CityWithoutPointOfInterestDto(){
                //    Name = city.Name,
                //    Id = city.Id,
                //    Description = city.Description
                //};
                return Ok(cityResult);
            }
        }
            //var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(i=> i.Id==id);
            //if (cityToReturn == null)       
            //{
            //    return NotFound();
            //}
            //return Ok(cityToReturn);
        }
         
    }

