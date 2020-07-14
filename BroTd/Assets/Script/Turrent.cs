using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Turrent : MonoBehaviour
{
    /// <summary>
    /// 优化碰撞检测的思路 指定layer层碰撞交集
    /// 设置碰撞交集的入口 edit -> project setting-physics -> layer collsion matrix
    /// </summary>
    public List<GameObject> monster = new List<GameObject>();

    public float attackRateTime = 0.5f;
    private float timer = 0;

    public GameObject bulletPrefab;
    private Transform bulletTransform;

    //炮塔头部
    public Transform turrentHead;

    public bool isDot = false;
    public float damageDot = 2.5f;

    public LineRenderer laser;

    public GameObject laserEffect;

    private void Start()
    {
        Transform[] result = GetComponentsInChildren<Transform>();
        foreach (var child in result) {

            if (child.CompareTag("Position")) 
            {
                bulletTransform = child;
                break;
            }

        }

        if (bulletTransform == null) 
        {
            Debug.Log("dont match any bullet position");
        }

        timer = attackRateTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            monster.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
        {
            monster.Remove(other.gameObject);
        }
    }

    private void Update()
    {

        //瞄准
        LookAtTarget();

        if (isDot) 
        {
            Dot();
        }
        else
        {
            timer += Time.deltaTime;
            if (monster.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
    }

    private void Dot()
    {

        GameObject target = ChooseTarget();

        if (target == null)
        {
            /// 丢失目标 回收特效
            this.laser.SetPositions(new Vector3[] { new Vector3(0,0,0), new Vector3(0, 0, 0) });
            laserEffect.SetActive(false);
            return;
        }

        //dot攻击
        this.laser.SetPositions(new Vector3[] { bulletTransform.position, target.transform.position });
        target.GetComponent<MonsterController>().TakeDamage(this.damageDot);
        laserEffect.SetActive(true);
        laserEffect.transform.position = target.transform.position;
    }
    private void Attack()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletTransform.position, bulletTransform.rotation);
        GameObject target = ChooseTarget();
        if (target == null)
        {
            timer = attackRateTime;
            return;
        }
        bullet.GetComponent<BulletController>().SetTarget(target.transform);
    }
    private void LookAtTarget() 
    {
        GameObject target = ChooseTarget();
        if (target == null)
        {
            return;
        }

        Vector3 targetPositon = target.transform.position;
        targetPositon.y = turrentHead.position.y;
        turrentHead.LookAt(targetPositon);
    }


    private GameObject ChooseTarget() 
    {

        for (int i = 0; i < monster.Count; i++)
        {
            if (monster[i] == null) 
            {
                monster.Remove(monster[i]);
            }
        }

        if (monster.Count > 0)
        {
            return monster[0];
        }
        else 
        {
            return null;
        }
    }
}
