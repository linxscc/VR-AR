/*
	   信息：Transform扩展
       2017/6/29
*/
using UnityEngine;
using System.Collections;
using PlaceAR.LabelDatas;

namespace PlaceAR
{
    public static class TransformExtensions
    {

        public static void Initial(this Transform aTransform)
        {
            aTransform.localPosition = Vector3.zero;
            aTransform.localRotation = Quaternion.identity;
            aTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        public static TransformData SaveLocal(this Transform aTransform)
        {
            return new TransformData()
            {
                position = aTransform.localPosition,
                rotation = aTransform.localRotation,
                localScale = aTransform.localScale
            };
        }
        public static TransformData SaveWorld(this Transform aTransform)
        {
            return new TransformData()
            {
                position = aTransform.position,
                rotation = aTransform.rotation,
                localScale = aTransform.localScale
            };
        }

        public static void LoadLocal(this Transform aTransform, TransformData aData)
        {
            aTransform.localPosition = aData.position;
            aTransform.localRotation = aData.rotation;
            aTransform.localScale = aData.localScale;
        }

        public static void LoadWorld(this Transform aTransform, TransformData aData)
        {
            aTransform.position = aData.position;
            aTransform.rotation = aData.rotation;
            aTransform.localScale = aData.localScale;
        }

        public static void CopyFrom(this Transform aTransform, Transform bTransform)
        {
            if (bTransform.parent)
            {

                aTransform.SetParent(bTransform.parent);
                aTransform.localPosition = bTransform.localPosition;
                aTransform.localRotation = bTransform.localRotation;
                aTransform.localScale = bTransform.localScale;
            }
            else
            {

                aTransform.position = bTransform.position;
                aTransform.rotation = bTransform.rotation;
                aTransform.localScale = bTransform.localScale;
            }
        }

        public static void CopyFrom(this Transform aTransform, GameObject Obj)
        {
            aTransform.CopyFrom(Obj.transform);
        }

        public static SerializableTransform Serialization(this Transform aTransform)
        {
            return new SerializableTransform()
            {
                Position = (SerializableVector3)aTransform.localPosition,
                Rotation = (SerializableVector3)aTransform.localEulerAngles,
                Scale = (SerializableVector3)aTransform.localScale,

            };
        }
        public static SerializableTransform Serialization(this Transform aTransform, string name,string chinaName)
        {
            return new SerializableTransform()
            {
                Position = (SerializableVector3)aTransform.localPosition,
                Rotation = (SerializableVector3)aTransform.localEulerAngles,
                Scale = (SerializableVector3)aTransform.localScale,
                Name = name,
                ChinaName = chinaName

            };
        }
        /*public static implicit operator SerializableTransform(Transform aTransform)
        {
            return new SerializableTransform()
            {
                Position = (SerializableVector3)aTransform.localPosition,
                Rotation = (SerializableVector3)aTransform.localEulerAngles,
                Scale = (SerializableVector3)aTransform.localScale
            };
        }*/

    }

    public class TransformData
    {

        public static TransformData identity
        {
            get
            {
                return new TransformData();
            }
        }

        public static TransformData Lerp(TransformData from, TransformData to, float factor)
        {
            var pos = Vector3.Lerp(from.position, to.position, factor);
            var rot = Quaternion.Lerp(from.rotation, to.rotation, factor);
            var sca = Vector3.Lerp(from.localScale, to.localScale, factor);
            return new TransformData(pos, rot, sca);
        }

        public TransformData() : this(Vector3.zero, Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f)) { }

        public TransformData(Vector3 pos, Quaternion rot, Vector3 sca)
        {
            position = pos;
            rotation = rot;
            localScale = new Vector3(0.4f, .4f, .4f);

        }

        public Vector3 position { set; get; }
        public Quaternion rotation { set; get; }
        public Vector3 localScale { set; get; }
    }

}
