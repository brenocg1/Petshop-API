using System.Collections.Generic;

namespace PetShop.Model
{
    public class PetOwner : Entity
    {
        public PetOwner()
        {
            Pets = new HashSet<Pet>();
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
