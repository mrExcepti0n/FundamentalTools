using SecurityBlock.Abstraction.IdentityProvider;

namespace SecurityBlock
{
    public class IdentityProviderFactory
    {
        private static IIdentityProvider _instance = null;
        private static readonly object LockObject = new object();

        public static IIdentityProvider Get {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new IdentityProvider();
                        }
                    }
                }

                return _instance;
            }
        }

        public static void Init(IIdentityProvider identityProvider)
        {
            lock (LockObject)
            {
                _instance = identityProvider;
            }
        }
    }
}
