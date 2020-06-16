using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SerializableVector4
{
    /// <summary>
    /// x component
    /// </summary>
    public float x;

    /// <summary>
    /// y component
    /// </summary>
    public float y;

    /// <summary>
    /// z component
    /// </summary>
    public float z;

    /// <summary>
    /// 
    /// </summary>
    public float w;

    public SerializableVector4() : this(1, 1, 1, 1) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rR"></param>
    /// <param name="rG"></param>
    /// <param name="rB"></param>
    public SerializableVector4(float rR, float rG, float rB) : this(rR, rG, rB, 1) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rR"></param>
    /// <param name="rG"></param>
    /// <param name="rB"></param>
    /// <param name="rA"></param>
    public SerializableVector4(float rR, float rG, float rB, float rA)
    {
        x = rR;
        y = rG;
        z = rB;
        w = rA;
    }

    public static SerializableVector4 operator +(SerializableVector4 a, SerializableVector4 b)
    {
        return new SerializableVector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    }

    public static SerializableVector4 operator -(SerializableVector4 a, SerializableVector4 b)
    {
        return new SerializableVector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    }

    public static SerializableVector4 operator *(float d, SerializableVector4 a)
    {
        return new SerializableVector4(d * a.x, d * a.y, d * a.z, d * a.w);
    }

    public static SerializableVector4 operator /(SerializableVector4 a, float d)
    {
        return new SerializableVector4(a.x / d, a.y / d, a.z / d, a.w / d);

    }
    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }

    /// <summary>
    /// Automatic conversion from SerializableVector4 to Vector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Vector4(SerializableVector4 rValue)
    {
        return new Vector4(rValue.x, rValue.y, rValue.z, rValue.w);
    }

    /// <summary>
    /// Automatic conversion from Vector3 to SerializableVector4
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableVector4(Vector4? rValue)
    {
        var value = rValue ?? Vector4.zero;
        return new SerializableVector4(value.x, value.y, value.z, value.w);
    }


}

