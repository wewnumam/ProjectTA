namespace ProjectTA.Module.Analytic
{
    public class PerformanceMetricsData
    {
        public int AverageFps { get; set; } = 0;
        public int MaxFps { get; set; } = int.MinValue;
        public int MinFps { get; set; } = int.MaxValue;
        public int AverageMemory { get; set; } = 0;
        public int MaxMemory { get; set; } = int.MinValue;
        public int MinMemory { get; set; } = int.MaxValue;
    }
}