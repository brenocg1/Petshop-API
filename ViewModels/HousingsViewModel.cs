
namespace PetShop.ViewModels
{
    public class HousingsViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int? IdPet { get; set; }
        public string Status { get; set; }
        public string PetName { get; set; }
        public string PetOwnerName { get; set; }
    }
}
