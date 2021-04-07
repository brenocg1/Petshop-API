using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petshop.ViewModels
{
    public class HousingsViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int? IdPet { get; set; }
        public string Status { get; set; }
    }
}
