using System.Linq;
using PetShop.Model;
using PetShop.Repository.Interfaces;

namespace PetShop.Repository
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        public PetRepository(PetDbContext context) : base(context)
        { }

        public IQueryable<Pet> GetAll()
            => _dbSet.AsQueryable<Pet>();

        public IQueryable<Pet> GetByName(string name)
            => _dbSet.Where(x=>x.Name.Contains(name));

        public override void Delete(Pet entity)
        {
            base.Delete(entity);
            _context.SaveChanges();
        }

        public override void Save(Pet entity)
        {
            base.Save(entity);
            _context.SaveChanges();
        }

        public override void Update(Pet entity)
        {
            base.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pet = GetById(id);
            Delete(pet);
        }
    }
}