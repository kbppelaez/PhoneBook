using PhonebookV3.Core.DataTransferObjects;

namespace PhonebookV3.Core
{
    public interface IContactsService
    {
        Task<ContactListData[]> GetAll(ContactSearchQueryData queryData);
        Task<string> InsertContact(ContactData newcontact);
        Task<ContactData> Search(int id);
        Task<string> UpdateContact(ContactData contact);
        Task<string> DeleteContact(int id);
    }
}
