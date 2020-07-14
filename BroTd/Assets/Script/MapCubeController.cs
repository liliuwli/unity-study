using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCubeController : MonoBehaviour
{
    [HideInInspector]
    public GameObject turrent;
    [HideInInspector]
    public bool isUpgrade = false;

    public GameObject buildEffect;

    private Renderer colorRenderer;

    //地形上建筑
    [HideInInspector]
    public Turret turrentStruct;

    public int UpgradeTurrent() 
    {
        if (this.isUpgrade) 
        {
            return 0;
        }
        this.Build(turrentStruct.turretUpPrefab);
        this.isUpgrade = true;
        return turrentStruct.costUp;
    }
    
    public int DestoryTurrent() 
    {
        Destroy(turrent);

        this.isUpgrade = false;
        this.turrent = null;

        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        return turrentStruct.cost;
    }


    private void Start()
    {
        colorRenderer = GetComponent<Renderer>();
    }

    public void BuildTurrent(Turret _turret)
    {
        this.turrentStruct = _turret;
        this.isUpgrade = false;
        GameObject prefab = turrentStruct.turretPrefab;
        this.Build(prefab);
    }

    private void Build(GameObject turrentPrefab) 
    {
        if (this.turrent != null)
        {
            Destroy(turrent);
        }
        turrent = GameObject.Instantiate(turrentPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
    }

    private void OnMouseEnter()
    {

        if (turrent == null && EventSystem.current.IsPointerOverGameObject() == false) 
        {
            //地图空白且未指向UI
            colorRenderer.material.SetColor("_Color", Color.green);
        }
    }

    private void OnMouseExit()
    {
        colorRenderer.material.SetColor("_Color", Color.white);
    }
}
