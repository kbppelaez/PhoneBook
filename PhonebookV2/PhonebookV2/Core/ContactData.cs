namespace PhonebookV2.Core
{
    public class ContactData
    {
        public int ContactId { get; set; }
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
                else
                {
                    return FirstName + " " + LastName;
                }
            }
        }
    }
}
