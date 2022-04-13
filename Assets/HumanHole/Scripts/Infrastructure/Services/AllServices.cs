public class AllServices
{
    public void RegisterSingle<TService>(TService implementation) where TService : IService =>
        Implementation<TService>.RegisterSingle(implementation);

    public TService Single<TService>() where TService : IService =>
        Implementation<TService>.Single();

    private static class Implementation<TService> where TService : IService
    {
        private static TService _serviceInstance;

        public static void RegisterSingle(TService implementation) =>
            _serviceInstance = implementation;

        public static TService Single() =>
            _serviceInstance;
    }
}