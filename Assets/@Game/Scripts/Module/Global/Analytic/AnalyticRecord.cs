using System;
using UnityEngine;

namespace ProjectTA.Module.Analytic
{
    [Serializable]
    public class AnalyticRecord
    {
        [field: SerializeField] public string SessionId { get; set; }
        [field: SerializeField] public string DeviceId { get; set; }
        [field: SerializeField] public string ScreenTimeInSeconds { get; set; }
        [field: SerializeField] public string AverageFps { get; set; }
        [field: SerializeField] public string MaxFps { get; set; }
        [field: SerializeField] public string MinFps { get; set; }
        [field: SerializeField] public string AverageMemory { get; set; }
        [field: SerializeField] public string MaxMemory { get; set; }
        [field: SerializeField] public string MinMemory { get; set; }
        [field: SerializeField] public string LogWarningCount { get; set; }
        [field: SerializeField] public string LogErrorCount { get; set; }
        [field: SerializeField] public string LogExceptionCount { get; set; }
    }
}