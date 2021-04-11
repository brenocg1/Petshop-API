
namespace PetShop.Requests
{
    public class UpdatePetRequest
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public string ReasonForHospitalization { get; set; }
        public string HealthCondition { get; set; }
    }
}
