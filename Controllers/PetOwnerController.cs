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
    public class PetOwnerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PetOwnerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Inserir dados do Dono do pet
        //Editar dados do Dono
        //Consulta de Dono

    }
}
