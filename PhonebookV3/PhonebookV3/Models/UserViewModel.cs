using PhonebookV3.Core;
using PhonebookV3.Core.Application;
using PhonebookV3.Core.DataTransferObjects;

namespace PhonebookV3.Models
{
    public class UserViewModel
    {
        // Constructors
        public UserViewModel() { }

        // Properties
        public IUsersService _usersService;
        public UserData User {  get; set; }
        public bool fromLogin { get; set; }
        public bool fromRegister { get; set; }
        public bool success {  get; set; }
        public string errorMsg { get; set; }

        //Methods
        public void AddService(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        public async Task VerifyExistingAccount()
        {
            errorMsg = await _usersService.VerifyExisting(User);

            if (errorMsg.Equals("OK"))
                success = true;
            else
                success = false;    
        }

        public async Task VerifyNewAccount()
        {
            if(await _usersService.VerifyNewAccount(User.Email))
            {
                success = true;
                await _usersService.RegisterAccount(User);
            }
            else
            {
                success = false;
                errorMsg = "Email address is already registered.";
            }
        }
    }
}
