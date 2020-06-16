using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CacheDarta_T
{
    /// <summary>
    /// 设备信息 缓存
    /// </summary>
    public class DeviceInfoCacheDarta
    {
        /// <summary>
        /// 设备是否支持AR功能
        /// </summary>
        public bool isCanOpenAR;
        /// <summary>
        /// 设备是否支持录屏功能
        /// </summary>
        public bool isCanOpenReplayVideo;

        /// <summary>
        /// 设备名字
        /// </summary>
        public string name;
        /// <summary>
        /// 设备系统
        /// </summary>
        public DeviceSystem system;
        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceModel model;
        /// <summary>
        /// 本地设备类型
        /// </summary>
        public DeviceModel localizedModel;
        /// <summary>
        /// 系统版本[详细版本]
        /// </summary>
        public float systemVersion;
        /// <summary>
        /// 主要系统版本[大版本]
        /// </summary>
        public int majorSystemVersion;
        /// <summary>
        /// GUID in Base64
        /// </summary>
        public string GUID;


        /// <summary>
        /// 设备信息
        /// </summary>
        private ISN_Device device;

        /// <summary>
        /// 初始化赋值 设备信息记录
        /// </summary>
        public void Init_DeviceInfoCacheDarta()
        {
#if UNITY_IPHONE
            name = device.Name;
            system = DeviceSystem.IOS;
            if (device.SystemName == "iPhone OS")
            {
                model = DeviceModel.iPhone;
                localizedModel = DeviceModel.iPhone;
            }
            else
            {
                model = DeviceModel.iPad;
                localizedModel = DeviceModel.iPad;
            }
            // systemVersion = float.Parse(device.SystemVersion);

            majorSystemVersion = device.MajorSystemVersion;
            GUID = device.GUID.Base64String;

            //判断设备是否支持AR功能 / 录屏功能
            if (majorSystemVersion >= 11.0f)
            {
                isCanOpenAR = true;
                isCanOpenReplayVideo = true;
                return;
            }
            else
                isCanOpenAR = false;
            if (majorSystemVersion >= 9.0f)
                isCanOpenReplayVideo = true;
            else
                isCanOpenReplayVideo = false;
#elif UNITY_ANDROID

#endif
        }

        private DeviceInfoCacheDarta()
        {
#if UNITY_IPHONE
            device = ISN_Device.CurrentDevice;
            //IOSMessage.Create("Device Info", "Name: " + device.Name + "\n"
            //  + "System Name: " + device.SystemName + "\n"
            //  + "Model: " + device.Model + "\n"
            //  + "Localized Model: " + device.LocalizedModel + "\n"
            //  + "System Version: " + device.SystemVersion + "\n"
            //  + "Major System Version: " + device.MajorSystemVersion + "\n"
            //  + "User Interface Idiom: " + device.InterfaceIdiom + "\n"
            //  + "GUID in Base64: " + device.GUID.Base64String);
#elif UNITY_ANDROID
#endif
        }
        private static DeviceInfoCacheDarta instance;
        public static DeviceInfoCacheDarta Instance
        {
            get
            {
                if (instance == null)
                    instance = new DeviceInfoCacheDarta();
                return instance;
            }
            set
            {
                instance = value;
            }
        }

    }
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceModel
    {
        iPhone,
        iPad,
        Android
    }
    /// <summary>
    /// 设备系统
    /// </summary>
    public enum DeviceSystem
    {
        IOS,
        Android
    }
}
