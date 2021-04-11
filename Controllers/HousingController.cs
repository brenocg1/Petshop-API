using Microsoft.AspNetCore.Mvc;
using PetShop.Repository.Interfaces;

namespace PetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousingController : ControllerBase
    {
        private readonly IHousingRepository _housingRepository;

        public HousingController(IHousingRepository housingRepository)
               => _housingRepository = housingRepository;


        [HttpGet]
        public IActionResult GetAllHousings()
               => Ok(_housingRepository.GetAll());
    }
}
