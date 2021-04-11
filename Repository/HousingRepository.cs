using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetShop.Model;
using PetShop.ViewModels;
using PetShop.Repository.Interfaces;

namespace PetShop.Repository
{
    public class HousingRepository : Repository<Housing>, IHousingRepository
    {
        public HousingRepository(PetDbContext context) : base(context)
        { }

        public IList<HousingsViewModel> GetAll()
        {          
            var sqlQuery = @"SELECT H.Id,
                H.Number, 
                P.Id as IdPet,
                P.Name as PetName,
                PO.Name as PetOwnerName,
                CASE 
                 WHEN P.HealthCondition IS NULL THEN 'Livre'
                ELSE P.HealthCondition 
                END  AS Status
                FROM Housing AS H LEFT JOIN Pet AS P
                    ON  p.Id=H.IdPet
                LEFT JOIN PetOwner as PO 
                ON P.IdPetOwner=PO.Id";
            _context.Database.OpenConnection();
            var list = new List<HousingsViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;
                using var result = command.ExecuteReader();
                while (result.Read())
                {
                    list.Add(new HousingsViewModel 
                    {
                        Id = int.Parse(result["Id"].ToString()),
                        Number = int.Parse(result["Number"].ToString()),
                        IdPet = result["IdPet"].ToString() == string.Empty ? null : int.Parse(result["IdPet"].ToString()),
                        Status = result["Status"].ToString(),                             
                        PetName = result["PetName"].ToString(),
                        PetOwnerName = result["PetOwnerName"].ToString(),
                    });
                }              
            };
            return list;
        }
    }
}