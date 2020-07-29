

using Model;
using Persistence;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Seervice
{
    public class UserService
    {

        public IEnumerable<UserGrid> GetAll() {
            var result = new List<UserGrid>();

            using (var ctx = new ApplicationDbContext()){
                result = (
                     from au in ctx.ApplicationUsers
                     from aur in ctx.ApplicationUserRole.Where(x => x.UserId == au.Id).DefaultIfEmpty()
                     from ar in ctx.ApplicationRoles.Where(x => x.Id == aur.RoleId && x.Enabled).DefaultIfEmpty()
                     select new UserGrid
                     {
                         Id = au.Id,
                         Name = au.UserName,
                         LastName = au.LastName,
                         Email = au.Email,
                         Role = ar.Name
                     }
                     ).ToList();
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
