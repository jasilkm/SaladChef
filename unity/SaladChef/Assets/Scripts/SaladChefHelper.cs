using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaladChefHelper 
{
    public static float NormalizedValue(float value, float min, float max, float minX, float maxX)
    {
        return ((value - min) * (maxX - minX)) / (max - min) + minX;
    }

    public static float DeNormalizedValue(float value, float min, float max, float minX, float maxX)
    {
        return (((min - max) * value - (maxX * min)) + (max * minX)) / (minX - maxX);
    }
}
