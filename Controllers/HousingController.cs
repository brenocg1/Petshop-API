using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using petshop.ModelsEF;
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
        public async Task<IList<Housing>> GetAllHousings()
        {
            using (var context = new DBPetContext())
            {
                return await context.Housings.ToListAsync();
            }
        }

        //Pegar alojamento vago
    }
}
