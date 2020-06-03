using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SecurityBlock.Abstraction.Model
{
    [Flags]
    public enum SecurityAccessActionEnum
    {
        [Display(Name = "Read")]
        R = 0x1,
        [Display(Name = "Create")]
        C = 0x2,
        [Display(Name = "Update")]
        U = 0x4,
        [Display(Name = "Delete")]
        D = 0x8,
        [Display(Name = "Create with document")]
        IWD = 0x10,
        [Display(Name = "Update with document")]
        UWD = 0x20,
        [Display(Name = "Delete With document")]
        DWD = 0x40,
        [Display(Name = "Write (CUD)")]
        W = C | U | D,  //14  E
        [Display(Name = "Write with document (CUD)")]
        WWD = IWD | UWD | DWD,  //112  70

        [Display(Name = "Read/Write")]
        RW = R | W, //15 F
        [Display(Name = "Read/Write with document")]
        RWWD = R | WWD, //113 71
    }
   
}
