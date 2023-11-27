namespace CodeBase.Services
{
    public class AllServices
    {
        private static AllServices _allServices;
        public static AllServices Container => _allServices ?? (_allServices = new AllServices());

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.Instance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.Instance;
        }

        private class Implementation<TService> where TService : IService
        {
            public static TService Instance;
        }
    }
}