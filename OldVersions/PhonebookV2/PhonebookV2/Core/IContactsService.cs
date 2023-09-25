namespace PhonebookV2.Core
{
    public interface IContactsService
    {
        Task<List<ContactData>> ListAll(ContactSearchQuery args);
    }
}
