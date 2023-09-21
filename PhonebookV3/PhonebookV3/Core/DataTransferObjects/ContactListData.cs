namespace PhonebookV3.Core.DataTransferObjects
{
    public class ContactListData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(LastName))
                {
                    return FirstName;
                }
                else return FirstName + " " + LastName;
            }
        }
    }
}
