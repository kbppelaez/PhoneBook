using PhonebookV3.Core.DataTransferObjects;
using PhonebookV3.Data;
using System.Security.Cryptography;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace PhonebookV3.Core.Application
{
    public class UsersService : IUsersService
    {
        private readonly PhonebookDbContext _db;
        public UsersService(PhonebookDbContext db) {
            _db = db;
        }

        // CHECK if USER EXISTS
        public async Task<string> VerifyExisting(UserData account)
        {
            User existingUser = await Find(account.Email);
            if (existingUser == null)
                return "Email does not exist.";

            if (VerifyHashedPassword(existingUser.Password, account.Password))
                return "OK";
            else return "Incorrect Password.";
        }

        // SEARCH the email
        public async Task<User> Find(string Email)
        {
            IQueryable<User> query = _db.User;
            query = query.Where(c => c.Email.Equals(Email));
            return await query.FirstOrDefaultAsync();
        }

        // CHECK if USER IS NEW
        public async Task<bool> VerifyNewAccount(string Email)
        {
            User existingUser = await Find(Email);
            if (existingUser == null) return true;
            return false;
        }

        // ADD USER TO DATABASE
        public async Task RegisterAccount(UserData newUser)
        {
            newUser.Password = HashPassword(newUser.Password);
            _db.Add(new User(newUser));
            await _db.SaveChangesAsync();
        }

        // [DE]HASHING FUNCTIONS for the Password
        public string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;

            using(Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }

            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public bool VerifyHashedPassword(string hashedpassword, string password)
        {
            byte[] buffer4;
            byte[] src = Convert.FromBase64String(hashedpassword);
            if (src.Length != 0x31 || src[0] != 0)
                return false;

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using(Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return buffer3.SequenceEqual(buffer4);
        }
    }
}
