using UnityEngine;
using System.Collections;
using DG.Tweening;
namespace ModelViewerProject.Model {

    using System;
    using Label3D;

    public class FaceToCameraHandler : MonoBehaviour {

        public event EventHandler EndRotate;

        const float perfectDistance = 0.5f;
        
        public void Rotate (Label3DHandler position , TweenCallback OnComplete)
        {
           // target = obj;
         HandleOnRotate ( position, OnComplete) ;
        }

        float duraction = 0.5F;


        private void HandleOnRotate (Label3DHandler rawLocalPosition,TweenCallback OnComplete)
        {
            Vector3 modelPos= rawLocalPosition.transform.GetComponent<MeshFilter>().sharedMesh.vertices.Bounds().center;
            var worldPosition = transform.InverseTransformPoint(rawLocalPosition.transform.localPosition);
            var toQ = Quaternion.FromToRotation(worldPosition, Vector3.back);
            var worldDis = Vector3.Distance(worldPosition, Vector3.zero);        
            var s =  perfectDistance / worldDis;
            s = Mathf.Clamp ( s, ScaleHandler.MinValue, ScaleHandler.MaxValue );
            var toS = Vector3.one * s;
            Tweener t = transform.DOLocalMove(-new Vector3(rawLocalPosition.transform.localPosition.x*transform.localScale.x, 
                rawLocalPosition.transform.localPosition.y* transform.localScale.x, rawLocalPosition.transform.localPosition.z* transform.localScale.x + 2.9f), 0.5f);
            t.OnComplete(OnComplete);
            // Vector3.Distance
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

            if ( EndRotate != null )
                EndRotate ( this, EventArgs.Empty );

          

        }
    }
}
