using PetShop.Model;

namespace PetShop.Repository.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        void Save(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int id);
    }
}