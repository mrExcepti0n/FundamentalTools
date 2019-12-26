using SecurityBlock.Abstraction.IdentityProvider;

namespace SecurityBlock.IdentityProvider
{
    public class IdentityProviderFactory
    {
        private static IIdentityProvider instance = null;
        private static readonly object _lockObject = new object();

        public static IIdentityProvider Get {
            get
            {
                lock (_lockObject)
                {
                    if (instance == null)
                    {
                        instance = new IdentityProvider();              
                    }
                }
                return instance;
            }
        }

        public static void Init(IIdentityProvider identityProvider)
        {
            lock (_lockObject)
            {
                instance = identityProvider;
            }
        }
    }
}
