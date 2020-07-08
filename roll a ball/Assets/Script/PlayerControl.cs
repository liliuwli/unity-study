using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    /**
     * 获取玩家输入
     * 解析输入让球移动
     * 
     */

    public float speed;

    private Rigidbody rb;

    private int count;

    public Text countText;

    public Text winText;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
    }

    void FixedUpdate()
    {
        ///执行物理计算前才被使用
        ///实现物理效果 对刚体添加作用力
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        ///销毁游戏对象
        ///Destroy(other.gameObject);
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();

            if (count >= 12) 
            {
                winText.text = "winer";
            }
        }
    }

    void setCountText()
    {
        countText.text = "Count:" + count.ToString();
    }
}
