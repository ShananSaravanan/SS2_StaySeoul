using Newtonsoft.Json;
using SS2_StaySeoul.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SS2_StaySeoul.Services
{

    public class ApiService
    {
        HttpClient client = new HttpClient();
        private string ngrokUrl = "https://ba7d-2001-d08-102b-4cfa-8cbf-e507-aa30-bd78.ngrok-free.app/";
        public async Task<User> LoginService(User loginUser)
        {
            string Url = ngrokUrl + "Api/Login";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            string json = JsonConvert.SerializeObject(loginUser);
            StringContent content = new StringContent(json,Encoding.UTF8,"application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                User loggedinUser = JsonConvert.DeserializeObject<User>(data);
                if (string.IsNullOrEmpty(data))
                {
                    return null;
                }
                else
                {
                    Application.Current.Properties["Username"] = loggedinUser.Username;
                    return loggedinUser;
                }
            }
            return null;
            
        }
        
        public async Task<List<Property>> GetPropertyListService()
        {
            string Url = ngrokUrl + "Api/GetPropertyList";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            HttpResponseMessage response = null;
            string json = "";
            HttpContent content = new StringContent(json,Encoding.UTF8,"application/json");
            response = await client.PostAsync(uri,content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Property> listofProperties = JsonConvert.DeserializeObject<List<Property>>(data);
                return listofProperties;
            }
            else
            {
                return null;
            }

        }
        public async Task<List<PropertyPrice>> GetPriceListService(PropertyPrice p)
        {
            string url = ngrokUrl + "Api/GetPriceList";
            Uri uri = new Uri(string.Format(url, string.Empty));
            string json = JsonConvert.SerializeObject(p); 
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri,content);
            if (response.IsSuccessStatusCode) { 
            string data = await response.Content.ReadAsStringAsync();
                
                List<PropertyPrice> propertyPrices = new List<PropertyPrice>();
             propertyPrices = JsonConvert.DeserializeObject<List<PropertyPrice>>(data);
                
                if (string.IsNullOrEmpty(data))
                {
                   
                    return null;
                }
                else
                {
                    return propertyPrices;
                }
            
            }
            return null;
        }
        public async Task<string> DeletePropertyPriceService(PropertyPrice itemPrice)
        {
            string Url = ngrokUrl + "Api/DeletePrice";
            Uri uri = new Uri(string.Format(Url, string.Empty));
            HttpResponseMessage response = null;
            string json = JsonConvert.SerializeObject(itemPrice);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine(data);
                return data;
            }
            else
            {
                return null;
            }

        }
        public async Task<string> AddPropertyPriceService(ItemPrice itemPrice)
        {
            string url = ngrokUrl + "Api/AddPrice";
            Uri uri = new Uri(string.Format(url, string.Empty));
            string json = JsonConvert.SerializeObject(itemPrice);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine("the data " + data);
                return data;
            }
            return null;
        }
    }
}
