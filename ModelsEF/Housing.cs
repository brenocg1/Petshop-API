using System;
using System.Collections.Generic;

#nullable disable

namespace petshop.ModelsEF
{
    public partial class Housing
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public int? IdPet { get; set; }

        public virtual Pet IdPetNavigation { get; set; }
    }
}
