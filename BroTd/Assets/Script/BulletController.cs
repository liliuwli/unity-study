using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 50;
    public float speed = 20;

    private Transform target;

    public GameObject explosionEffectPrefab;

    private float distanceSpaceTarget = 1.1f;

    public void SetTarget(Transform _target) 
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Close(false);
            return;
        }
        ///lookat 指定z轴朝向目标
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        ///修正炮弹z轴在y轴270
        transform.Rotate(new Vector3(0, 90, 0));

        OnCollisionSelf();
    }

    void OnCollisionSelf() 
    {
        Vector3 space = this.target.position - transform.position;
        if (space.magnitude <= distanceSpaceTarget)
        {
            target.GetComponent<MonsterController>().TakeDamage(damage);
            Close(true);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Monster")
        {
            other.GetComponent<MonsterController>().TakeDamage(damage);
            Close(true);
        }
    }
    */

    void Close(bool decreed) 
    {
        if (decreed)
        {
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(effect, 1);
        }
        Destroy(this.gameObject);
    }
}
