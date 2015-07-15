
using NewHomeAffairs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class IDNumberController : ApiController
    {
        IDNumber[] idNumber = new IDNumber[]{
            new IDNumber{dateOfBirth=900502,gender=5838,countryIdentifier=0,racialIdentifier=8,checkBit=7}
        };

        [HttpGet]
        public string getIDNumber()
        {

            return generateValidIDNumber();
        }

        public string generateValidIDNumber()
        {
            //No need for a try catch

            DateTime time = DateTime.Now;
            String format = "yyMMdd";
            var t = time.ToString(format);
            Random rnd = new Random();

            IDNumber idNumber = new IDNumber()
            {
                dateOfBirth = Int32.Parse(t),
                gender = rnd.Next(1000, 10000),
                countryIdentifier = rnd.Next(0, 2),
                racialIdentifier = rnd.Next(8, 10),
                checkBit = rnd.Next(1, 10)
            };

            return ("" + idNumber.dateOfBirth + idNumber.gender + idNumber.countryIdentifier + idNumber.racialIdentifier + idNumber.checkBit);
        }
        
        [HttpGet]
        public int GetControlDigit(string idNumber)
        {
            int d = -1;

            //Putting try catch in order to log any number exception. incase someone pass a string.
            try
            {
                int a = 0;
                for (int i = 0; i < 6; i++)
                {
                    a += int.Parse(idNumber[2 * i].ToString());
                }
                int b = 0;
                for (int i = 0; i < 6; i++)
                {
                    b = b * 10 + int.Parse(idNumber[2 * i + 1].ToString());
                }
                b *= 2;
                int c = 0;
                do
                {
                    c += b % 10;
                    b = b / 10;
                }
                while (b > 0);
                c += a;
                d = 10 - (c % 10);
                if (d == 10) d = 0;
            }
            catch (Exception e) 
            {
                string logFilePath = string.Empty;

                if (ConfigurationManager.AppSettings.AllKeys.Contains("LogFileDirectory"))
                {
                    logFilePath = ConfigurationManager.AppSettings["LogFileDirectory"];
                }

                

                if (!File.Exists(logFilePath))
                {
                    File.WriteAllText(logFilePath, DateTime.Now + " " + e.Message + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(logFilePath, DateTime.Now + " " + e.Message + Environment.NewLine);
                }


            
            } 
            
            return d;
        }

    }


}
