using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Outlook_Calendar_Test.Models;
using RestSharp;
using System;
using System.Web.Mvc;

namespace Outlook_Calendar_Test.Controllers
{
    public class CalendarController : Controller
    {
        string tokensFile = "C:\\Users\\alk22\\source\\repos\\Outlook Calendar Test\\files\\tokens.json";

        public ActionResult CreateEvent(CalendarEvent calendarEvent)
        {
            JObject tokens = JObject.Parse(System.IO.File.ReadAllText(tokensFile));

            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + tokens["access_token"].ToString());
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", JsonConvert.SerializeObject(calendarEvent), ParameterType.RequestBody);

            restClient.BaseUrl = new Uri("https://graph.microsoft.com/v1.0/me/calendar/events");
            var response = restClient.Post(restRequest);

            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Error");
        }

        public ActionResult Index()
        {
            return View();
        }


    }
}