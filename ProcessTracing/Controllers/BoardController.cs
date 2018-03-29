using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using ProcessTracing.Models;

namespace ProcessTracing.Controllers
{
    public class BoardController : Controller
    {

        //The below variables are only for temporary use -they will be replaced
        string key = "f27de49532bf871ca048feeb9e3ea8dc";
        string token = "442ec0924e45b0b9b1453aecc9007cbd47fec9fb873a16be2cdcc45e7a82122f";

        // GET: Board
        public ActionResult Index()
        {
            var model =new Login();
            var provider = HttpContext.GetOwinContext().Authentication.GetAuthenticationTypes(x => !string.IsNullOrWhiteSpace(x.Caption)).ToList();
            
            return View();
        }
    }
}