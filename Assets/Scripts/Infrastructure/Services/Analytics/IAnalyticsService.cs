namespace Infrastructure.Services.Analytics
{
    public interface IAnalyticsService : IService
    {
        void SetEvent(AnalyticsEvents analyticsEvents);
        void SetEvent(AnalyticsEvents analyticsEvents, string key, object value);
    }
}
