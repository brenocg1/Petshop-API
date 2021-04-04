using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Models
{
    public class Pet : Entity
    {
        //id do dono
        public string Name { get; set; }
        public string ReasonForHospitalization { get; set; }
        public string HealthStatus { get; set; }
        public string ProfilePhotoFileName { get; set; }
    }
}
