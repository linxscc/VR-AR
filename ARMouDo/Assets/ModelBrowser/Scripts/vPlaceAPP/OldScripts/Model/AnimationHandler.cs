﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using PlaceAR.LabelDatas;
using PlaceAR;

public class AnimationHandler : MonoBehaviour
{

    Vector3 Position;
    Vector3 initialPosition;
    private Vector3 rotation;
    private Transform parent;
    public void OnInit(Vector3 pos)
    {
        parent = transform.parent;
        Position = GetComponent<PrefabChildControl>().LocalPosition;
        initialPosition = GetComponent<PrefabChildControl>().InitialPosition;
        rotation = transform.localEulerAngles;
    }
    /// <summary>
    /// 分
    /// </summary>
    public void OnAssemble()
    {
        HandleOnAnimate(false);
    }
    /// <summary>
    /// 合
    /// </summary>
    public void OnDisassemble()
    {
        HandleOnAnimate(true);
    }

    bool isAnimating = false;
    float duraction = 0.5f;
    void HandleOnAnimate(bool isDisassemble)
    {
        transform.parent = parent;
        //transform.rotation = rotation;
        // var from = isDisassemble ? initialPosition : Position;
        var to = isDisassemble ? Position : initialPosition;
        transform.localEulerAngles = isDisassemble ? transform.localEulerAngles : rotation;
        // if(transform.name=="001")
        // print(transform.name+"=="+ Position+"=="+ initialPosition);
        // to = transform.TransformDirection(to);
        transform.DOLocalMove(to, 0.5f);
        // Tweener twee;
        // twee.OnComplete();
        //transform.localPosition = Vector3.Lerp(from, to, 0.5f);

    }
    //IEnumerator HandleOnAnimate ( bool isDisassemble )
    //{

    //    var lastTime = -0.5f;

    //    var from = transform.localPosition;
    //    var to = isDisassemble ? Position : Vector3.zero;

    //    while ( lastTime < duraction )
    //    {
    //        lastTime += Time.deltaTime;
    //        var factor = lastTime / duraction;
    //        if ( factor > 1f ) factor = 1f;

    //        transform.localPosition = Vector3.Lerp ( from, to, factor );

    //        yield return new WaitForFixedUpdate ( );
    //    }

    //    yield return null;
    //}

}