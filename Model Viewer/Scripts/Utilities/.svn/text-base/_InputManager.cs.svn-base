using UnityEngine;
using System.Collections;
using System.Text;

using System.Linq;
using System;

namespace ZERO.Utilities
{
 
    public class InputEventArgs : System.EventArgs
    {
        public bool Used { set; get; }
        public GameObject Receiver { set; get; }
        public Vector2 UV0 { set; get; }
        public Vector2 UV1 { set; get; }
        public Vector2 UVN { set; get; }

    }

    public class _InputManager : _Singleton<_InputManager>
    {

        //（仅限单机）鼠标在某对象上悬停；
        public delegate void OnHoverEvent ( GameObject obj );
        public static event OnHoverEvent OnHover;

        //（仅限单机）鼠标在某对象上悬停后，离开；
        public delegate void OnExitEvent ( GameObject obj );
        public static event OnExitEvent OnExit;

        public delegate void OnClickEvent ( Vector3 position );
        public static event OnClickEvent OnClick;
        /// <summary>
        /// From Mouse Down to Mouse Up , Mouse is always over same object.
        /// </summary>
        /// <param name="obj"></param>
        public delegate void OnFocusInEvent ( GameObject obj, Vector2 pos );
        public static event OnFocusInEvent OnFocusIn;

        //鼠标左键按下，或抬起时，不在任何相关层级对象上；
        public delegate void OnFocusOutEvent ( );
        public static event OnFocusOutEvent OnFocusOut;

        /// <summary>
        /// 鼠标左键抬起；
        /// </summary>
        public delegate void OnReleaseEvent ( );
        public static event OnReleaseEvent OnRelease;

        //"KeyCode.Home" 
        public delegate void OnBackHomeEvent ( );
        public static event OnBackHomeEvent OnBackHome;

        //"KeyCode.Escape" or Back Key
        //public delegate void OnEscapeEvent ( );
        //public static event OnEscapeEvent OnEscape;

        ////鼠标左键/手势（单点）仅按下;
        //	public delegate void OnFlashTapEvent(GameObject g);
        //	public static event OnFlashTapEvent OnFlashTap;
        //	//鼠标左键/手势（单点）点击;
        //	public delegate bool OnShortTapEvent(GameObject g);
        //	public event OnShortTapEvent OnShortTap;
        //	//鼠标左键/手势（单点）長按;
        //	public delegate bool OnLongTapEvent(GameObject g);
        //	public event OnLongTapEvent OnLongTap;

        ///鼠标左键/手势（按下无论是否有拖动对象）的移动，或（无论是否有拖动对象）的移动
        //开始；
        public delegate void OnMoveStartEvent ( Vector3 pos );
        public static event OnMoveStartEvent OnMoveStart;
        //鼠标左键/手势（单点）拖拽;
        public delegate void OnMoveEvent ( Vector3 pos );
        public static event OnMoveEvent OnMove;
        //鼠标左键/手势（单点）释放;
        public delegate void OnMoveEndEvent ( );
        public static event OnMoveEndEvent OnMoveEnd;

        public static event EventHandler OnDragStart;
        public static event EventHandler OnDrag;
        public static event EventHandler OnDragEnd;

        public static event EventHandler OnDragUVStart;
        public static event EventHandler OnDragUV;
        public static event EventHandler OnDragUVEnd;

        public static event EventHandler OnEscape;

        public static event EventHandler OnControlOpen;

        //public delegate void OnDragStartEvent ( GameObject obj, Vector2 position );
        //public static event OnDragStartEvent OnDragStart;
        //	//鼠标左键/手势（单点）拖拽;
        //public delegate void OnDragEvent ( GameObject obj, Vector2 position );
        //public static event OnDragEvent OnDrag;
        //鼠标左键/手势（单点）释放;
        //public delegate void OnDragEndEvent ( GameObject obj );
        //public static event OnDragEndEvent OnDragEnd;

        //鼠标左键+shift/手势（双点）拖拽 （在此场景中设定为缩放）;
        public delegate void OnScaleEvent ( float delta );
        public static event OnScaleEvent OnScale;

        public Camera RaycastCamera;
        public LayerMask selectableLayer;

        public Vector3 mousePosition;
        public Vector3 clickPosition;

        //Vector3 worldPosition = RaycastCamera.ScreenToWorldPoint(Input.mousePosition);
        public Vector3 WorldPosition
        {
            get
            {
                //return RaycastCamera.ScreenToWorldPoint ( Input.mousePosition );
                return RaycastCamera.ScreenToWorldPoint ( new Vector3 ( Input.mousePosition.x, Input.mousePosition.y, RaycastCamera.nearClipPlane ) );

            }
        }

        //private Rect checkAreaRect = new Rect();
        //for 240 dpi display, a touch almost is 3/8 inch, so it is about 90 Pixels dimension...
        //private const float checkAreaWidth = 90F;
        //private const float checkAreaHeight = 90F;
        //private const float tickTime = 0.01F;

        public bool isStationary = false;

        private bool isDragStart = false;
        private bool isDragUVStart = false;

        private bool isMultiTouchStart = false;

        //	private float startDistance;

        private Vector2 screenCenter;
        //	StringBuilder textBuilder = new StringBuilder();

        /// <summary>
        /// Gets or sets the input_ y_ area_ factor.
        /// Action: the screen dimension is left bottom to right top!
        /// x : bottom value;
        /// y : upper value;
        /// </summary>
        /// <value>The input_ y_ area_ factor.</value>
        //public Vector2 Input_Y_Area_Factor { set; get; }

        public Rect EffectiveArea = new Rect(0, 0, Screen.width, Screen.height);

        bool IsOut
        {
            get
            {
                return !EffectiveArea.Contains ( Input.mousePosition );
            }
        }

        public bool isPause { set; get; }

        //	public bool IsTouchEnable{set; get;}

        void OnEnable ( )
        {
            //#if !UNITY_EDITOR && UNITY_WEBGL
            //WebGLInput.captureAllKeyboardInput = false;
            //#endif
        }

        public void Init ( )
        {
            //		IsTouchEnable = true;
            //Input_Y_Area_Factor = new Vector2(0.0f, 1.0f);//Vector2.zero;//
            EffectiveArea = new Rect ( 0, 0, Screen.width, Screen.height );
            screenCenter = new Vector2 ( Screen.width * 0.5F, -Screen.height * 0.5F );
        }



        public void Release ( )
        {
            //TODO
        }

        void Start ( )
        {
            Init ( );
        }

        void Update ( )
        {

            if ( RaycastCamera == null )
                return;

#if ( ( ( UNITY_ANDROID || UNITY_IPHONE ) && !UNITY_EDITOR ) )
		UndateTouch();
#else
            UpdateMouse ( );
#endif

            if ( Input.GetKeyDown ( KeyCode.Escape ) )
            {
                if ( OnEscape != null )
                    OnEscape (this, EventArgs.Empty );
            }

            if ( Input.GetKeyDown ( KeyCode.Home ) )
            {
                if ( OnBackHome != null )
                    OnBackHome ( );
            }

            if(
//#if ( !UNITY_EDITOR )
//                Input.GetKeyDown(KeyCode.LeftControl) && 
//#endif
                Input.GetKeyDown ( KeyCode.O ) )
            {
                
                if(!isSleeping && OnControlOpen != null )
                {
                    InputEventArgs e = new InputEventArgs();
                    
                    OnControlOpen ( this, e );
                    if ( e.Used )
                    {
                        Sleep ( 2 );
                    }
                }
            }
        }

        bool isSleeping = false;

        void Sleep(float time )
        {
            StartCoroutine ( HandleOnSleep ( time ) );
        }

        IEnumerator HandleOnSleep(float time )
        {
            var duraction = time;
            var lastTime = 0f;
            isSleeping = true;
            while(lastTime < duraction )
            {
                lastTime += Time.deltaTime;

                yield return new WaitForFixedUpdate ( );
            }
            isSleeping = false;
            yield return null;
        }

        void UndateTouch ( )
        {
            //		if(!IsTouchEnable)
            //			return;
            int count = Input.touchCount;
            if ( count == 1 )
            {
                singleTouch ( );
            }
            else if ( count >= 2 )
            {
                multiTouch ( );
            }
            else
            {
                if ( OnRelease != null )
                    OnRelease ( );
                //SelectedObject = null;
            }
        }

        private GameObject DownObj;

        private GameObject UpObj;
        
        public GameObject SelectedObject;

        private GameObject[] HoverObjs;

        private GameObject DragedObj;

        private GameObject DragedUVObj;

        public string DragObjectName;

        public void ClearFocusObjects ( )
        {
            DownObj = null;
            UpObj = null;
        }

        void singleTouch ( )
        {
            if ( IsOut )
                return;

            if ( isMultiTouchStart )
            {
                isMultiTouchStart = false;
            }

            Vector2 worldPosition = RaycastCamera.ScreenToWorldPoint(Input.GetTouch(0).position);

            if ( Input.GetTouch ( 0 ).phase == TouchPhase.Began )
            {
                DownObj = OnRaycast ( Input.GetTouch ( 0 ).position );
                UpObj = null;

                if ( OnClick != null && DownObj == null && UpObj == null )// && SelectedObject == null)
                {
                    OnClick ( worldPosition );
                    return;
                }
            }

            if ( Input.GetTouch ( 0 ).phase == TouchPhase.Stationary )
            {
            }
            else if ( Input.GetTouch ( 0 ).phase == TouchPhase.Moved )
            {
                //Vector2 delta = Input.GetTouch(0).deltaPosition;
                //Vector2 position = RaycastCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                if ( OnMove != null )
                {
                    OnMove ( worldPosition );// delta);
                }
                if ( !isDragStart && DownObj && OnDragStart != null )
                {
                    isDragStart = true;
                    var e = System.EventArgs.Empty;// new DragEventArgs(DownObj, worldPosition);
                    OnDragStart ( this, e );// obj, worldPosition );
                }
                else
                {
                    if ( OnDrag != null )
                    {
                        var e = System.EventArgs.Empty;//new DragEventArgs( DownObj, worldPosition );
                        OnDrag ( this, e );
                    }
                    //OnDrag ( DownObj, worldPosition );//delta);//
                }
            }
            else if ( Input.GetTouch ( 0 ).phase == TouchPhase.Ended || Input.GetTouch ( 0 ).phase == TouchPhase.Canceled )
            {
                if ( OnRelease != null )
                    OnRelease ( );

                if ( isDragStart && DownObj && OnDragEnd != null )
                {
                    if ( OnDragEnd != null )
                    {
                        var e  = System.EventArgs.Empty;//new DragEventArgs(DownObj, worldPosition);
                        OnDragEnd ( this, e );
                    }
                    //OnDragEnd ( DownObj );
                    isDragStart = false;
                }

                UpObj = OnRaycast ( Input.GetTouch ( 0 ).position );

                if ( UpObj && DownObj && DownObj == UpObj )
                {
                    SelectedObject = UpObj;
                }

                if ( OnFocusIn != null && DownObj && UpObj && DownObj == UpObj )
                {

                    OnFocusIn ( UpObj, worldPosition );
                }

                if ( OnFocusOut != null && !UpObj )
                {
                    OnFocusOut ( );
                }

                //SelectedObject = null;
            }
        }


        private void multiTouch ( )
        {
            if ( isDragStart )
            {
                isDragStart = false;
            }
            if ( ( Input.GetTouch ( 0 ).phase == TouchPhase.Began || Input.GetTouch ( 0 ).phase == TouchPhase.Stationary )
                && ( Input.GetTouch ( 1 ).phase == TouchPhase.Began || Input.GetTouch ( 1 ).phase == TouchPhase.Stationary ) )
            {
                if ( !isMultiTouchStart )
                {
                }
            }
            else if ( Input.GetTouch ( 0 ).phase == TouchPhase.Moved || Input.GetTouch ( 1 ).phase == TouchPhase.Moved )
            {
                if ( !isMultiTouchStart )
                {
                    isMultiTouchStart = true;
                    //				startDistance = Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);
                    //				if(OnMultiMoveStart != null)
                    //					OnMultiMoveStart(Input.GetTouch(0).position, Input.GetTouch(1).position);
                }
                else
                {
                    //				float precent = startDistance / Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);
                    Vector2 delta = Input.GetTouch(1).deltaPosition - Input.GetTouch(0).deltaPosition;
                    if ( delta.sqrMagnitude > 0.01F )//
                    {
                        Vector2 originDir = Input.GetTouch(1).position - Input.GetTouch(0).position;

                        float dir = Vector2.Dot(originDir, delta);
                        if ( OnScale != null )
                            OnScale ( dir );
                    }
                    //				if(OnMultiMove != null)
                    //					OnMultiMove(precent, (Input.GetTouch(0).position - Input.GetTouch(1).position).magnitude);

                }
            }
            else if ( Input.GetTouch ( 0 ).phase == TouchPhase.Ended || Input.GetTouch ( 0 ).phase == TouchPhase.Canceled
                     || Input.GetTouch ( 1 ).phase == TouchPhase.Ended || Input.GetTouch ( 1 ).phase == TouchPhase.Canceled )
            {
                //
                if ( isMultiTouchStart )
                {
                    //				if(OnMultiMoveEnd != null)
                    //					OnMultiMoveEnd();
                    isMultiTouchStart = false;
                }
            }

        }

        void HandleOnMouseHover ( )
        {
            GameObject[] objs = OnRaycastAll(Input.mousePosition);

            if ( HoverObjs != null && HoverObjs.Length > 0 )
            {
                if ( OnExit != null )
                {

                    foreach ( var obj in HoverObjs.Where ( o => !objs.Contains ( o ) ) )
                    {
                        OnExit ( obj );
                    }
                }
                if ( OnHover != null )
                {

                    foreach ( var obj in objs.Where ( o => !HoverObjs.Contains ( o ) ) )
                    {
                        OnHover ( obj );
                    }
                }

            }
            HoverObjs = objs;
            //Debug.Log(HoverObjs.Count());
        }

        void HandleOnMouseDown ( )
        {
            DownObj = OnRaycast ( Input.mousePosition );

            UpObj = null;

            if ( OnClick != null && DownObj == null && UpObj == null )
            {

                OnClick ( WorldPosition );
                return;
            }

            isStationary = true;

            clickPosition = Input.mousePosition;
            mousePosition = Input.mousePosition;

            if ( Input.GetKey ( KeyCode.LeftAlt ) )
            {
                isMultiTouchStart = true;
            }
        }

        void HandleOnMouse ( )
        {
            mousePosition = Input.mousePosition;
            if ( isStationary )
            {

                var isMoved = Vector3.Distance(mousePosition, clickPosition)>8F;
                if ( isMoved )//!checkAreaRect.Contains(mousePosition))
                {
                    isStationary = false;

                    if ( OnDragStart != null && !isDragStart )
                    {
                        var obj= OnRaycast(clickPosition);
                        if ( obj != null )
                        {
                            var e = System.EventArgs.Empty;//new DragEventArgs(obj, WorldPosition);
                            OnDragStart ( this, e );// obj, worldPosition );
                            //if ( e.Used )
                            {
                                isDragStart = true;
                                DragedObj = obj;
                                DragObjectName = obj.name;
                                Debug.Log ( obj.name + " is Drag start; " );
                            }
                        }
                    }

                    if ( OnDragUVStart != null && !isDragUVStart )
                    {
                        RaycastHit hit;
                        if ( OnRaycast ( clickPosition, out hit ) )
                        {
                            var uv0 = hit.textureCoord;
                            var uv1 = hit.textureCoord2;
                            var uvN = hit.lightmapCoord;
                            var obj = hit.transform.gameObject;
                            var e = System.EventArgs.Empty;//new DragEventArgs(obj, uv0);
                            OnDragUVStart ( obj, e );
                            //if ( e.Used )
                            {
                                isDragUVStart = true;

                            }
                        }
                    }

                    if ( !DownObj && OnMoveStart != null )
                        OnMoveStart ( Input.mousePosition );
                }
            }
            else
            {
                if ( Input.GetKey ( KeyCode.LeftAlt ) && isMultiTouchStart )
                {
                    float x = Input.mousePosition.x;
                    Vector2 originDir = (Vector2)mousePosition - screenCenter;
                    Vector2 delta = mousePosition - clickPosition;
                    float dir = Vector2.Dot(originDir, delta);
                    if ( OnScale != null )
                        OnScale ( dir );
                }
                else
                {
                    if ( !DownObj && OnMove != null )
                    {
                        OnMove ( Input.mousePosition );
                    }
                    if ( OnDrag != null && DragedObj )
                    {
                        OnDrag ( this, System.EventArgs.Empty);//new DragEventArgs ( DownObj, WorldPosition ) );
                    }

                    if ( OnDragUV != null && DragedUVObj != null )
                    {
                        RaycastHit hit;
                        if ( OnRaycast ( clickPosition, out hit ) && hit.transform.gameObject == DragedUVObj )
                        {
                            var uv0 = hit.textureCoord;
                            var uv1 = hit.textureCoord2;
                            var uvN = hit.lightmapCoord;
                            var obj = hit.transform.gameObject;


                            var e =System.EventArgs.Empty;// new DragEventArgs(obj, uv0);
                            OnDragUV ( obj, e );

                        }
                        else
                        {
                            if ( OnDragUVEnd != null )
                            {
                                //TODO
                                Debug.Log ( "Drag Exit !!!" );
                            }
                        }
                    }

                }
                clickPosition = mousePosition;
            }
        }

        void HandleOnMouseUp ( )
        {
            //Debug.Log("Mouse Up");
            if ( OnRelease != null )
                OnRelease ( );

            if ( OnMoveEnd != null )
                OnMoveEnd ( );

            if ( isDragStart && DragedObj && OnDragEnd != null )
            {
                var e  = System.EventArgs.Empty;//new DragEventArgs(DragedObj, WorldPosition);
                OnDragEnd ( this, e );
            }

            if ( OnDragUVEnd != null )
            {
                //TODO
                Debug.Log ( "Drag Exit !!!" );
            }

            isDragStart = false;
            DragedObj = null;
            DragObjectName = "";

            UpObj = OnRaycast ( Input.mousePosition );

            if ( isStationary && UpObj && DownObj && DownObj == UpObj )
            {
                SelectedObject = UpObj;
            }

            if ( OnFocusIn != null && SelectedObject )
            {
                OnFocusIn ( SelectedObject, WorldPosition );
            }

            isPause = false;
        }

        void UpdateMouse ( )
        {

            if ( isPause )
                return;

            if ( IsOut )
                return;

            HandleOnMouseHover ( );

            if ( Input.GetMouseButtonUp ( 1 ) )
            {
                SelectedObject = null;
                if ( OnFocusOut != null )
                    OnFocusOut ( );
                isPause = false;
            }

            if ( Input.GetMouseButtonDown ( 0 ) )
            {
                HandleOnMouseDown ( );
            }

            if ( Input.GetMouseButton ( 0 ) )
            {
                HandleOnMouse ( );
            }

            if ( Input.GetMouseButtonUp ( 0 ) )
            {
                HandleOnMouseUp ( );
            }

            if ( Input.GetKeyUp ( KeyCode.LeftAlt ) )
            {
                if ( isMultiTouchStart )
                {
                    isMultiTouchStart = false;
                }
            }

        }

        GameObject [ ] OnRaycastAll ( Vector3 pos )
        {
            Ray ray = RaycastCamera.ScreenPointToRay(pos);
            RaycastHit[] hits;
            hits = Physics.RaycastAll ( ray, Mathf.Infinity, selectableLayer );
            return hits.
                    OrderBy ( hit => hit.transform.position.z ).
                    Select ( hit => hit.transform.gameObject ).
                    ToArray ( );
        }

        GameObject OnRaycastFirstLayer ( Vector3 pos )
        {
            Ray ray = RaycastCamera.ScreenPointToRay(pos);
            RaycastHit[] hits;
            hits = Physics.RaycastAll ( ray, Mathf.Infinity, selectableLayer );

            //  Debug.Log("Hits Count : " + hits.Length);

            if ( hits.Length > 0 )
            {

                return hits.
                    OrderBy ( hit => hit.transform.position.z ).
                    Select ( hit => hit.transform.gameObject ).
                    FirstOrDefault ( );

            }
            return null;
        }

        bool OnRaycastHit ( Vector3 pos )
        {
            Ray ray = RaycastCamera.ScreenPointToRay(pos);
            RaycastHit hit;
            if ( Physics.Raycast ( ray, out hit, Mathf.Infinity, selectableLayer ) )
            {
                return true;//.transform.gameObject;
            }
            return false;
        }

        bool OnRaycast ( Vector3 pos, out RaycastHit hit )
        {
            Ray ray = RaycastCamera.ScreenPointToRay(pos);
            return Physics.Raycast ( ray, out hit, Mathf.Infinity, selectableLayer );
        }

        GameObject OnRaycast ( Vector3 pos )
        {
            Ray ray = RaycastCamera.ScreenPointToRay(pos);
            RaycastHit hit;
            if ( Physics.Raycast ( ray, out hit, Mathf.Infinity, selectableLayer ) )
            {
                return hit.transform.gameObject;
            }
            return null;
        }


    }

}