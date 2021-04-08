using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using petshop.ModelsEF;
using petshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HousingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<int> GetFirstEmptyHousing(int idPet)
        {
            try
            {
                using (var context = new DBPetContext())
                {
                    var housing = await context.Housings.FirstOrDefaultAsync(x => x.IdPet == null);
                    housing.IdPet = idPet;
                    context.SaveChanges();
                    return housing.Number.Value;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<bool> isFull()
        {
            try
            {
                using (var context = new DBPetContext())
                {
                    var housing = await context.Housings.FirstOrDefaultAsync(x => x.IdPet == null);
                    return housing == null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IList<HousingsViewModel>> GetAllHousings()
        {

            try
            {
                using (var context = new DBPetContext())
                {
                    var housings = await context.Housings
                        .Include(x => x.IdPetNavigation)
                        .ThenInclude(x => x.IdPetOwnerNavigation)
                        .ToListAsync();
                    var listViewModel = new List<HousingsViewModel>();
                    foreach (var house in housings)
                    {
                        var element = new HousingsViewModel();
                        element.Id = house.Id;
                        element.IdPet = house.IdPet;
                        element.Number = house.Number.Value;

                        if (house.IdPet == null)
                        {
                            element.Status = "Livre";
                            listViewModel.Add(element);
                            continue;
                        }

                        element.PetName = house.IdPetNavigation.Name;
                        element.PetOwnerName = house.IdPetNavigation.IdPetOwnerNavigation.Name;

                        if(house.IdPetNavigation.HealthCondition == "Em Tratamento" ||
                            house.IdPetNavigation.HealthCondition ==  "Se Recuperando")
                        {
                            element.Status = "Ocupado";
                        }
                        else
                        {
                            element.Status = "Esperando o dono";
                        }

                        listViewModel.Add(element);
                    }

                    return listViewModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        //Pegar alojamento vago
    }
}
