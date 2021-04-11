using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetShop.Model;
using PetShop.Repository.Interfaces;
using PetShop.Requests;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetOwnerController : ControllerBase
    {
        private readonly IPetOwnerRepository _petOwnerRepository;

        public PetOwnerController(IPetOwnerRepository petOwnerRepository)
               => _petOwnerRepository = petOwnerRepository;

        [HttpGet("{id}")]
        public IActionResult GetPetOwnerById(int id)
               => Ok(_petOwnerRepository.GetById(id)); 

        [HttpGet]
        public IActionResult GetAllPetOwners()
               => Ok(_petOwnerRepository.GetAll().ToList());               

        [HttpPost]
        public IActionResult CreatePetOwner(CreatePetOwnerRequest request)
        {
            var petOwner = new PetOwner 
            {
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber
            };
            _petOwnerRepository.Save(petOwner);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePetOwner(int id)
        {
            _petOwnerRepository.Delete(id);
            return Ok();
        }
    }
}
