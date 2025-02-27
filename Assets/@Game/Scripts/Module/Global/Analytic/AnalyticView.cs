using Agate.MVC.Base;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

namespace ProjectTA.Module.Analytic
{
    public class AnalyticView : BaseView
    {
        [SerializeField, ReadOnly]
        private float _fpsUpdateInterval = 1.0f;

        private float _lastFpsUpdate = 0.0f;
        private int _frameCount = 0;
        private float _totalDeltaTime = 0.0f;
        private long _totalMemoryUsage = 0;
        private int _memorySamples = 0;

        private PerformanceMetricsData _performanceMetricsData = null;

        private UnityAction<PerformanceMetricsData> _onPerformanceMetrics;

        public float FpsUpdateInterval => _fpsUpdateInterval;

        private void Start()
        {
            if (_performanceMetricsData == null)
            {
                _performanceMetricsData = new PerformanceMetricsData();
            }
        }

        private void Update()
        {
            // Track frame count and delta time
            _frameCount++;
            _totalDeltaTime += Time.deltaTime;

            // Collect memory data
            int currentMemory = (int)(Profiler.GetMonoUsedSizeLong() / 1048576f);
            _totalMemoryUsage += currentMemory;
            _memorySamples++;

            // Update min and max memory
            _performanceMetricsData.MinMemory = Math.Min(_performanceMetricsData.MinMemory, currentMemory);
            _performanceMetricsData.MaxMemory = Math.Max(_performanceMetricsData.MaxMemory, currentMemory);

            // Update FPS and log metrics every interval
            if (Time.time - _lastFpsUpdate > _fpsUpdateInterval)
            {
                _performanceMetricsData.AverageFps = (int)(_frameCount / _totalDeltaTime);

                // Update min and max FPS
                _performanceMetricsData.MinFps = Math.Min(_performanceMetricsData.MinFps, _performanceMetricsData.AverageFps);
                _performanceMetricsData.MaxFps = Math.Max(_performanceMetricsData.MaxFps, _performanceMetricsData.AverageFps);

                // Calculate average memory usage
                _performanceMetricsData.AverageMemory = (int)(_totalMemoryUsage / _memorySamples);

                _onPerformanceMetrics?.Invoke(_performanceMetricsData);

                // Reset counters for the next interval
                _frameCount = 0;
                _totalDeltaTime = 0.0f;
                _lastFpsUpdate = Time.time;
            }
        }

        public void SetFpsUpdateInterval(float interval)
        {
            _fpsUpdateInterval = interval;
        }

        public void SetCallback(UnityAction<PerformanceMetricsData> performanceMetrics)
        {
            _onPerformanceMetrics = performanceMetrics;
        }
    }
}