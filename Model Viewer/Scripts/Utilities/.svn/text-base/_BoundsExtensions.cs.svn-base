using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class _BoundsExtensions
{

    public static bool IsNullOrEmpty(this Bounds bounds)
    {
        return bounds == null || bounds.size == Vector3.zero;
    }
    
    public static Bounds Bounds(this List<Vector3> vertices)
    {
        var min_x = vertices.Min(p => p.x);
        var max_x = vertices.Max(p => p.x);
        var min_y = vertices.Min(p => p.y);
        var max_y = vertices.Max(p => p.y);
        var min_z = vertices.Min(p => p.z);
        var max_z = vertices.Max(p => p.z);

        var min = new Vector3(min_x, min_y, min_z);
        var max = new Vector3(max_x, max_y, max_z);

        var center = (max + min) * 0.5F;
        var size = max - min;

        return new Bounds(center, size);
    }

    public static Bounds Bounds(this Vector3[] vertices)
    {
        var min_x = vertices.Min(p => p.x);
        var max_x = vertices.Max(p => p.x);
        var min_y = vertices.Min(p => p.y);
        var max_y = vertices.Max(p => p.y);
        var min_z = vertices.Min(p => p.z);
        var max_z = vertices.Max(p => p.z);

        var min = new Vector3(min_x, min_y, min_z);
        var max = new Vector3(max_x, max_y, max_z);

        var center = (max + min) * 0.5F;
        var size = max - min;

        return new Bounds(center, size);
    }

}
