using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, Boolean includePointOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        Boolean CityExists(int cityId);
        void AddPointOfInterestForCIty(int cityId, PointOfInterest pointOfInterest);
        Boolean Save();
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
