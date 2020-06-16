using UnityEngine;
using System.Collections;
using DG.Tweening;

using System;
using PlaceAR;
using vPlace_zpc;

public class FaceToCameraHandler : MonoBehaviour
{
    const float perfectDistance = 0.5f;

    float duraction = 0.5f;
    public void Rotate(PrefabChildControl position, TweenCallback OnComplete)
    {
        HandleOnRotate(position, OnComplete);
    }

    private void HandleOnRotate(PrefabChildControl rawLocalPosition, TweenCallback OnComplete)
    {
        float worldDis = 0;
        if (ModelControl.GetInstance().model.name.Equals("sxd"))
            worldDis = rawLocalPosition.transform.parent.localPosition.z * 0.2f;
        else
            worldDis = rawLocalPosition.transform.parent.localPosition.z * .5f;//将物体拉近距离相机一半的距离
        
        Tweener t = transform.DOLocalMove(
            -new Vector3(rawLocalPosition.transform.localPosition.x * transform.localScale.x * rawLocalPosition.transform.parent.localScale.x,
            rawLocalPosition.transform.localPosition.y * transform.localScale.x * rawLocalPosition.transform.parent.localScale.y, 
            rawLocalPosition.transform.localPosition.z * transform.localScale.x * rawLocalPosition.transform.parent.localScale.z + worldDis), 0.5f);

        t.OnComplete(OnComplete);

        //Vector3 modelPos = rawLocalPosition.transform.GetComponent<MeshFilter>().sharedMesh.vertices.Bounds().center;
        //var worldPosition = transform.InverseTransformPoint(rawLocalPosition.transform.localPosition);
        //var toQ = Quaternion.FromToRotation(worldPosition, Vector3.back);
        //var s = perfectDistance / worldDis;
        //Debug.Log("Perfect Away From Zero : " + s);
        //s = Mathf.Clamp(s, ScaleHandler.MinValue, ScaleHandler.MaxValue);
        //var toS = Vector3.one * s;// Vector3.Distance
        // Vector3 targetVector = new Vector3();
        //Tweener t= transform.DOLocalRotateQuaternion(toQ, 0.4f);
        // Tweener t = transform.DOLocalMove()
        //t.OnComplete(OnComplete);
        // transform.DOScale(1, 0.5f);
        //var lastTime = -0.25f;

        //while(lastTime < duraction )
        //{
        //    lastTime += Time.deltaTime;
        //    var factor = lastTime / duraction;
        //    if ( factor > 1f ) factor = 1f;

        //    transform.rotation = Quaternion.Lerp ( fromQ, toQ, factor );
        //    transform.localScale = Vector3.Lerp ( fromS, toS, factor );

        //    yield return new WaitForFixedUpdate ( );
        //}
    }
}
