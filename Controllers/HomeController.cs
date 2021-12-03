using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outlook_Calendar_Test.Controllers
{
    public class HomeController : Controller
    {
        string credentialsFile = "C:\\Users\\alk22\\source\\repos\\Outlook Calendar Test\\files\\credentials.json";
        string adminCredentialsFile = "C:\\Users\\alk22\\source\\repos\\Outlook Calendar Test\\files\\admincredentials.json";

        public ActionResult Index()
        {
            return View();
        }

 
        
        public ActionResult OauthRedirect()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            var redirectUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?" +
                              "&scope=" + credentials["scopes"].ToString() +
                              "&response_type=code" +
                              "&response_mode=query" +
                              "&state=alk228" +
                              "&redirect_uri=" + credentials["redirect_url"].ToString() +
                              "&client_id=" + credentials["client_id"].ToString();
            return Redirect(redirectUrl);
        }

        public ActionResult AdminOauthRedirect()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(adminCredentialsFile));

            var redirectUrl = "https://login.microsoftonline.com/common/adminconsent?"+
                              "&state=alk228" +
                              "&redirect_uri=" +credentials["redirect_url"].ToString()+
                              "&client_id=" + credentials["client_id"].ToString();

            return Redirect(redirectUrl);
        }
    }
}