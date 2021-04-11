using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetShop.Model;
using PetShop.Repository.Interfaces;
using PetShop.Requests;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetRepository _petRepository;

        public PetController(IPetRepository petRepository)
               => _petRepository = petRepository;


        [HttpGet]
        public IActionResult GetAllPets()
               => Ok( _petRepository.GetAll().ToList());              

        [HttpGet("{id}")]
        public IActionResult GetPetById(int id)
               => Ok(_petRepository.GetById(id));


        [HttpGet("search/{name}")]
        public IActionResult SearchPet(string name)
               => Ok(_petRepository.GetByName(name)); 


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _petRepository.Delete(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(UpdatePetRequest request)
        {
            var pet = new Pet 
            {
                Id = request.PetId,    
                Name = request.Name,
                HealthCondition = request.HealthCondition,
                ReasonForHospitalization = request.ReasonForHospitalization,
            };
            _petRepository.Update(pet);

            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreatePetRequest request)
        {
            var pet = new Pet
            {
                IdPetOwner = request.PetOwnerId,
                Name = request.Name,
                HealthCondition = request.HealthCondition,
                ReasonForHospitalization = request.ReasonForHospitalization
            };
            _petRepository.Save(pet);

            return CreatedAtAction(nameof(GetPetById),new { id = pet.Id });
        }
    }
}
