using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    /// mark  确定视野边界  快捷键还原视野

    public float speed = 50;
    public float mouse_scroll_speed = 400;

    void _init() { 
        //初始化视角
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");          /// a d
        float v = Input.GetAxis("Vertical");            /// w s

        float mouse = Input.GetAxis("Mouse ScrollWheel"); ///鼠标滚轮
        transform.Translate(new Vector3(h * speed, mouse* mouse_scroll_speed, v * speed) * Time.deltaTime,Space.World);
    }
}
