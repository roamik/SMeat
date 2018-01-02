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
        private readonly IUnitOfWork _unitOfWork;
        public DataBaseInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        //In this function you can add some testing records into your database 
        //Warning!!! to avoid duplicates - check if data already exists
        public async Task Initialize()
        {
            var locationsCount = await _unitOfWork.BoardsRepository.CountAsync(filters: null);
            var workplacesCount = await _unitOfWork.WorkplacesRepository.CountAsync(filters: null);

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

            if (workplacesCount == 0)
            {
                var workplaces = new List<Workplace>
                {
                    new Workplace {CompanyName = "Neadevis", Location = new Location {City = "Lviv", Country = "Ukraine", Street = "Naukova"} },
                    new Workplace {CompanyName = "Abto Software", Location = new Location {City = "Lviv", Country = "Ukraine", Street = "V.Velukogo"}},
                    new Workplace {CompanyName = "SoftServe", Location = new Location {City = "Kyiv", Country = "Ukraine", Street = "Khlibna"}},
                    new Workplace {CompanyName = "Chlenocom", Location = new Location {City = "Wroclaw", Country = "Poland", Street = "Aikurwu"}},
                    new Workplace {CompanyName = "Auchan", Location = new Location {City = "Frankfurt", Country = "Germany", Street = "Meinfuhrera"}},
                    new Workplace {CompanyName = "LifeCell", Location = new Location {City = "Lviv", Country = "Ukraine", Street = "Patona"}},
                    new Workplace {CompanyName = "OLIMP", Location = new Location {City = "Lviv", Country = "Ukraine", Street = "Shchyretska"}},
                    new Workplace {CompanyName = "MTS", Location = new Location {City = "Lviv", Country = "Ukraine", Street = "Banderu"}},
                };

                foreach (var workplace in workplaces)
                {
                    await _unitOfWork.WorkplacesRepository.AddAsync(workplace);
                }

                await _unitOfWork.Save();
            }
        }
    }
}
