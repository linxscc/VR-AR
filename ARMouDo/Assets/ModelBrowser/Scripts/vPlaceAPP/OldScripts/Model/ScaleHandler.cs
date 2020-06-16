using UnityEngine;
using System.Collections;
using System;

    public class ScaleHandler : MonoBehaviour
    {

        public static event EventHandler OnOverflowMax;
        public static event EventHandler OnOverflowMin;

        public static float MaxValue = 1.75f;
        public static float MinValue = 0.5F;

        bool _needsStatic = true;
        float _scaleSpeed = 0.5F;

        public float Value
        {
            get
            {
                return transform.localScale.x;
            }
        }

        bool IsGreaterThanMax { get { return Value >= MaxValue; } }
        bool IsLessThanMin { get { return Value <= MinValue; } }


        public void StartScale ( int dir )
        {
            StartCoroutine ( HandleOnScale ( dir ) );
        }

        public void StopScale ( )
        {
            _needsStatic = true;
        }

        IEnumerator HandleOnScale ( int dir )
        {

            var scaleDir = dir;
            _needsStatic = false;

            while ( !_needsStatic )
            {

                var delta = Vector3.one * scaleDir * _scaleSpeed * Time.deltaTime;

                transform.localScale += delta;

                if ( IsGreaterThanMax )
                {
                    _needsStatic = true;
                    if ( OnOverflowMax != null )
                        OnOverflowMax ( this, System.EventArgs.Empty );
                }

                if ( IsLessThanMin )
                {
                    _needsStatic = true;
                    if ( OnOverflowMin != null )
                        OnOverflowMin ( this, System.EventArgs.Empty );
                }

                yield return new WaitForFixedUpdate ( );
            }

            yield return null;
        }

    }
