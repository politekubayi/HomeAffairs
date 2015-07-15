using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewHomeAffairs.Models
{
    public class IDNumber
    {
        public Int32 dateOfBirth { get; set; }
        public Int32 gender { get; set; }
        public int countryIdentifier { get; set; }
        public int racialIdentifier { get; set; }
        public int checkBit { get; set; }
    }
}