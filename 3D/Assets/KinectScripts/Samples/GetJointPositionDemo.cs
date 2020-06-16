using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// 得到关节坐标
/// </summary>
public class GetJointPositionDemo : MonoBehaviour
{
    [Tooltip("[T:我们想跟踪的Kinect关节] The Kinect joint we want to track.")]
    public KinectInterop.JointType joint = KinectInterop.JointType.HandRight;

    [Tooltip("[T:Kinect坐标（米）中的当前关节位置。]Current joint position in Kinect coordinates (meters).")]
    public Vector3 jointPosition;

    [Tooltip("[T:我们是否将关节数据保存到CSV文件。]  Whether we save the joint data to a CSV file or not.")]
    public bool isSaving = false;

    [Tooltip("[T: CSV文件的路径，我们要保存关节数据。] Path to the CSV file, we want to save the joint data to.")]
    public string saveFilePath = "joint_pos.csv";

    [Tooltip("[T:将数据保存到CSV文件多少秒，或0保存不停。] How many seconds to save data to the CSV file, or 0 to save non-stop.")]
    public float secondsToSave = 0f;


    /// <summary>
    /// 数据保存到csv文件的开始时间
    /// </summary>
    private float saveStartTime = -1f;


    void Start()
    {
        if (isSaving && File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }


    void Update()
    {
        if (isSaving)
        {
            //没有该路径   则创建
            if (!File.Exists(saveFilePath))
            {
                using (StreamWriter writer = File.CreateText(saveFilePath))
                {
                    // [T:CSV文件头] csv file header
                    string sLine = "time,joint,pos_x,pos_y,poz_z";
                    writer.WriteLine(sLine);
                }
            }

            // check the start time
            if (saveStartTime < 0f)
            {
                saveStartTime = Time.time;
            }
        }

        // 得到关节点位置
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            //传感器当前至少 检测到存在一个用户
            if (manager.IsUserDetected())
            {
                long userId = manager.GetPrimaryUserID();

                if (manager.IsJointTracked(userId, (int)joint))
                {
                    // 输出关节位置，便于跟踪
                    Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
                    jointPosition = jointPos;

                    if (isSaving)
                    {
                        if ((secondsToSave == 0f) || ((Time.time - saveStartTime) <= secondsToSave))
                        {
                            using (StreamWriter writer = File.AppendText(saveFilePath))
                            {
                                string sLine = string.Format("{0:F3},{1},{2:F3},{3:F3},{4:F3}", Time.time, ((KinectInterop.JointType)joint).ToString(), jointPos.x, jointPos.y, jointPos.z);
                                writer.WriteLine(sLine);
                            }
                        }
                    }
                }
            }
        }

    }

}
