
using System;
using UnityEngine;

public class Wave
{
    public float X;
    public float InitialAmplitude;
    public float T;
    public float Decay;
    public bool Done;
    public float Oscilator;

    public float ComputeValue(float x)
    {
        float distanceCoef = CalculateDistanceCoef(x);
        float A = distanceCoef * InitialAmplitude * Decay * Oscilator;
        return A * Mathf.Cos(x - X);
    }

    private float CalculateDistanceCoef(float x)
    {
        float distance = Mathf.Abs(x - X);
        float t = 2 * T;
        if (distance > t)
        {
            return 0;
        }
        else
        {
            return 1 - (distance / t) * (distance / t);
        }
    }
}
