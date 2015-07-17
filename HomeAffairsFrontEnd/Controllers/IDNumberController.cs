using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using HomeAffairsFrontEnd.Models;
using System.Configuration;

namespace HomeAffairsFrontEnd.Controllers
{
    public class IDNumberController : Controller
    {
        IDNumber idNumber;
        string id = string.Empty;


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateIDNumber()
        {
            //No need for exception handling as the erro(s) will be bubbled up from the webAPI call.

            Int64 idn = 0;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(getIDNumberAPIHost());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(getIDNumberAPILocation()).Result;
            char[] c = { '\\', '"' };

            if (response.IsSuccessStatusCode)
            {
                id = response.Content.ReadAsStringAsync().Result;
                ViewBag.idNum = Int64.Parse((id.Trim(c)));
                idn = ViewBag.idNum;

                TempData["idNumber"] = idn;

                idNumber = new IDNumber
                {
                    idNumber = id
                };

            }
            else
            {
                idNumber = new IDNumber
                {
                    idNumber = id
                };
                ViewBag.idNum = "Couldn't Generate ID Number";

            }


            return View();

        }

        public ActionResult GetControlDigit()
        {
            //No need for exception handling as the erro(s) will be bubbled up from the webAPI call.

            var idN = TempData["idNumber"];

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetControlDigitAPIHost());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(GetControlDigitAPILocation() + idN).Result;
            char[] c = { '\\', '"' };
            if (response.IsSuccessStatusCode)
            {
                ViewBag.valid = (response.Content.ReadAsStringAsync().Result).Trim(c);

            }

            return View();

        }
        public ActionResult validationView()
        {
            return View("GenerateIDNumber");
        }
        public ActionResult validateIDNumber()
        {

            return View();

        }
        public ActionResult ValidateExisting(string textIDNumber)
        {
            //No need for exception handling as the erro(s) will be bubbled up from the webAPI call.

            var idN = textIDNumber;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(GetControlDigitAPIHost());

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(GetControlDigitAPILocation() + idN).Result;
            char[] c = { '\\', '"' };
            if (response.IsSuccessStatusCode)
            {
                ViewBag.valid = (response.Content.ReadAsStringAsync().Result).Trim(c);

            }
            return View("GetControlDigit");

        }
        public string getIDNumberAPILocation()
        {
            string sSDirFilePath = string.Empty;

            if (ConfigurationManager.AppSettings.AllKeys.Contains("getIDNumberAPILocation"))
            {
                sSDirFilePath = ConfigurationManager.AppSettings["getIDNumberAPILocation"];
            }


            return sSDirFilePath;
        }

        public string getIDNumberAPIHost()
        {
            string sSDirFilePath = string.Empty;

            if (ConfigurationManager.AppSettings.AllKeys.Contains("getIDNumberAPIHost"))
            {
                sSDirFilePath = ConfigurationManager.AppSettings["getIDNumberAPIHost"];
            }


            return sSDirFilePath;
        }

        public string GetControlDigitAPILocation()
        {
            string sSDirFilePath = string.Empty;

            if (ConfigurationManager.AppSettings.AllKeys.Contains("GetControlDigitAPILocation"))
            {
                sSDirFilePath = ConfigurationManager.AppSettings["GetControlDigitAPILocation"];
            }


            return sSDirFilePath;
        }
        public string GetControlDigitAPIHost()
        {
            string sSDirFilePath = string.Empty;

            if (ConfigurationManager.AppSettings.AllKeys.Contains("getIDNumberAPILocation"))
            {
                sSDirFilePath = ConfigurationManager.AppSettings["getIDNumberAPIHost"];
            }


            return sSDirFilePath;
        }

        public string getLogFileDirectory()
        {
            string sSDirFilePath = string.Empty;

            if (!ConfigurationManager.AppSettings.AllKeys.Contains("LogFileDirectory"))
            {
                sSDirFilePath = ConfigurationManager.AppSettings["LogFileDirectory"];
            }

            return sSDirFilePath;
        }


    }
}
