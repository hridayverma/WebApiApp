using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApiApp.Models;
using System.Net.Http;

namespace WebApiApp.Controllers
{
    public class APIClientController : Controller
    {
        // GET: APIClient
        public ActionResult Index()
        {
            IEnumerable<User_Master> users;
            HttpClient client = new HttpClient();
            client.BaseAddress =new Uri("http://localhost:65416/api/");
            var resTask=client.GetAsync("UserMaster");           
            resTask.Wait();
            var resResult=resTask.Result;
            if (resResult.IsSuccessStatusCode) {
                var readTask=resResult.Content.ReadAsAsync<IList<User_Master>>();
                readTask.Wait();
                users = readTask.Result;            
            }
            else
            {
                users = Enumerable.Empty<User_Master>();
                ModelState.AddModelError("", "Some Server Error!!!!");
            }
            return View(users);
        }

        //for insertion
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User_Master user_Master)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:65416/api/usermaster");
            var postTask=client.PostAsJsonAsync("User_Master", user_Master);
            postTask.Wait();
            var res = postTask.Result;
            if (res.IsSuccessStatusCode){
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Some error occured!!!");
            }
            return View(user_Master);
        }







    }
}