using Agate.MVC.Base;
using ProjectTA.Module.GameConstants;
using System;
using UnityEngine;

namespace ProjectTA.Module.Analytic
{
    public class AnalyticModel : BaseModel
    {
        public bool IsAppQuit { get; private set; } = false;
        public AnalyticFormConstants AnalyticFormConstants { get; private set; } = null;
        public int ScreenTimeInSeconds { get; private set; } = 0;
        public PerformanceMetricsData PerformanceMetricsData { get; private set; } = null;
        public int LogWarningCount { get; private set; } = 0;
        public int LogErrorCount { get; private set; } = 0;
        public int LogExceptionCount { get; private set; } = 0;

        public void SetScreenTimeInSeconds(float screenTimeInSeconds)
        {
            ScreenTimeInSeconds = (int)screenTimeInSeconds;
        }

        public void SetPerformanceMetrics(PerformanceMetricsData performanceMetricsData)
        {
            PerformanceMetricsData = performanceMetricsData;
        }

        public void AddLogCounter(LogType type)
        {
            if (type == LogType.Warning)
            {
                LogWarningCount++;
            }
            else if (type == LogType.Error)
            {
                LogErrorCount++;
            }
            else if (type == LogType.Exception)
            {
                LogExceptionCount++;
            }
        }

        public void SetIsAppQuit(bool isAppQuit)
        {
            IsAppQuit = isAppQuit;
        }

        public void SetAnalyticFormConstants(AnalyticFormConstants analyticFormConstants)
        {
            AnalyticFormConstants = analyticFormConstants;
        }

        public AnalyticRecord GetAnalyticRecord()
        {
            AnalyticRecord analyticRecord = new AnalyticRecord();
            analyticRecord.SessionId = Guid.NewGuid().ToString();
            analyticRecord.DeviceId = SystemInfo.deviceUniqueIdentifier;
            analyticRecord.ScreenTimeInSeconds = ScreenTimeInSeconds.ToString();
            if (PerformanceMetricsData != null)
            {
                analyticRecord.AverageFps = PerformanceMetricsData.AverageFps.ToString();
                analyticRecord.MaxFps = PerformanceMetricsData.MaxFps.ToString();
                analyticRecord.MinFps = PerformanceMetricsData.MinFps.ToString();
                analyticRecord.AverageMemory = PerformanceMetricsData.AverageMemory.ToString();
                analyticRecord.MaxMemory = PerformanceMetricsData.MaxMemory.ToString();
                analyticRecord.MinMemory = PerformanceMetricsData.MinMemory.ToString();
            }
            analyticRecord.LogWarningCount = LogWarningCount.ToString();
            analyticRecord.LogErrorCount = LogErrorCount.ToString();
            analyticRecord.LogExceptionCount = LogExceptionCount.ToString();
            return analyticRecord;
        }
    }
}