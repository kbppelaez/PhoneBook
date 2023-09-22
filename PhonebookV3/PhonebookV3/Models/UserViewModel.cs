using PhonebookV3.Core.Application;
using PhonebookV3.Core.DataTransferObjects;

namespace PhonebookV3.Models
{
    public class UserViewModel
    {
        // Constructors
        public UserViewModel() { }

        // Properties
        public UsersService _usersService;
        public UserData User {  get; set; }
        public bool fromLogin { get; set; }
        public bool success {  get; set; }
        public string errorMsg { get; set; }

        //Methods
        public void AddService(UsersService usersService)
        {
            this._usersService = usersService;
        }

        public async Task Verify()
        {
            errorMsg = await _usersService.Verify(User);

            if (errorMsg.Equals("OK"))
                success = true;
            else
                success = false;
        }
    }
}
