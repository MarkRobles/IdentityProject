

using Model;
using Persistence;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Seervice
{
    public class UserService
    {

        public IEnumerable<ApplicationUser> GetAll() {
            var result = new List<ApplicationUser>();

            using (var ctx = new ApplicationDbContext()){
                result = ctx.ApplicationUsers.ToList();
            }
            return result;
        }


        public ApplicationUser Get(string id)
        {
            var result = new ApplicationUser();

            using (var ctx = new ApplicationDbContext())
            {
                result = ctx.ApplicationUsers.Where(x => x.Id == id).Single();
            }
            return result;
        }


        public  void Update(ApplicationUser model)
        {
            var result = new ApplicationUser();

            using (var ctx = new ApplicationDbContext())
            {
                var originalEntity = ctx.ApplicationUsers.Where(x => x.Id == model.Id).Single();

                originalEntity.Name = model.Name;
                originalEntity.LastName = model.LastName;

                ctx.Entry(model).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        
        }
    }
}
