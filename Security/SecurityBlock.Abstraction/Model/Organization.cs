using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBlock.Abstraction.Model
{
    public class Organization
    {
        public Organization(string claimValue)
        {
            var splittedResult = claimValue.Split(',');

            OGRN = splittedResult[0];
            Name = splittedResult[1];
        }

        public string OGRN { get; set; }
        public string Name { get; set; }
    }
}
