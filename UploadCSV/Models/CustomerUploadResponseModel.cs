namespace UploadCSV.Models
{
    public class CustomerUploadResponseModel
    {
        public string added { get; set; }
        public RestCallOutcome outcome { get; set; }
        public string[] errors { get; set; }
    }
}