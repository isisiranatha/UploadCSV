namespace UploadCSV.Models
{
    public class CustomerUploadVerifyModel
    {
        public string property { get; set; }
        public string customer { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public string file { get; set; }
        public string hash { get; set; }
        public RestResultOutcome Returnoutcome { get; set; }
    }

    public enum RestResultOutcome
    {
        SUCCESS,
        FAIL
    }
}