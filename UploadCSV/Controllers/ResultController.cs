using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using UploadCSV.Models;

namespace UploadCSV.Controllers
{
    public class ResultController : Controller
    {
        List<CustomerUploadModel> CustomerOutList = new List<CustomerUploadModel>();
        // GET: Result
        public async Task<ActionResult> index()
        {
                                   
            return View(await CheckCustomerData());
        }

        private async Task<List<CustomerUploadModel>> CheckCustomerData()
        {
            foreach (CustomerUploadedHashResult oCustomerUploadedHashResult in CustomerHashCodeListModel.HashCodeList)
            {

                HttpClient chkCustomerClient;
                //The URL of the WEB API Service
                string serverUrl = "http://evilapi.afdcloud.com.au/check?hash=" + oCustomerUploadedHashResult.hash;

                chkCustomerClient = new HttpClient();
                chkCustomerClient.BaseAddress = new Uri(serverUrl);
                chkCustomerClient.DefaultRequestHeaders.Accept.Clear();
                chkCustomerClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await chkCustomerClient.GetAsync(serverUrl);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    //var oCustomerUploadModel = JsonConvert.DeserializeObject<List<CustomerUploadModel>>(responseData);
                    var oCustomerUploadModel = (CustomerUploadModel)JsonConvert.DeserializeObject<CustomerUploadModel>(responseData);

                    CustomerOutList.Add(oCustomerUploadModel);
                }
                else
                {
                    CustomerUploadModel oCustomerUploadModel = new CustomerUploadModel();
                    oCustomerUploadModel.hash = "Hash" + oCustomerUploadedHashResult.hash + "Not Found";

                }

            }
            return CustomerOutList;

        }

    }
}