using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 SetVec(this Vector3 vec, float x = float.NaN, float y = float.NaN, float z = float.NaN)
    {
        if (!float.IsNaN(vec.x))
            vec.x = x;
        if (!float.IsNaN(vec.y))
            vec.y = y;
        if (!float.IsNaN(vec.z))
            vec.z = z;

        return vec;
    }
}
