using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace HTCVIVE
{
    //镭射光标事件结构体
    public struct PointerEventArgs
    {
        //控制器索引
        public uint controllerIndex;
        //标记
        public uint flags;
        //距离
        public float distance;
        //目标
        public Transform target;
    }

    //光标事件代理
    public delegate void PointerEventHandler(object sender, PointerEventArgs e);


    public class SteamVR_LaserPointer : MonoBehaviour
    {
        //是否激活
        private bool active = true;
        /// <summary>
        /// 设置是否激活
        /// </summary>
        public bool Active
        {
            set
            {
                active = value;
                if (holder != null)
                {
                    if (active)
                    {
                        holder.SetActive(true);
                    }
                    else
                    {
                        holder.SetActive(false);
                    }
                }
            }
            get { return active; }
        }
        public RaycastHit hit;
        //颜色
        public Color color;
        //厚度
        public float thickness = 0.002f;
        //载体
        public GameObject holder;
        //光标
        public GameObject pointer;
        private GameObject sphere;
        bool isActive = false;
        //是否添加刚体
        public bool addRigidBody = false;
        //参考
        public Transform reference;
        //出入事件
        public event CallBack<GameObject> PointerIn;
        public event CallBack<GameObject> PointerOut;
        public event CallBack<RaycastHit> hitEvent;
        /// <summary>
        /// 脚本事件
        /// </summary>
        private VRButtonTouchAction buttonControl;

        //之前接触的对象
        Transform previousContact = null;

        // Use this for initialization
        void Start()
        {
            buttonControl = GetComponent<VRButtonTouchAction>();
            //active = true;
            //初始化载体
            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;

            //初始化光标: 正方体原型 父物体为载体 大小 位置 碰撞体 刚体 材质 颜色
            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.transform.parent = holder.transform;
            Destroy(sphere.GetComponent<SphereCollider>());
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    Object.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
            sphere.GetComponent<MeshRenderer>().material = newMaterial;
        }

        /// <summary>
        /// 引发光标进入事件
        /// </summary>
        /// <param name="e">E.</param>
        public virtual void OnPointerIn(PointerEventArgs e)
        {
           // print("enter");
            // color = Color.green;
            sphere.SetActive(true);
            pointer.GetComponent<MeshRenderer>().material.color = Color.green;
            if (PointerIn != null)
                PointerIn(gameObject);
            ExecuteEvents.Execute<IPointerEnterHandler>(e.target.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
        }


        /// <summary>
        /// 引发光标离开事件
        /// </summary>
        /// <param name="e">E.</param>
        public virtual void OnPointerOut(PointerEventArgs e)
        {
           // print("out");
            pointer.GetComponent<MeshRenderer>().material.color = Color.red;
            sphere.SetActive(false);
            if (PointerOut != null)
                PointerOut(gameObject);
            ExecuteEvents.Execute<IPointerExitHandler>(e.target.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
        }


        // Update is called once per frame
        void Update()
        {

            // print("ptint");
            //如果没有激活则激活之
            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            //距离
            float dist = 100f;

            //获取追踪控制器
            SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();

            //发射一条正前方向的射线 获取击中与否
            Ray raycast = new Ray(transform.position, transform.forward);
            // RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);
            if (hitEvent != null)
                hitEvent(hit);
            //之前有击中对象 且 不是之前击中的对象  引发离开之前击中对象事件
            if (previousContact && previousContact != hit.transform)
            {
               // print("click");
                PointerEventArgs args = new PointerEventArgs();
                if (controller != null)
                {
                    args.controllerIndex = controller.controllerIndex;
                }
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            //击中 且不是之前击中的对象  引发光标进入当前对象事件
            if (bHit && previousContact != hit.transform&&(hit.transform.tag==Tag.prefab||hit.transform.gameObject.layer==5))
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                if (controller != null)
                {
                    argsIn.controllerIndex = controller.controllerIndex;
                }
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            //没有击中
            if (!bHit)
            {
               
                previousContact = null;
            }
            //击中 且在有效范围
            if (bHit && hit.distance < 100f)
            {
               
                dist = hit.distance;
            }

            //控制器非空 且 控制器扳机按下 加粗  / 反之 不加粗
            if (buttonControl != null && buttonControl.IsTriggerPreaed)
            {
               
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
            }
            //更新位置
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
            sphere.transform.position = hit.point;
        }

    }
}