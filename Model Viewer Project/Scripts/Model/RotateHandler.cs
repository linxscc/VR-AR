using UnityEngine;
using System.Collections;

namespace ModelViewerProject.Model
{

    public class RotateHandler : MonoBehaviour
    {


        bool _needsStatic = true;
        float _rotateSpeed = 32F;

        public void StartRotate ( Vector2 dir )
        {
            //_rotateDir = dir;
            StartCoroutine ( HandleOnRotate ( dir ) );
        }

        public void StopRotate ()
        {
            _needsStatic = true;
        }
        public void PrefabRotate(Vector2 dir)
        {
            transform.Translate(dir);
            //transform.localRotation *= Quaternion.AngleAxis(dir.x, transform.InverseTransformDirection(Vector3.up))
            // * Quaternion.AngleAxis(dir.y, transform.InverseTransformDirection(Vector3.right));
        }
        IEnumerator HandleOnRotate ( Vector2 dir )
        {
            var _rotateDir = dir;
            _needsStatic = false;

            while ( !_needsStatic )
            {
                float rotX = -_rotateDir.x * _rotateSpeed * Time.deltaTime;
                float rotY = _rotateDir.y * _rotateSpeed  * Time.deltaTime;

                transform.localRotation *= Quaternion.AngleAxis ( rotX, transform.InverseTransformDirection ( Vector3.up ) )
                    * Quaternion.AngleAxis ( rotY, transform.InverseTransformDirection ( Vector3.right ) );

                yield return new WaitForFixedUpdate ( );
            }
            yield return null;
        }
        
    }
}