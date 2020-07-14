using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// 怪物ai
public class MonsterController : MonoBehaviour
{

    public float speed = 5;

    public float hp = 120;


    public GameObject explosionEffectPrefab;

    private float totalHp;
    public Slider HpSlider;

    private static Transform[] positions;

    /// 地图寻路点
    private int positions_key = 0;

    // Start is called before the first frame update
    void Start()
    {
        positions = WayPointManger.positions;
        totalHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() 
    {
        if (positions_key == positions.Length)
        {
            MonsterCome();
            return;
        }

        ///取得方向的单位向量
        transform.Translate((positions[positions_key].position - transform.position).normalized * Time.deltaTime * speed);

        if (Vector3.Distance(positions[positions_key].position,transform.position) < 0.3f) 
        {
            positions_key++;
        }
    }

    private void OnDestroy()
    {
        MonsterManger.CountMonsterAlive--;
    }

    ///到达终点
    private void MonsterCome()
    {
        Close(true);
        ///触发失败
        GameManger.Instance.Failed();
    }

    public void TakeDamage(float damage) 
    {
        if (hp > damage)
        {
            hp -= damage;
            HpSlider.value = (float)hp / totalHp;
        }
        else
        {
            HpSlider.value = 0;
            hp = 0;
            Close(false);
        }

    }

    private void Close(bool isCome)
    {
        if (isCome)
        {

        }
        else 
        {
            ///被打死触发特效
            GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

            //当前怪物颜色
            Material material = GetComponent<MeshRenderer>().materials[0];

            //当前特效材质对象
            Material m_Material = effect.GetComponent<ParticleSystemRenderer>().material;

            //当前特效材质修改颜色
            m_Material.SetColor("_Color", material.color);
            Destroy(effect, 1);
        }
        Destroy(this.gameObject);
    }
}
