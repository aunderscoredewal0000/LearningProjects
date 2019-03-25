using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDTO> Cities {get;set;}
        public CitiesDataStore()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO{
                Id = 1,
                Name = "New York City",
                Description = "One with Great Park.",
                PointOfInterest = new List<PointOfInterestDto>
                {
                    new PointOfInterestDto
                    {
                        Id=1,
                        Name="Centeral Park",
                        Description="Place to sit"
                    },
                    new PointOfInterestDto
                    {
                        Id =2,
                        Name = "Empire State Buulding",
                        Description = "Historical building with nothing to see"
                    }
                }
               
                },
                new CityDTO{
                Id = 2,
                Name = "Antwerp",
                Description = "One with Cathederal.",
                PointOfInterest = new List<PointOfInterestDto>
                {
                    new PointOfInterestDto
                    {
                        Id=1,
                        Name="Cathederal",
                        Description="Place to Worship"
                    },
                    new PointOfInterestDto
                    {
                        Id =2,
                        Name = "Ant",
                        Description = "Place to twerk"
                    }
                }
                },
                new CityDTO{
                Id = 3,
                Name = "Paris",
                Description = "One with Big Tower.",
                PointOfInterest = new List<PointOfInterestDto>
                {
                    new PointOfInterestDto
                    {
                        Id=1,
                        Name="Efille tower",
                        Description="just a tall tower"
                    },
                    new PointOfInterestDto
                    {
                        Id =2,
                        Name = "Jardilands",
                        Description = "A lost Mi9 CLient"
                    },
                    new PointOfInterestDto
                    {
                        Id =3,
                        Name = "GoSport",
                        Description = "Existing  Mi9 CLient"
                    }
                }
                }
            };
        }
    }
}
