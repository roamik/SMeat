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

            if(workplacesCount == 0)
            {
                var workplaces = new List<Workplace>
                {
                    new Workplace {CompanyName = "Neadevis", LocationId = "00f69324-a37d-46ef-94fb-29b0890a77ed"},
                    new Workplace {CompanyName = "Abto Software", LocationId = "0ae8d303-16e2-481a-9855-f6a6e20d2c5c"},
                    new Workplace {CompanyName = "SoftServe", LocationId = "3aeaafa7-5064-46fe-9ab8-0e41135682d4"},
                    new Workplace {CompanyName = "Chlenocom", LocationId = "515fb78b-47cb-4305-a0b1-f0c453aa50fb"},
                    new Workplace {CompanyName = "Auchan", LocationId = "61ded8ff-5b96-4c21-8b91-f63c21068bc4"},
                    new Workplace {CompanyName = "LifeCell", LocationId = "660e0db9-7550-4222-b538-e512f56e41db"},
                    new Workplace {CompanyName = "OLIMP", LocationId = "754f6175-b5d0-49e5-962d-8a098243b08a"},
                    new Workplace {CompanyName = "MTS", LocationId = "754f6175-b5d0-49e5-962d-8a098243b08a"},
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
