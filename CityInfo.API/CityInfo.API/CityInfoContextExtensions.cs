using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }
            var Cities = new List<City>()
            {
                new City{
                Name = "New York City",
                Description = "One with Great Park.",
                PointOfInterest = new List<PointOfInterest>
                {
                    new PointOfInterest
                    {
                        Name="Centeral Park",
                        Description="Place to sit"
                    },
                    new PointOfInterest
                    {
                        Name = "Empire State Buulding",
                        Description = "Historical building with nothing to see"
                    }
                }

                },
                new City{
                Name = "Antwerp",
                Description = "One with Cathederal.",
                PointOfInterest = new List<PointOfInterest>
                {
                    new PointOfInterest
                    {
                        Name="Cathederal",
                        Description="Place to Worship"
                    },
                    new PointOfInterest
                    {
                        Name = "Ant",
                        Description = "Place to twerk"
                    }
                }
                },
                new City{
                Name = "Paris",
                Description = "One with Big Tower.",
                PointOfInterest = new List<PointOfInterest>
                {
                    new PointOfInterest
                    {
                        Name="Efille tower",
                        Description="just a tall tower"
                    },
                    new PointOfInterest
                    {
                        Name = "Jardilands",
                        Description = "A lost Mi9 CLient"
                    },
                    new PointOfInterest
                    {
                        Name = "GoSport",
                        Description = "Existing  Mi9 CLient"
                    }
                }
                }
            };
            context.Cities.AddRange(Cities);
            context.SaveChanges();
        }
    }
}
