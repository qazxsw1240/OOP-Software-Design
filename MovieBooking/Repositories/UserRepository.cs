using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using MovieBooking.Entity;
using MovieBooking.Utility;

namespace MovieBooking.Repositories
{
    public class UserRepository(EntityDbContext dbContext) : DbContextRepository<User>(dbContext)
    {
        public override bool Add(User entity)
        {
            if (!NameValidator.Test(entity.Name) || Entities.Any(u => u.Name == entity.Name))
            {
                return false;
            }
            return base.Add(entity);
        }

        public override bool Update(User entity)
        {
            return NameValidator.Test(entity.Name) && base.Update(entity);
        }

        protected override DbSet<User> GetDbSet()
        {
            return dbContext.Users;
        }

        private static readonly Validator<string> NameValidator = new Validator<string>()
            .With(
                name => !string.IsNullOrWhiteSpace(name),
                _ => throw new ArgumentException("Name cannot be empty."))
            .With(
                name => name.All(ch => char.IsLetterOrDigit(ch) || char.IsSymbol(ch)),
                _ => throw new ArgumentException("Name contains only readable ASCII characters"));
    }
}
