﻿using System.Threading;
using NUnit.Framework;
using Unity.PerformanceTesting;

public class MeasureTimerTests
{
    private static int s_CallCount;

    [Test, Performance]
    public void MeasureMethod_Run()
    {
        Measure.Method(() => { Thread.Sleep(1); }).Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups.Count, 1);
        Assert.IsTrue(AllSamplesHigherThan0(test));
    }

    [Test, Performance]
    public void MeasureMethod_MeasurementCount_Run()
    {
        s_CallCount = 0;
        Measure.Method(() => { s_CallCount++; })
            .MeasurementCount(10)
            .Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups[0].Samples.Count, 10);
        Assert.AreEqual(10, s_CallCount);
    }

    [Test, Performance]
    public void MeasureMethod_SetupCleanup_Run()
    {
        s_CallCount = 0;
        Measure.Method(() => { s_CallCount++; })
            .MeasurementCount(10)
            .SetUp(() => s_CallCount++)
            .CleanUp(() => s_CallCount++)
            .Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups[0].Samples.Count, 10);
        Assert.AreEqual(30, s_CallCount);
    }
    
    [Test, Performance]
    public void MeasureMethod_Setup_Run()
    {
        s_CallCount = 0;
        Measure.Method(() => { })
            .MeasurementCount(10)
            .SetUp(() => s_CallCount++)
            .Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups[0].Samples.Count, 10);
        Assert.AreEqual(10, s_CallCount);
    }
    
    [Test, Performance]
    public void MeasureMethodCleanup_Run()
    {
        s_CallCount = 0;
        Measure.Method(() => { })
            .MeasurementCount(10)
            .CleanUp(() => s_CallCount++)
            .Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups[0].Samples.Count, 10);
        Assert.AreEqual(10, s_CallCount);
    }

    [Test, Performance]
    public void MeasureMethod_MeasurementAndIterationCount_Run()
    {
        s_CallCount = 0;
        Measure.Method(() => { s_CallCount++; })
            .WarmupCount(10)
            .MeasurementCount(10)
            .IterationsPerMeasurement(5)
            .Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups[0].Samples.Count, 10);
        Assert.AreEqual(60, s_CallCount);
    }

    [Test, Performance]
    public void MeasureMethod_GC_Run()
    {
        Measure.Method(() => { }).GC().Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups.Count, 2);
        Assert.AreEqual(test.SampleGroups[0].Name, "Time.GC()");
        Assert.IsTrue(LessThanOne(test.SampleGroups[0]));
    }

    [Test, Performance]
    public void MeasureMethod_WithEmptyMarkers_Run()
    {
        Measure.Method(() => { }).ProfilerMarkers("empty").Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups.Count, 1);
    }

    [Test, Performance]
    public void MeasureMethod_WithMarkers_Run()
    {
        Measure.Method(() => { MeasureProfilerSamplesTests.CreatePerformanceMarker("loop", 1); })
            .ProfilerMarkers("loop").Run();

        var test = PerformanceTest.Active;
        Assert.AreEqual(test.SampleGroups.Count, 2);
    }

    private static bool AllSamplesHigherThan0(PerformanceTest test)
    {
        foreach (var sampleGroup in test.SampleGroups)
        {
            foreach (var sample in sampleGroup.Samples)
            {
                if (sample <= 0) return false;
            }
        }

        return true;
    }

    private static bool LessThanOne(SampleGroup sampleGroup)
    {
        foreach (var sample in sampleGroup.Samples)
        {
            if (sample > 1f)
            {
                return false;
            }
        }

        return true;
    }
}