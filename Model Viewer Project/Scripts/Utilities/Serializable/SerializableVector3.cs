using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SerializableVector3
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

    public SerializableVector3() : this(0, 0, 0)
    {

    }

    public SerializableVector3(float rX, float rY) : this(rX, rY, 0) { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="rX"></param>
    /// <param name="rY"></param>
    /// <param name="rZ"></param>
    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    public static SerializableVector3 operator +(SerializableVector3 a, SerializableVector3 b)
    {
        return new SerializableVector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static SerializableVector3 operator -(SerializableVector3 a, SerializableVector3 b)
    {
        return new SerializableVector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static SerializableVector3 operator *(float d, SerializableVector3 a)
    {
        return new SerializableVector3(d * a.x, d * a.y, d*a.z);
    }

    public static SerializableVector3 operator /(SerializableVector3 a, float d)
    {
        return new SerializableVector3( a.x / d,  a.y/ d, a.z / d);

    }
    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// <summary>
    /// Automatic conversion from SerializableVector3 to Vector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        Vector3 v3;
        try
        {
            v3 = new Vector3(rValue.x, rValue.y, rValue.z);
        }
        catch (Exception)
        {
            Debug.LogError(rValue);
            throw;
        }
        return v3;
    }

    public static implicit operator Vector2(SerializableVector3 rValue)
    {
        return new Vector2(rValue.x, rValue.y);
    }

    /// <summary>
    /// Automatic conversion from Vector3 to SerializableVector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableVector3(Vector3? rValue)
    {
        var value = rValue ?? Vector3.zero;
        return new SerializableVector3(value.x, value.y, value.z);
    }
    
    public static implicit operator SerializableVector3(Vector2? rValue)
    {
        var value = rValue ?? Vector3.zero;
        return new SerializableVector3(value.x, value.y, 0);
    }

    /// <summary>
    /// SerializableVector3 => SerializableVector2
    /// </summary>
    /// <param name="rValue"></param>
    public static implicit operator SerializableVector2(SerializableVector3 rValue)
    {
        return new SerializableVector2(rValue.x, rValue.y);
    }

    
}

