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
    public class PetController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PetOwnerController _petOwnerController;

        public PetController(IConfiguration configuration, PetOwnerController petOwnerController)
        {
            _configuration = configuration;
            _petOwnerController = petOwnerController;
        }

        [HttpGet]
        public async Task<IEnumerable<Pet>> GetAllPets()
        {
            using (var context = new DBPetContext())
            {
                return await context.Pets.ToListAsync();
            }
        }

        [HttpGet]
        [Route("GetById")]
        public Pet GetPetById([FromQuery] long id)
        {
            using (var context = new DBPetContext())
            {
                return context.Pets.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        [HttpPut]
        [Route("Create")]
        public async Pet CreatePet([FromBody] CreatePetRequest request)
        {
            using (var context = new DBPetContext())
            {
                var pet = new Pet();

                await _petOwnerController.CreatePetOwner(request);

                //return context.Pets.Where(x => x.Id == id).FirstOrDefault();
            }
        }


        //Inserir dados do pet (Informar em que alojamento ele está)
        //Editar dados do pet
        //Delete Pet
        //Consulta de Pet
    }
}
