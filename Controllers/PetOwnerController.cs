using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("[action]")]
        public PetOwner GetPetOwnerById([FromQuery] long id)
        {
            using (var context = new DBPetContext())
            {
                return context.PetOwners.Where(x => x.Id == id).FirstOrDefault();
            }
        }


        //Inserir dados do Dono do pet
        //Editar dados do Dono
        //Consulta de Dono

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePetOwner(CreatePetOwnerRequest request)
        {
            var PetOwner = new PetOwner() {
                Name = request.Name.Trim(),
                Address = request.Address.Trim(),
                PhoneNumber = request.PhoneNumber.Trim()
            } ;
            try
            {
                using (var context = new DBPetContext())
                {
                    context.Add(PetOwner);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

            return CreatedAtAction(nameof(GetPetOwnerById), new { id = PetOwner.Id }, PetOwner);
        }


        [HttpGet("[action]")]
        public async Task<IList<PetOwner>> GetAllPetOwners()
        {
            using (var context = new DBPetContext())
            {
                return await context.PetOwners.ToListAsync();
            }
        }

    }
}
