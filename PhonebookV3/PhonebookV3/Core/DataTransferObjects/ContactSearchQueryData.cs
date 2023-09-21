namespace PhonebookV3.Core.DataTransferObjects
{
    public class ContactSearchQueryData
    {
        public string Term { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public Dictionary<string,string> ToParam(int change) {
            return new Dictionary<string, string>
            {
                {"Term", this.Term},
                {"Page", (this.Page + change).ToString()},
                {"PageSize", this.PageSize.ToString() }
            };
        }
    }
}
