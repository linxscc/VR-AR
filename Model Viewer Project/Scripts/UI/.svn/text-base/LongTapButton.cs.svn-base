using UnityEngine;
using System.Collections;
namespace ModelViewerProject.UI
{
    using System;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class LongTapButton : Selectable, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Serializable]
        public class ButtonDownEvent : UnityEvent { }


        [FormerlySerializedAs("OnButtonDown")]
        [SerializeField]
        private ButtonDownEvent m_OnButtonDown = new ButtonDownEvent();
        
        public ButtonDownEvent OnButtonDown
        {
            get { return m_OnButtonDown; }
            set { m_OnButtonDown = value; }
        }


        [Serializable]
        public class ButtonUpEvent : UnityEvent { }

        [FormerlySerializedAs("OnButtonUp")]
        [SerializeField]
        private ButtonUpEvent m_OnButtonUp = new ButtonUpEvent();

        public ButtonUpEvent OnButtonUp
        {
            get { return m_OnButtonUp; }
            set { m_OnButtonUp = value; }
        }


        protected LongTapButton ( ) { }

        public override void OnPointerDown(PointerEventData eventData )
        {
            base.OnPointerDown ( eventData );
            if ( eventData.button != PointerEventData.InputButton.Left )
                return;
            HandleOnButtonDown ( );
        }

        public override void OnPointerUp ( PointerEventData eventData )
        {
            base.OnPointerUp ( eventData );
            if ( eventData.button != PointerEventData.InputButton.Left )
                return;
            HandleOnButtonUp ( );
        }

        private void HandleOnButtonDown ( )
        {
            if ( !IsActive ( ) || !IsInteractable ( ) )
                return;
            m_OnButtonDown.Invoke ( );
        }

        private void HandleOnButtonUp ( )
        {
            if ( !IsActive ( ) || !IsInteractable ( ) )
                return;
            m_OnButtonUp.Invoke ( );
        }

    }
}