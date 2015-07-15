using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Headers;
using System.Net.Http;

namespace TestSolutions
{
    [TestClass]
    public class IDNumberFunctionalityTest
    {
       [TestMethod]
        public void GetIDNumber()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:51338/");

              client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
              HttpResponseMessage response = client.GetAsync("api/idnumber").Result;

             char[] c = { '\\', '"' };

                if (response.IsSuccessStatusCode)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail("Failed to get id number.");

                }

            }

       [TestMethod]
       public void validateIDNumber()
       {
           HttpClient client = new HttpClient();
           client.BaseAddress = new Uri("http://localhost:51338/");

           client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));
           HttpResponseMessage response = client.GetAsync("api/IDNumber?idNumber="+8905027838087).Result;

           char[] c = { '\\', '"' };

           if (response.IsSuccessStatusCode)
           {
               Assert.IsTrue(true);
              

           }
           else
           {
               Assert.Fail("Failed to validate id number.");

           }

       }
        }

    
}
