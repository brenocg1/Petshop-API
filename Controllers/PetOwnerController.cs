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
        private readonly PetController _petController;

        public PetOwnerController(IConfiguration configuration, PetController petController)
        {
            _configuration = configuration;
            _petController = petController;
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

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePetOwner(CreatePetOwnerRequest request)
        {
            var PetOwner = new PetOwner() {
                Name = request.Name.Trim(),
                Address = request.Address.Trim(),
                PhoneNumber = request.PhoneNumber.Trim()
            };
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

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeletePetOwner(int id)
        {
            try
            {
                using (var context = new DBPetContext())
                {
                    var owner = await context.PetOwners
                        .Where(x => x.Id == id)
                        .Include(x => x.Pets)
                        .FirstOrDefaultAsync();

                    if(owner == null)
                    {
                        return NotFound();
                    }
                    
                    if(owner.Pets.Count > 0)
                    {
                        foreach (var pet in owner.Pets)
                        {
                            await _petController.DeletePetById(pet.Id);
                        }
                    }

                    context.Remove(owner);
                    context.SaveChanges();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IList<PetOwner>> GetAllPetOwners()
        {
            using (var context = new DBPetContext())
            {
                return await context.PetOwners.Include(x => x.Pets).ToListAsync();
            }
        }

    }
}
