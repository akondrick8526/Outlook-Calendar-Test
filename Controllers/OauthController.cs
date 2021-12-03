using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Outlook_Calendar_Test.Controllers
{
    public class OauthController : Controller
    {
        string credentialsFile = "C:\\Users\\alk22\\source\\repos\\Outlook Calendar Test\\files\\credentials.json";
        string tokensFile = "C:\\Users\\alk22\\source\\repos\\Outlook Calendar Test\\files\\tokens.json";
        string adminCredentialsFile = "C:\\Users\\alk22\\source\\repos\\Outlook Calendar Test\\files\\admincredentials.json";


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Callback(string code, string state, string error)
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));

            if(!string.IsNullOrWhiteSpace(code))
            {
                RestClient restClient = new RestClient();
                RestRequest restRequest = new RestRequest();

                restRequest.AddParameter("client_id", credentials["client_id"].ToString());
                restRequest.AddParameter("scope", credentials["scopes"].ToString());
                restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
                restRequest.AddParameter("code", code);
                restRequest.AddParameter("grant_type", "authorization_code");
                restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());

                restClient.BaseUrl = new Uri("http://login.microsoftonline.com/common/oauth2/v2.0/token");
                var response = restClient.Post(restRequest);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.File.WriteAllText(tokensFile, response.Content);
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Error");
        }

        public ActionResult AdminCallback(string tenant, string state, string admin_consent)
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(adminCredentialsFile));

            if(!string.IsNullOrWhiteSpace(tenant))
            {
                RestClient restClient = new RestClient();
                RestRequest restRequest = new RestRequest();

                restRequest.AddParameter("client_id", credentials["client_id"].ToString());
                restRequest.AddParameter("scope", credentials["scopes"].ToString());
                restRequest.AddParameter("grant_type", "client_credentials");
                restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());

                restClient.BaseUrl = new Uri($"https://login.microsoftonline.com/{tenant}/oauth2/v2.0/token");
                var response = restClient.Post(restRequest);

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.File.WriteAllText(tokensFile, response.Content);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Error");
        }

        public ActionResult RefreshToken()
        {
            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest();

            restRequest.AddParameter("client_id", credentials["client_id"].ToString());
            restRequest.AddParameter("grant type", "refresh_token");
            restRequest.AddParameter("scope", credentials["scopes"].ToString());
            restRequest.AddParameter("refresh_token", tokens["refresh_token"].ToString());
            restRequest.AddParameter("redirect_uri", credentials["redirect_url"].ToString());
            restRequest.AddParameter("client_secret", credentials["client_secret"].ToString());

            restClient.BaseUrl = new Uri($"https://login.microsoftonline.com/common/oauth2/v2.0/token");
            var response = restClient.Post(restRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.IO.File.WriteAllText(tokensFile, response.Content);
                return RedirectToAction("Index", "Home");
            }
        
            return RedirectToAction("Error");


    }

        
    }
}