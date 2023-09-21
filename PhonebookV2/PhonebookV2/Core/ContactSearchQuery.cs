namespace PhonebookV2.Core
{
    public class ContactSearchQuery
    {
        public string Term {  get; set; }
        public int? Page { get; set; }
        public int? ItemCount { get; set; }
    }
}
