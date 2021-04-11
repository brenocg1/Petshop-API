using System.Linq;
using PetShop.Model;
using PetShop.Repository.Interfaces;

namespace PetShop.Repository
{
    public class PetOwnerRepository : Repository<PetOwner>, IPetOwnerRepository
    {
        public PetOwnerRepository(PetDbContext context) : base(context)
        { }

        public override void Delete(PetOwner entity)
        {
            base.Delete(entity);
            _context.SaveChanges();
        }

        public override void Save(PetOwner entity)
        {
            base.Save(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var petOwner = GetById(id);
            Delete(petOwner);
        }

        public IQueryable<PetOwner> GetAll()
               => _dbSet.AsQueryable();
    }
}