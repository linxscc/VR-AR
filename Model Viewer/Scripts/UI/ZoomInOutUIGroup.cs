using UnityEngine;
using UnityEngine.UI;
using System;

namespace ModelViewerProject.UI
{

    public class ZoomInOutUIGroup : MonoBehaviour
    {

        public LongTapButton ZoomInButton;
        public LongTapButton ZoomOutButton;

        public event EventHandler BeginZoomIn;
        public event EventHandler EndZoomIn;

        public event EventHandler BeginZoomOut;
        public event EventHandler EndZoomOut;

        public void DisableZoomIn ( )
        {
            ZoomInButton.interactable = false;
        }


        public void DisableZoomOut ( )
        {
            ZoomOutButton.interactable = false;
        }

        public void EnableAll ( )
        {
            EnableZoomIn ( );
            EnableZoomOut ( );
        }

        void EnableZoomIn ( )
        {
            if ( !ZoomInButton.interactable )
                ZoomInButton.interactable = true;
        }
        void EnableZoomOut ( )
        {
            if ( !ZoomOutButton.interactable )
                ZoomOutButton.interactable = true;
        }

        public void StartZoomIn ( )
        {
            EnableZoomOut ( );
            if ( BeginZoomIn != null )
            {
                //print(12);
                BeginZoomIn ( this, System.EventArgs.Empty );
            }
        }

        public void StartZoomOut ( )
        {
            EnableZoomIn ( );
            if ( BeginZoomOut != null )
            {
                BeginZoomOut( this, System.EventArgs.Empty );
            }
        }

        public void StopZoomIn ( )
        {
            if ( EndZoomIn != null )
            {
                EndZoomIn ( this, System.EventArgs.Empty );
            }
        }

        public void StopZoomOut ( )
        {
            if ( EndZoomOut != null )
            {
                EndZoomOut ( this, System.EventArgs.Empty );
            }
        }
                
    }
}
