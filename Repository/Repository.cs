using Microsoft.EntityFrameworkCore;
using PetShop.Model;
using PetShop.Repository.Interfaces;
namespace PetShop.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly PetDbContext _context;
        protected DbSet<T> _dbSet;

        public Repository(PetDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id)
            => _context.Set<T>().Find(id);

        public virtual void Delete(T entity)
            => _context.Remove(entity);            

        public virtual void Save(T entity)
            => _context.Add(entity);

        public virtual void Update(T entity)
        {
            _context.Update<T>(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}