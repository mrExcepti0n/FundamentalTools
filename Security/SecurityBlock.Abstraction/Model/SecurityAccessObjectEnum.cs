using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBlock.Abstraction.Model
{
    public enum SecurityAccessObjectEnum
    {
        Agreement = 1,       
        Import = 2,
        Export = 3,
        Report = 4,
        PersonalData = 5,
        Administration = 6
    }
}
