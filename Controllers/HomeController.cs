using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using login_registration.Models;

namespace login_registration.Controllers 
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.RegError = "";
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user)
        {
            ViewBag.RegError = "";
            if(ModelState.IsValid)
            {
                if((int?)HttpContext.Session.GetInt32("ID") != null)
                {
                    ViewBag.RegError = "a user is already signed in";
                    return View("Index");
                }
                string duplicateCheck = $"SELECT * FROM users WHERE email='{user.Email}';";
                List<Dictionary<string, object>> emailCheck = _dbConnector.Query(duplicateCheck);
                if(emailCheck.Count > 0)
                {
                    ViewBag.RegError = "e-mail address is already registered";
                    return View("Index");
                }
                string addUser = $"INSERT INTO users (first_name, last_name, email, password, created_at, updated_at)VALUES ('{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Password}', NOW(), NOW());";
                _dbConnector.Execute(addUser);
                string query = $"SELECT * FROM users WHERE email='{user.Email}' AND password='{user.Password}';";
                List<Dictionary<string, object>> UserInfo = _dbConnector.Query(query);
                HttpContext.Session.SetInt32("ID", (int)UserInfo[0]["id"]);
                HttpContext.Session.SetString("Name", user.FirstName);
                return Redirect("success");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            ViewBag.LoginError = "";
            return View("Login");
        }

        [HttpPost]
        [Route("processlogin")]
        public IActionResult ProcessLogin(UserLogin user)
        {
            ViewBag.LoginError = "";
            if(ModelState.IsValid)
            {
                if((int?)HttpContext.Session.GetInt32("ID") != null)
                {
                    ViewBag.LoginError = "a user is already signed in";
                    return View("Login");
                }
                string query = $"SELECT * FROM users WHERE email='{user.Email}' AND password='{user.Password}';";
                List<Dictionary<string, object>> Login = _dbConnector.Query(query);
                if(Login.Count > 0)
                {
                    HttpContext.Session.SetInt32("ID", (int)Login[0]["id"]);
                    HttpContext.Session.SetString("Name", (string)Login[0]["first_name"]);
                    return Redirect("success");
                }
                ViewBag.LoginError = "e-mail address and password do not match";
            }
            return View("Login");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            if((int?)HttpContext.Session.GetInt32("ID") == null)
            {
                return Redirect("/");
            }
            string userInfo = $"SELECT * FROM users WHERE id='{HttpContext.Session.GetInt32("ID") }' AND first_name='{HttpContext.Session.GetString("Name") }';";
            List<Dictionary<string, object>> UserInfo = _dbConnector.Query(userInfo);
            ViewBag.UserInfo = UserInfo;
            ViewBag.Name = HttpContext.Session.GetString("Name");
            return View("Success");
        }

        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
