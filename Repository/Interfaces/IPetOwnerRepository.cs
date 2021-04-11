using System.Linq;
using PetShop.Model;

namespace PetShop.Repository.Interfaces
{
    public interface IPetOwnerRepository : IRepository<PetOwner>
    {
        IQueryable<PetOwner> GetAll();
        void Delete(int id);
    }
}