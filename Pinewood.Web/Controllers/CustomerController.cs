using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pinewood.Web.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace Pinewood.Web.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44344/api");
        private readonly HttpClient _httpClient;
        public CustomerController()
        {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/Get/").Result;   

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);
            }

            return View(customerList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Customer/Post", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Customer Added";
                        return RedirectToAction("Index");

                    }

                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

            return View();

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                CustomerViewModel customer = new CustomerViewModel();
                HttpResponseMessage respones = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/Detail/" + id).Result;
                if (respones.IsSuccessStatusCode)
                {
                    string data = respones.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Edit(CustomerViewModel model)
        {         

            try
            {
                if (ModelState.IsValid)
                {
                    string data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Customer/Put", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Customer details updated";
                        return RedirectToAction("Index");

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                CustomerViewModel customer = new CustomerViewModel();  
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Customer/Detail/" + id).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<CustomerViewModel>(data);
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Customer/Delete/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer details deleted";
                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
    }
}
