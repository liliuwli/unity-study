using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{

    public GameObject gameover;

    public Text endMessage;

    public static GameManger Instance;

    private MonsterManger monsterManger;

    private void Awake()
    {
        Instance = this;
        monsterManger = GetComponent<MonsterManger>();
    }

    public void Win() 
    {
        gameover.SetActive(true);
        endMessage.text = "玩家胜利";
    }


    public void Failed()
    {
        monsterManger.Fail();
        gameover.SetActive(true);
    }

    public void Retry() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
