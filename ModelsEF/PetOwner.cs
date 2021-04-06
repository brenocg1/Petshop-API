using System;
using System.Collections.Generic;

#nullable disable

namespace petshop.ModelsEF
{
    public partial class PetOwner
    {
        public PetOwner()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
