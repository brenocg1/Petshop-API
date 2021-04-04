using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public PetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Inserir dados do pet (Informar em que alojamento ele está)
        //Editar dados do pet
        //Delete Pet
        //Consulta de Pet

        //[HttpGet]
        //public JsonResult Get()
        //{
        //}
    }
}
