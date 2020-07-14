using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManger : MonoBehaviour
{
    /// 管理所有地图拐点
    public static Transform[] positions;


    private void Awake()
    {
        positions = new Transform[transform.childCount];

        for (int i = 0; i < positions.Length; i++) 
        {
            positions[i] = transform.GetChild(i);
        }

    }
}
