using Agate.MVC.Base;
using ProjectTA.Module.GameConstants;
using System;
using System.Collections.Generic;
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
        public List<string> LogMessages { get; set; } = new();

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
            analyticRecord.SessionId = GetFormattedSystemInfo();
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

        private string GetFormattedSystemInfo()
        {
            // Use StringBuilder for efficient string concatenation
            System.Text.StringBuilder infoBuilder = new System.Text.StringBuilder();

            // Add system information to the StringBuilder
            infoBuilder.AppendLine("=== SYSTEM INFORMATION ===");
            infoBuilder.AppendLine($"Device Model: {SystemInfo.deviceModel}");
            infoBuilder.AppendLine($"Device Name: {SystemInfo.deviceName}");
            infoBuilder.AppendLine($"Device Type: {SystemInfo.deviceType}");
            infoBuilder.AppendLine($"Processor Type: {SystemInfo.processorType}");
            infoBuilder.AppendLine($"Processor Count: {SystemInfo.processorCount}");
            infoBuilder.AppendLine($"Processor Frequency: {SystemInfo.processorFrequency} MHz");
            infoBuilder.AppendLine($"System Memory Size: {SystemInfo.systemMemorySize} MB");
            infoBuilder.AppendLine($"Graphics Device Name: {SystemInfo.graphicsDeviceName}");
            infoBuilder.AppendLine($"Graphics Device Type: {SystemInfo.graphicsDeviceType}");
            infoBuilder.AppendLine($"Graphics Memory Size: {SystemInfo.graphicsMemorySize} MB");
            infoBuilder.AppendLine($"Operating System: {SystemInfo.operatingSystem}");
            infoBuilder.AppendLine($"Battery Level: {SystemInfo.batteryLevel * 100:F1}%");
            infoBuilder.AppendLine($"Battery Status: {SystemInfo.batteryStatus}");
            infoBuilder.AppendLine($"Screen Resolution: {Screen.currentResolution.width}x{Screen.currentResolution.height}");
            infoBuilder.AppendLine($"Supported Resolutions:");
            foreach (var resolution in Screen.resolutions)
            {
                infoBuilder.AppendLine($"  - {resolution.width}x{resolution.height} @ {resolution.refreshRate} Hz");
            }

            // Return the formatted string
            return infoBuilder.ToString();
        }
    }
}