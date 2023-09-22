using PhonebookV3.Core.DataTransferObjects;
using PhonebookV3.Data;

namespace PhonebookV3.Core
{
    public interface IUsersService
    {
        Task<string> VerifyExisting(UserData account);
        Task<User> Find(string Email);
        Task<bool> VerifyNewAccount(string Email);
        Task RegisterAccount(UserData newUser);
    }
}
