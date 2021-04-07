using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Requests
{
    public class CreatePetRequest
    {
        public int PetOwnerId { get; set; }
        public string Name { get; set; }
        public string ReasonForHospitalization { get; set; }
        public string HealthCondition { get; set; }
    }
}
