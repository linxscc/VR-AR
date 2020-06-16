using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ModelViewerProject.Animate
{
    using Label3D;

    public class AnimationController : MonoBehaviour
    {

        public List<AnimationHandler>  animas { set; get; }

        public void OnInit(List<LabelData> datas )
        {
            if ( animas == null )
                animas = new List<AnimationHandler> ( );
            else
                animas.Clear ( );

            var children = transform.GetComponentsInChildren<Transform>();

            foreach ( var data in datas )
            {
                var child = children.FirstOrDefault( t => t.name == data.name);
                if ( child )
                {

                    var anim = child.GetComponent<AnimationHandler>();
                    if ( anim == null )
                        anim = child.gameObject.AddComponent<AnimationHandler> ( );

                    anim.OnInit ( data.localPosition );

                    animas.Add ( anim );
                }
            }

        }
        public void OnAssemble ( )
        {
            foreach(var anim in animas )
            {
                anim.OnAssemble ( );
            }
        }

        public void OnDisassemble ( )
        {
            foreach ( var anim in animas )
            {
                anim.OnDisassemble ( );
            }
        }
    }
}
