using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Requests
{
    public class CreatePetRequest
    {
        public string PetName { get; set; }
        public string PetOwnerName { get; set; }
        public string PetOwnerAddress { get; set; }
        public string PetOwnetPhoneNumber { get; set; }
        public string ReasonForHospitalization { get; set; }
        public string HealthStatus { get; set; }
    }
}
