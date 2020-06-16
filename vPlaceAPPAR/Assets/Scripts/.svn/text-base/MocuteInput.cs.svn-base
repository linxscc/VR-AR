/*
 *    日期:
 *    作者:
 *    标题:
 *    功能:
*/
using UnityEngine;
using System.Collections;
using System;

namespace PlaceAR
{
    public class MocuteInput : MonoBehaviour
    {
        private StereoCam stereoCam;
        public GUIStyle gui;
        private void Start()
        {
            stereoCam = GetComponent<StereoCam>();
        }
        void Update()
        {
            //if (Input.GetButton("Fire1"))
            //{
            //    stereoCam.eyeDistance += 0.001f;
            //}
            //if (Input.GetButton("Fire2"))
            //{
            //    stereoCam.eyeDistance -= 0.001f;
            //}
            //if (Input.GetButton("Fire3"))
            //{
            //    stereoCam.parallaxDistance += 0.01f;
            //}
            //if (Input.GetButton("Jump"))
            //{
            //    stereoCam.parallaxDistance -= 0.01f;
            //}

        }
        float horizontalvalue;
        private void OnGUI()
        {
            string a = stereoCam.eyeDistance + "==" + stereoCam.parallaxDistance;
            GUI.Label(new Rect(0, 0, 500, 50), a, gui);
            stereoCam.parallaxDistance = GUI.HorizontalSlider(new Rect(20, 50, 200, 20), stereoCam.parallaxDistance, 0, 10);
            stereoCam.eyeDistance = GUI.HorizontalSlider(new Rect(20, 80, 200, 20), stereoCam.eyeDistance, 0, 0.2f);
        }
    }
}