using System;


namespace SecurityBlock.Abstraction.Model
{
    [Flags]
    public enum SecurityAccessActionEnum
    {
        R = 0x1,
        I = 0x2,
        U = 0x4,
        D = 0x8,
        IWD = 0x10,
        UWD = 0x20,
        DWD = 0x40,
        W = I | U | D,  //14  E
        WWD = IWD | UWD | DWD,  //112  70

        RW = R | W, //15 F
        RWWD = R | WWD, //113 71
        WD = IWD | UWD | DWD | WWD | RWWD
    }
   
}
