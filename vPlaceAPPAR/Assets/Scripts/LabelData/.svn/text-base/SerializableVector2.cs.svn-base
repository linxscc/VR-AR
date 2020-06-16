/*
	   信息：2017/6/29
       序列化Vector2数据
*/
using UnityEngine;
using System.Collections;
using System;

namespace PlaceAR.LabelDatas
{
    [Serializable]
    public class SerializableVector2
    {
        /// <summary>
        /// x component
        /// </summary>
        public float x;

        /// <summary>
        /// y component
        /// </summary>
        public float y;

        public SerializableVector2() : this(0, 0) { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="rZ"></param>
        public SerializableVector2(float rX, float rY)
        {
            x = rX;
            y = rY;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[{0}, {1}]", x, y);
        }

        public static SerializableVector2 operator +(SerializableVector2 a, SerializableVector2 b)
        {
            return new SerializableVector2(a.x + b.x, a.y + b.y);
        }

        public static SerializableVector2 operator -(SerializableVector2 a, SerializableVector2 b)
        {
            return new SerializableVector2(a.x - b.x, a.y - b.y);
        }

        public static SerializableVector2 operator *(float d, SerializableVector2 a)
        {
            return new SerializableVector2(d * a.x, d * a.y);
        }

        public static SerializableVector2 operator /(SerializableVector2 a, float d)
        {
            return new SerializableVector2(a.x / d, a.y / d);
        }

        public double magnitude
        {
            get
            {
                return Math.Sqrt(x * x + y * y);
            }
        }


        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Vector2(SerializableVector2 rValue)
        {
            return new Vector3(rValue.x, rValue.y);
        }

        public static implicit operator Vector3(SerializableVector2 rValue)
        {
            return new Vector3(rValue.x, rValue.y, 0);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator SerializableVector2(Vector2? rValue)
        {
            var value = rValue ?? Vector2.zero;
            return new SerializableVector2(value.x, value.y);
        }

        public static implicit operator SerializableVector2(Vector3? rValue)
        {
            var value = rValue ?? Vector2.zero;
            return new SerializableVector2(value.x, value.y);
        }

        /// <summary>
        /// SerializableVector2 => SerializableVector3
        /// </summary>
        /// <param name="rValue"></param>
        public static implicit operator SerializableVector3(SerializableVector2 rValue)
        {
            return new SerializableVector3(rValue.x, rValue.y, 0);
        }
    }
}
