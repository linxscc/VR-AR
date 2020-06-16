/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
namespace MoDouAR
{
    public class OnBecame3DUI : MonoBehaviour
    {
        
        private void OnBecameVisible()
        {
            print(true);
            transform.parent.GetComponent<Menu3D>().OnBecame = true;
           
        }
        private void OnBecameInvisible()
        {
            print(false);
            transform.parent.GetComponent<Menu3D>().OnBecame = false;
           
        }
    }
}