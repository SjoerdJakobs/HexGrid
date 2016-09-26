using UnityEngine;

public static class Hexagon
{

    public const float radius = 10f;

    public const float innerRadius = radius * 0.866025404f;

    public static Vector3[] corners = {
        new Vector3(0f, 0f, radius),
        new Vector3(innerRadius, 0f, 0.5f * radius),
        new Vector3(innerRadius, 0f, -0.5f * radius),
        new Vector3(0f, 0f, -radius),
        new Vector3(-innerRadius, 0f, -0.5f * radius),
        new Vector3(-innerRadius, 0f, 0.5f * radius),
        new Vector3(0f, 0f, radius)
    };
}