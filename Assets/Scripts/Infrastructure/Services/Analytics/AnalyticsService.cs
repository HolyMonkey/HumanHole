using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Infrastructure.Services.Analytics
{
    public class AnalyticsService: IAnalyticsService
    {
        public void SetEvent(AnalyticsEvents analyticsEvents)
        {
           AnalyticsResult analyticsResult = UnityEngine.Analytics.Analytics.CustomEvent($"{analyticsEvents}");
           Debug.Log($"AnalyticsResult {analyticsResult}");
        }
        
        public void SetEvent(AnalyticsEvents analyticsEvents, string key, object value)
        {
            AnalyticsResult analyticsResult = UnityEngine.Analytics.Analytics.CustomEvent($"{analyticsEvents}",new Dictionary<string, object>
            {
                { key, value },
            });
            Debug.Log($"AnalyticsResult {analyticsResult}");
        }
    }
}
