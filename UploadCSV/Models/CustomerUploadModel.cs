namespace UploadCSV.Models
{
    public class CustomerUploadModel
    {
        public string property { get; set; }
        public string customer { get; set; }
        public string action { get; set; }
        public string value { get; set; }
        public string file { get; set; }
        public string hash { get; set; }
    }

    public enum Outcome
    {
        SUCCESS,
        FAIL
    }
}