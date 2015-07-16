
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
            };

            String strIdNumber = string.Empty;
            strIdNumber = (strIdNumber + idNumber.dateOfBirth) + idNumber.gender + idNumber.countryIdentifier + idNumber.racialIdentifier;
            strIdNumber = strIdNumber + (GetControlDigit(strIdNumber));
            return strIdNumber;
        }

        [HttpGet]
        public string validateIDNumber(string id)
        {
            char[] idNCharArray = id.ToCharArray();
            int oddNumber = 0;
            Int32 intEven = 0;
            Int64 intIdN = 0;
            string strIdN = "";
            string even = string.Empty;
            for (int i = 0; i < idNCharArray.Length; i++)
            {
                if (((i + 1) % 2) != 0 && i != 12)
                {
                    oddNumber += int.Parse(idNCharArray[i] + "");
                }

            }

            for (int i = 0; i < idNCharArray.Length; i++)
            {
                if (((i + 1) % 2) == 0)
                {
                    even += idNCharArray[i];
                }

            }

            intEven = (Int32.Parse(even)) * 2;

            char[] evenChar = (intEven.ToString()).ToCharArray();
            intEven = 0;
            for (int i = 0; i < evenChar.Length; i++)
            {
                intEven += int.Parse(evenChar[i].ToString());

            }
            intIdN = oddNumber + intEven;
            strIdN = intIdN.ToString();

            intIdN = 10 - (Int64.Parse((strIdN.ElementAt((strIdN.Length - 1)).ToString())));

            if (intIdN >= 10)
            {
                intIdN = Int64.Parse((strIdN.ElementAt((strIdN.Length - 1)).ToString()));
            }

            if (intIdN == Int64.Parse(id.ElementAt((id.Length - 1)).ToString()))
            {

                return "ID Valid";
            }

            return "ID is not Valid";
        }


        private int GetControlDigit(string idNumber)
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
