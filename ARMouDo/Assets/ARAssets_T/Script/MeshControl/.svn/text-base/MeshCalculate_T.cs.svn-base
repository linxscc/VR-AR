using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tools_XYRF
{
    public class MeshCalculate_T
    {
        /// <summary>
        /// 获取模型网格最低点
        /// </summary>
        /// <returns>坐标</returns>
        static public Vector3 GetModeMeshMin()
        {
            Mesh mesh;
            Vector3 result = Vector3.zero;
            if (ModeMesh_T.Instance.singleMesh != null)
                mesh = ModeMesh_T.Instance.singleMesh;
            else
                mesh = ModeMesh_T.Instance.minMesh;
            Vector3[] vertices = mesh.vertices;
            int p = 0;
            int flag = -1;
            float minheight = 10000f;
            while (p < vertices.Length)
            {
                if (vertices[p].y < minheight)
                {
                    minheight = vertices[p].y;
                    result = vertices[p];
                    flag = p;
                }
                p++;
            }
            return result;
        }
        /// <summary>
        /// 获取模型网格最高点
        /// </summary>
        /// <returns>坐标</returns>
        static public Vector3 GetModeMeshMax()
        {
            Mesh mesh;
            Vector3 result = Vector3.zero;
            if (ModeMesh_T.Instance.singleMesh != null)
                mesh = ModeMesh_T.Instance.singleMesh;
            else
                mesh = ModeMesh_T.Instance.maxMesh;
            Vector3[] vertices = mesh.vertices;
            int p = 0;
            int flag = -1;
            float maxheight = -10000f;
            while (p < vertices.Length)
            {
                if (vertices[p].y > maxheight)
                {
                    maxheight = vertices[p].y;
                    result = vertices[p];
                    flag = p;
                }
                p++;
            }
            return result;
        }

    }
}
