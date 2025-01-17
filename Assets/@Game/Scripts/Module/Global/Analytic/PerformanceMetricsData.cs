namespace ProjectTA.Module.Analytic
{
    public class PerformanceMetricsData
    {
        public float AverageFps { get; set; } = 0;
        public float MaxFps { get; set; } = float.MinValue;
        public float MinFps { get; set; } = float.MaxValue;
        public long AverageMemory { get; set; } = 0;
        public long MaxMemory { get; set; } = long.MinValue;
        public long MinMemory { get; set; } = long.MaxValue;
    }
}