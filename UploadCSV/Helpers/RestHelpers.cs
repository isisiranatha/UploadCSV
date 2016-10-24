using System;
using System.Net.Http;
using System.Threading.Tasks;
using UploadCSV.Models;

namespace UploadCSV.Helpers
{
    public class RestHelper
    {
       
        public static async Task<CustomerUploadResultModel> UploadCustomerData(string url, string apipath, CustomerUploadModel customer)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(apipath, customer);
            
            if (response.IsSuccessStatusCode)
            {
                CustomerUploadSuccessModel successModel = await response.Content.ReadAsAsync<CustomerUploadSuccessModel>();
                successModel.outcome = RestCallOutcome.SUCCESS;
                return successModel;
            }
            else
            {
                CustomerUploadErrorModel errorModel = await response.Content.ReadAsAsync<CustomerUploadErrorModel>();
                errorModel.outcome = RestCallOutcome.FAIL;                
                return errorModel;
            }
        }
        
        public static async Task<CustomerUploadVerifyModel> VerifyCustomerData(string url, string apipath, string hash)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apipath + "/check?hash=" + hash);
            CustomerUploadVerifyModel CustomerUploadVerify = await response.Content.ReadAsAsync<CustomerUploadVerifyModel>();

            if ( response.IsSuccessStatusCode)
            {
                CustomerUploadVerify.Returnoutcome = RestResultOutcome.SUCCESS;
                return CustomerUploadVerify;
            }else  
            {
                CustomerUploadVerify.Returnoutcome = RestResultOutcome.FAIL;
                return CustomerUploadVerify;
            }
        }



    }
}