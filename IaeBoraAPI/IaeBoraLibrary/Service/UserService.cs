using IaeBoraLibrary.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;
using IaeBoraLibrary.Model.Context;
using IaeBoraLibrary.Utils.Tools;
using IaeBoraLibrary.Model;
using System.Linq;

namespace IaeBoraLibrary.Service
{
    public static class UserService
    {
        public static User GetUser(string userId)
        {
            using (var context = new Context())
            {
                var userRegistered = context.Users.Where(u => u.GoogleId == userId).FirstOrDefault();
                if (userRegistered == null)
                    throw new UserServiceException("Usuário não cadastrado.");

                return userRegistered;
            }
        }

        public static void CreateUser(User user)
        {
            if (AddressTools.PostalCodeValidator(user.Address))
            {
                using (var context = new Context())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new AddressServiceException("CEP inválido.");
            }
        }

        public static void UpdateUser(User user)
        {
            using (var context = new Context())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
