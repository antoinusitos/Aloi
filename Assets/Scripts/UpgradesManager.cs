﻿using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    private static UpgradesManager myInstance = null;

    private List<Upgrade> myUpgrades = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        myInstance = this;
        LoadUpgrades();
    }

    public static UpgradesManager GetInstance()
    {
        return myInstance;
    }

    private void LoadUpgrades()
    {
        myUpgrades = new List<Upgrade>();
        myUpgrades.Add(new MomentumBooster());
        myUpgrades.Add(new OverampedBiceps());
        myUpgrades.Add(new DashThrusters());
    }

    public Upgrade GetUpgrade(int anIndex)
    {
        return myUpgrades[anIndex];
    }

    public Upgrade GetUpgrade(string aName)
    {
        for(int i = 0; i < myUpgrades.Count; i++)
        {
            if(myUpgrades[i].GetName() == aName)
            {
                return myUpgrades[i];
            }
        }

        return null;
    }
}
