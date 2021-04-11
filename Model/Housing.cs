
namespace PetShop.Model
{
    public class Housing : Entity
    {
        public int? Number { get; set; }
        public int? IdPet { get; set; }

        public virtual Pet IdPetNavigation { get; set; }
    }
}
