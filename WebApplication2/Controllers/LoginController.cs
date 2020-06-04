using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public async Task<ActionResult> Index()
        {
            
            //var client = new HttpClient();
            //var responseTask = await client.GetAsync("https://localhost:44309/api/values");
            //if (responseTask.IsSuccessStatusCode)
            //{
            // var sss=  await responseTask.Content.ReadAsStringAsync();
            //}
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(loginclass Info)
        {
            var client = new HttpClient();
            //var client = new HttpClient();
            //var responseTask = await client.GetAsync("https://localhost:44309/api/values");
            //if (responseTask.IsSuccessStatusCode)
            //{
            // var sss=  await responseTask.Content.ReadAsStringAsync();
            if (ModelState.IsValid)
            {
                using (client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63847");

                    //HTTP POST
                    if (string.IsNullOrEmpty(Info.PassCode))
                    {
                        var httpContent = new StringContent(JsonConvert.SerializeObject(Info), Encoding.UTF8, "application/json");
                        var postTask = client.PostAsync("api/Register", httpContent);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                            loginclass Infor = JsonConvert.DeserializeObject<loginclass>(await result.Content.ReadAsStringAsync());
                            return View(Infor);
                        }
                    }
                }
            }
            else
            {
                using (client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63847");
                    var httpContent = new StringContent(JsonConvert.SerializeObject(Info), Encoding.UTF8, "application/json");
                    var postTask = client.PostAsync("/api/Validate", httpContent);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        if (!Info.ShowPassCode)
                            Info.ShowPassCode = true;
                        if (!string.IsNullOrEmpty(Info.PassCode) && Info.ShowPassCode)
                            Info.LoggedIn = true;
                        //}
                        return View(Info);
                    }
                }               
            }
            return View(Info);

        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> SignUp(UserInfo signUpModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63847");
                    var httpContent = new StringContent(JsonConvert.SerializeObject(signUpModel), Encoding.UTF8, "application/json");

                    var postTask = client.PostAsync("api/Register/PostRegister", httpContent);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        UserInfo Infor = JsonConvert.DeserializeObject<UserInfo>(await result.Content.ReadAsStringAsync());
                        return RedirectToActionPermanent("Register", Infor);
                    }
                }
            }
            return View(signUpModel);
        }
       
        public ActionResult Register(UserInfo message)
        {
           // ViewBag.Message = message;
            return View(message);
        }
        [HttpPost]
        public async Task<ActionResult> Register(UserInfo message,string url)
        {
            // ViewBag.Message = message;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63847");
                var httpContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

                var postTask = client.PostAsync("api/Validate", httpContent);
                postTask.Wait();

                var result = postTask.Result;
                UserInfo Infor;
                if (result.IsSuccessStatusCode)
                {
                     Infor = JsonConvert.DeserializeObject<UserInfo>(await result.Content.ReadAsStringAsync());
                    //}
                    return RedirectToActionPermanent("Validate", Infor);
                    //return View(Info);
                }
            }
            return View();
        }
        public ActionResult Validate(UserInfo message)
        {
            // ViewBag.Message = message;
            return View(message);
        }
    }
}