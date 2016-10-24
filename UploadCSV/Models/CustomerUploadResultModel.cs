namespace UploadCSV.Models
{
    public class CustomerUploadResultModel
    {
        public string added { get; set; }
        public RestCallOutcome outcome { get; set; }
        public CustomerUploadErrorModel errors { get; set; }
    }

    public enum RestCallOutcome
    {
        SUCCESS,
        FAIL
    }
}