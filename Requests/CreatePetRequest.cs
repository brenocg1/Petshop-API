
namespace PetShop.Requests
{
    public class CreatePetRequest
    {
        public int PetOwnerId { get; set; }
        public string Name { get; set; }
        public string ReasonForHospitalization { get; set; }
        public string HealthCondition { get; set; }
    }
}
