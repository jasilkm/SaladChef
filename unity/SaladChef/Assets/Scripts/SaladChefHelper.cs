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

    public static Vector3 GetScreenPosition(float xPos, float yPos)
    {
        return Camera.main.WorldToScreenPoint(new Vector3(xPos, yPos, 0));
    }
    public static Vector3 GetWorldPosition(float xPos, float yPos)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(xPos, yPos, 0));
    }

    public static int[] Shuffle(int[] a)
    {
        // Loops through array
        for (int i = a.Length - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            int temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }

     

        return a;
    }

    private static Camera GetCamera()
    {
        Camera camera = Camera.main;

        if (camera == null)
        {
            camera = GameObject.Find("MainCamera").GetComponent<Camera>();
            Debug.Log(camera);
        }
        return camera;
    }
}
