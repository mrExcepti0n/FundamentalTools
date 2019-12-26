using SecurityBlock.Abstraction.Model;


namespace SecurityBlock.Abstraction
{
    public static class IdentityExtensions
    {
        public static bool LessThan(this SecurityAccessActionEnum accessRight, SecurityAccessActionEnum accessRule)
        {
            if (accessRight == SecurityAccessActionEnum.RW || accessRight == SecurityAccessActionEnum.W ||
                accessRight == SecurityAccessActionEnum.RWWD || accessRight == SecurityAccessActionEnum.WWD || 
                accessRight == accessRule)
            {
                return false;
            }

            return true;
        }

        //RW RWWD - false
        //RWWD RWWD - true
        //RWWD RW - true
        //RIWD RWWD - false
        //RIWD RUWD - false
        //RUWD RUWD - true
        public static bool NeedDocument(this SecurityAccessActionEnum accessRight)
        {
            if (accessRight == SecurityAccessActionEnum.RW || accessRight == SecurityAccessActionEnum.D ||
                accessRight == SecurityAccessActionEnum.I || accessRight == SecurityAccessActionEnum.U || accessRight == SecurityAccessActionEnum.W)
            {
                return false;
            }

            return true;
        }
    }
}
