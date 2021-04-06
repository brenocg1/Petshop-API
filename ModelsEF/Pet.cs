using System;
using System.Collections.Generic;

#nullable disable

namespace petshop.ModelsEF
{
    public partial class Pet
    {
        public Pet()
        {
            Housings = new HashSet<Housing>();
        }

        public int Id { get; set; }
        public int? IdPetOwner { get; set; }
        public string Name { get; set; }
        public string ReasonForHospitalizaion { get; set; }
        public string ProfilePhotoFileName { get; set; }

        public virtual PetOwner IdPetOwnerNavigation { get; set; }
        public virtual ICollection<Housing> Housings { get; set; }
    }
}
