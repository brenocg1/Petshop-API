﻿using Microsoft.AspNetCore.Http;
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
        private readonly HousingController _housingController;

        public PetController(
            IConfiguration configuration,
            HousingController housingController)
        {
            _configuration = configuration;
            _housingController = housingController;
        }



        //Inserir dados do pet (Informar em que alojamento ele está) OK
        //Editar dados do pet
        //Delete Pet OK
        //Consulta de Pet

        [HttpGet("[action]")]
        public async Task<IList<Pet>> SearchPet([FromQuery] string name)
        {
            try
            {
                using (var context = new DBPetContext())
                {
                    return await context.Pets.Where(x => x.Name.Contains(name.Trim())).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IList<Pet>> GetAllPets()
        {
            using(var context = new DBPetContext())
            {
                return await context.Pets.ToListAsync();
            }
        }

        [HttpGet("[action]")]
        public async Task<Pet> GetPetById([FromQuery] long id)
        {
            using (var context = new DBPetContext())
            {
                return await context.Pets.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        //set pet owner name as unique
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeletePetById([FromQuery] long id)
        {
            try
            {
                using (var context = new DBPetContext())
                {
                    var pet = context.Pets.Where(x => x.Id == id).FirstOrDefault();
                    var house = await context.Housings.Where(x => x.IdPet == pet.Id).FirstOrDefaultAsync();
                    //liberando o alojamento antes de excluir o animal
                    if(house != null)
                    {
                        house.IdPet = null;
                    }

                    //deletando o animal
                    context.Remove(pet);
                    context.SaveChanges();
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdatePet([FromBody] UpdatePetRequest request)
        {

            try
            {
                using (var context = new DBPetContext())
                {
                    var pet = await this.GetPetById(request.PetId);

                    if(pet == null)
                    {
                        return BadRequest();
                    }

                    pet.HealthCondition = request.HealthCondition;
                    pet.Name = request.Name;
                    pet.ReasonForHospitalization = request.ReasonForHospitalization;
                    context.Update(pet);
                    await context.SaveChangesAsync();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> CreatePet([FromBody] CreatePetRequest request)
        {
            try
            {
                if (await _housingController.isFull())
                {
                    return -1;
                }

                var pet = new Pet() {
                    IdPetOwner = request.PetOwnerId,
                    Name = request.Name.Trim(),
                    HealthCondition = request.HealthCondition.Trim(),
                    ReasonForHospitalization = request.ReasonForHospitalization.Trim()
                };

                using (var context = new DBPetContext())
                {
                    await context.AddAsync(pet);
                    await context.SaveChangesAsync();

                    return await _housingController.GetFirstEmptyHousing(pet.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
