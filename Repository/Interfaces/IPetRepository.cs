using System.Linq;
using PetShop.Model;

namespace PetShop.Repository.Interfaces
{
    public interface IPetRepository : IRepository<Pet>
    {
        IQueryable<Pet> GetAll();
        IQueryable<Pet> GetByName(string name);
        void Delete(int id);
    }
}