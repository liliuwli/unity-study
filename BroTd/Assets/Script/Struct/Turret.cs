using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turret
{
    public GameObject turretPrefab;
    public int cost;
    public GameObject turretUpPrefab;
    public int costUp;
    public TurretType type;
}

public enum TurretType 
{
    Laser,
    Missile,
    Standard
}