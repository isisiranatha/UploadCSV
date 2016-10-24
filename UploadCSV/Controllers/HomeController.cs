using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using UploadCSV.Helpers;
using UploadCSV.Models;

namespace UploadCSV.Controllers
{
    public class HomeController : Controller
    {
        List<CustomerUploadModel> customers = new List<CustomerUploadModel>();
        List<CustomerUploadedHashResult> CustomerUploadedHashResult = new List<CustomerUploadedHashResult>();
        List<CustomerUploadErrorModel> CustomerUploadedErrorList = new List<CustomerUploadErrorModel>();
        List<CustomerUploadModel> CustomerOutList = new List<CustomerUploadModel>();
        
        public async Task<ActionResult> Index()
        {
            
            return View();
        }
               
        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            //List<CustomerUploadModel> customers = new List<CustomerUploadModel>();
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);                    
                    StreamReader csvreader = new StreamReader(file.InputStream);

                    while (!csvreader.EndOfStream)
                    {
                        var line = csvreader.ReadLine();
                        var values = line.Split(',');
                        if (values.Length > 1)
                        {
                            try
                            {
                                customers.Add(new CustomerUploadModel() { action = "order created", customer = values[0], file = "customerdata.csv", property = "Indika Sisiranatha", value = values[1] });                                

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("");
                            }
                        }
                    }

                   
                    string serverUrl = "http://evilapi.afdcloud.com.au/";
                    string apiPath = "/upload";
                    CustomerHashCodeListModel.HashCodeList = new List<CustomerUploadedHashResult>();
                    CustomerUploadErrorListModel.ErrorCodeList = new List<CustomerUploadErrorModel>();

                    foreach (CustomerUploadModel customer in customers)
                    {
                        HttpClient client;
                        client = new HttpClient();
                        client.BaseAddress = new Uri(serverUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        CustomerUploadResultModel result = await RestHelper.UploadCustomerData(serverUrl, apiPath, customer);
                        
                        if (result.outcome == RestCallOutcome.SUCCESS)
                        {
                            CustomerUploadedHashResult oCustomerUploadedHashResult = new CustomerUploadedHashResult();
                            oCustomerUploadedHashResult.added = result.added;
                            oCustomerUploadedHashResult.hash = ((CustomerUploadSuccessModel)result).hash;

                            CustomerHashCodeListModel.HashCodeList.Add(oCustomerUploadedHashResult);
                            
                        }
                        else
                        {
                            CustomerUploadErrorModel oCustomerUplodedError = new CustomerUploadErrorModel();
                            oCustomerUplodedError.added = result.added;
                            oCustomerUplodedError.errors = result.errors.errors;

                            CustomerUploadErrorListModel.ErrorCodeList.Add(oCustomerUplodedError);                           
                        }
                    }
                }
            }

            
            return RedirectToAction("Index", "Result");
                 
            
        }

           

    }

}