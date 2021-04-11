using System.Collections.Generic;

namespace PetShop.Model
{
    public class Pet : Entity
    {
        public Pet()
        {
            Housings = new HashSet<Housing>();
        }

        public int IdPetOwner { get; set; }
        public string Name { get; set; }
        public string ReasonForHospitalization { get; set; }
        public string ProfilePhotoFileName { get; set; }
        public string HealthCondition { get; set; }

        public virtual PetOwner IdPetOwnerNavigation { get; set; }
        public virtual ICollection<Housing> Housings { get; set; }
    }
}
