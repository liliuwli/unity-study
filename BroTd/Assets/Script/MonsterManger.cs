using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManger : MonoBehaviour
{
    public static int CountMonsterAlive = 0;

    public Wave[] waves;

    public Transform start;

    public float waveCd = 3;

    //是否为循环圈模式 即出兵按上一波消亡开始 否则为按时出兵
    public bool isLoop = false;

    private Coroutine coroutine;

    void Start()
    {
        ///协程产生
        coroutine = StartCoroutine(SpawnMonster());
    }

    public void Fail()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnMonster()
    {
        foreach (Wave wave in waves) 
        { 
            for(int i=0;i < wave.count;i++)
            {

                GameObject.Instantiate(wave.MonsterPrefab , start.position ,Quaternion.identity);
                CountMonsterAlive++;

                if (i != wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.cd);
                }

            }

            while (CountMonsterAlive>0 && !isLoop) 
            {
                yield return 0;
            }

            yield return new WaitForSeconds(waveCd);
        }

        while (CountMonsterAlive > 0)
        {
            yield return 0;
        }

        ///触发游戏胜利
        GetComponent<GameManger>().Win();
    }
}
