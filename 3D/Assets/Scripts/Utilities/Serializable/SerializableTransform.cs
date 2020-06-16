using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SerializableTransform
{
    //Basic Property
    public SerializableVector3 Position { set; get; }

    public SerializableVector3 Scale { set; get; }
    public SerializableVector3 Rotation { set; get; }
    /// <summary>
    /// 模型名称
    /// </summary>
    public string LocalName { set; get; }
    /// <summary>
    /// 中文名称
    /// </summary>
    public string Name { set; get; }

    public static implicit operator SerializableTransform ( Transform rValue )
    {
        return rValue.Serialization ( );
    }
    

    public override string ToString ( )
    {
        return string.Format ( "{{Position : {0}, Rotation : {1}, Scale : {2}}}", Position.ToString ( ), Rotation.ToString ( ), Scale.ToString ( ) );
    }
}
