using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.Models
{
    public class Housing : Entity
    {
        public int HousingNumber { get; set; }
        public int QuantityOfPets { get; set; }
    }
}
