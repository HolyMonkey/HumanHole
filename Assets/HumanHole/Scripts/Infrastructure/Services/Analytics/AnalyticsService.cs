using System.Collections.Generic;

namespace Infrastructure.Services.Analytics
{
    public class AnalyticsService: IAnalyticsService
    {
        public void SetEvent(AnalyticsEvents analyticsEvents) => 
            UnityEngine.Analytics.Analytics.CustomEvent($"{analyticsEvents}");

        public void SetEvent(AnalyticsEvents analyticsEvents, string key, object value) =>
            UnityEngine.Analytics.Analytics.CustomEvent($"{analyticsEvents}",new Dictionary<string, object> {{ key, value },});
    }
}
