using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManger : MonoBehaviour
{
    public Turret laserData;
    public Turret missileData;
    public Turret standardData;

    public Text laserCost;
    public Text missileCost;
    public Text standardCost;

    private Turret selected;
    private MapCubeController mapCube;

    public Text moneyText;
    public int money = 1000;

    public Text notice;
    public Animator animator;

    public GameObject upgradeUI;
    public Button upgradeButton;
    public Button destoryButton;

    private Animator upgradeAnimator;

    private void Awake()
    {
        laserCost.text = "$"+laserData.cost.ToString();
        missileCost.text = "$"+ missileData.cost.ToString();
        standardCost.text = "$"+ standardData.cost.ToString();
    }

    private void Start()
    {
        UpdateMoney();
        upgradeAnimator = upgradeUI.GetComponent<Animator>();
    }

    void UpdateMoney(int change = 0) 
    {
        money += change;

        moneyText.text = "$" + money.ToString();
    }

    void Notice(string noticeText)
    {
        if (notice.gameObject.activeSelf)
        {
            return;
        }
        StartCoroutine(NoticeMsg(noticeText));
    }

    IEnumerator NoticeMsg(string noticeText,float seconds = 2)
    {
        notice.gameObject.SetActive(true);
        animator.SetTrigger("Fadein");
        notice.text = noticeText;
        yield return new WaitForSeconds(seconds);
        notice.gameObject.SetActive(false);
        yield return 0;
    }

    private void Update()
    {
        ///碰撞处理
        RaycastHit raycastHit;
        bool isCollider = CheckClick(Input.mousePosition, "MapCube", out raycastHit);

        if (isCollider && selected != null)
        {
            MapCubeController mapCube = raycastHit.collider.gameObject.GetComponent<MapCubeController>();

            if (mapCube.turrent == null)
            {
                if (money >= selected.cost)
                {
                    ///扣钱
                    UpdateMoney(-selected.cost);
                    mapCube.BuildTurrent(selected);
                }
                else 
                {
                    Notice("余额不足");
                }
            }
            else
            {
                if (mapCube != this.mapCube)
                {
                    ///升级
                    ShowMenu(mapCube.transform.position, mapCube.isUpgrade);
                    this.mapCube = mapCube;
                }
                else 
                {
                    this.mapCube = null;
                    StartCoroutine(HiddenMenu());
                }
            }
        }
    }

    bool CheckClick(Vector3 mouse , string match , out RaycastHit hitinfo)
    {
        ///点击事件
        if (Input.GetMouseButtonDown(0))
        {
            ///鼠标参数为空  触摸另外处理
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                bool isCollider;

                ///鼠标点击转化为射线 射线匹配指定层
                Ray ray = Camera.main.ScreenPointToRay(mouse);

                if (match == "")
                {
                    isCollider = Physics.Raycast(ray, out hitinfo, 1000);
                }
                else
                {
                    isCollider = Physics.Raycast(ray, out hitinfo, 1000, LayerMask.GetMask(match));
                }


                return isCollider;

            }
        }

        hitinfo = new RaycastHit();
        return false;
    }

    public void OnLaserSelected(bool isOn) 
    {
        if (isOn)
        {
            selected = laserData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selected = missileData;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selected = standardData;
        }
    }


    private void ShowMenu(Vector3 position,bool isUpgrade)
    {
        StopCoroutine("HiddenMenu");
        upgradeUI.SetActive(false);
        upgradeUI.transform.position = position;
        upgradeUI.SetActive(true);

        upgradeButton.interactable = !isUpgrade;
    }

    IEnumerator HiddenMenu()
    {
        upgradeAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        upgradeUI.SetActive(false);
    }

    public void OnUpgardeBottonDown()
    {
        if (money >= this.mapCube.turrentStruct.costUp)
        {
            int costUp = this.mapCube.UpgradeTurrent();
            UpdateMoney(0 - costUp);
        }
        else
        {
            //to do
            Notice("余额不足 升级失败");
        }

        StartCoroutine(HiddenMenu());
    }

    public void OnDestoryBottonDown()
    {
        int cost = this.mapCube.DestoryTurrent();
        UpdateMoney(cost);
        StartCoroutine(HiddenMenu());
    }
}
