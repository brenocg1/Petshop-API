using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using petshop.ModelsEF;
using petshop.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetOwnerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PetOwnerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPut]
        [Route("Create")]
        public async Task<PetOwner> CreatePetOwner([FromBody] CreatePetRequest request)
        {
            using (var context = new DBPetContext())
            {
                var petOwner = new PetOwner() {
                    Name = request.PetOwnerName,
                    Address = request.PetOwnerAddress,
                    PhoneNumber = request.PetOwnetPhoneNumber
                };

                context.Add(petOwner);
                await context.SaveChangesAsync();

                return petOwner;
            }
        }

        //Inserir dados do Dono do pet
        //Editar dados do Dono
        //Consulta de Dono

    }
}
