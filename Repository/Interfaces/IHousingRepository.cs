using System.Collections.Generic;
using PetShop.Model;
using PetShop.ViewModels;

namespace PetShop.Repository.Interfaces
{
    public interface IHousingRepository : IRepository<Housing>
    {
        IList<HousingsViewModel> GetAll();
    }
}