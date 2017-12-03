using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SMeat.DAL;
using SMeat.MODELS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMeat.API.Helpers
{
    public class DataBaseInitializer : IDataBaseInitializer
    {
        private IUnitOfWork _unitOfWork;
        public DataBaseInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //In this function you can add some testing records into your database 
        //Warning!!! to avoid duplicates - check if data already exists
        public async Task Initialize()
        {
            var locationsCount = await _unitOfWork.LocationsRepository.CountAsync(filters: null);

            if (locationsCount == 0)
            {
                var locations = new List<Location>
                {
                    new Location {City = "Lviv", Country = "Ukraine", Street = "Banderu"},
                    new Location {City = "Kyiv", Country = "Ukraine", Street = "Khreshchatyk" },
                    new Location {City = "Ternopil", Country = "Ukraine", Street = "Ternopilska" },
                    new Location {City = "Donetsk", Country = "Ukraine", Street = "Donetska" },
                    new Location {City = "Kharkiv", Country = "Ukraine" },
                    new Location {City = "Kropyvnytskyi", Country = "Ukraine" },
                    new Location {City = "Odessa", Country = "Ukraine" },
                    new Location {City = "Uman", Country = "Ukraine", Street = "Zhopomirska" }
                };

                foreach (var location in locations)
                {
                    await _unitOfWork.LocationsRepository.AddAsync(location);
                }

                await _unitOfWork.Save();
            }
        }
    }
}
