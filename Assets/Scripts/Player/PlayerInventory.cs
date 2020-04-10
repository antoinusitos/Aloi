using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private PlayerUpgrades myPlayerUpgrades = null;

    private PlayerStats myPlayerStats = null;

    private List<Upgrade> myUpgradesInventory = new List<Upgrade>();

    [SerializeField]
    private GameObject myInventory = null;

    private bool myInventoryOpen = false;

    [SerializeField]
    private Transform myInventoryChipsPanel = null;

    [SerializeField]
    private Transform myInventoryChipsItemPrefab = null;

    [SerializeField]
    private Transform mySlotAChipsPanel = null;
    [SerializeField]
    private Transform mySlotBChipsPanel = null;
    [SerializeField]
    private Transform mySlotCChipsPanel = null;
    [SerializeField]
    private Transform mySlotDChipsPanel = null;

    [SerializeField]
    private Text myExperienceText = null;

    [SerializeField]
    private Text myCurrentWattText = null;
    [SerializeField]
    private Slider myWattSlider = null;

    private Transform myCurrentUpgradeTransformSelected = null;
    private Upgrade myCurrentUpgradeSelected = null;

    private int myUpgradeIndex = 0;

    private Transform myCurrentUpgradeReplaceTransformSelected = null;
    private Upgrade myCurrentUpgradeReplaceSelected = null;

    private int myUpgradeReplaceIndex = 0;

    [SerializeField]
    private GameObject myInventoryChipsObject = null;

    private bool myIsReplacing = false;

    [SerializeField]
    private GameObject myReplaceChipDescriptionObject = null;

    [SerializeField]
    private Text mySelectedChipNameText = null;

    [SerializeField]
    private Text mySelectedChipDescText = null;

    [SerializeField]
    private Text myReplaceChipNameText = null;

    [SerializeField]
    private Text myReplaceChipDescText = null;

    private void Start()
    {
        myPlayerUpgrades = GetComponent<PlayerUpgrades>();
        myPlayerStats = GetComponent<PlayerStats>();
        myUpgradesInventory.Add(UpgradesManager.GetInstance().GetUpgrade(0));
        myUpgradesInventory.Add(UpgradesManager.GetInstance().GetUpgrade(1));
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            myInventoryOpen = !myInventoryOpen;
            myInventory.SetActive(myInventoryOpen);

            if(myInventoryOpen)
            {
                UpdateInventory();
            }
            else
            {
                CloseInventory();
            }
        }

        if (!myInventoryOpen)
            return;

        if(Input.GetButtonDown("Jump"))
        {
            myIsReplacing = true;
            if (myCurrentUpgradeTransformSelected != null && myCurrentUpgradeSelected != null)
            {
                ShowInventoryChip();
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(myIsReplacing)
            {
                MoveReplaceCursor(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (myIsReplacing)
            {
                MoveReplaceCursor(false);
            }
        }
    }

    private void CloseInventory()
    {
        for (int i = 0; i < myInventoryChipsPanel.childCount; i++)
        {
            Destroy(myInventoryChipsPanel.GetChild(i).gameObject);
        }

        for (int i = 0; i < mySlotAChipsPanel.childCount; i++)
        {
            Destroy(mySlotAChipsPanel.GetChild(i).gameObject);
        }

        for (int i = 0; i < mySlotBChipsPanel.childCount; i++)
        {
            Destroy(mySlotBChipsPanel.GetChild(i).gameObject);
        }

        for (int i = 0; i < mySlotCChipsPanel.childCount; i++)
        {
            Destroy(mySlotCChipsPanel.GetChild(i).gameObject);
        }

        for (int i = 0; i < mySlotDChipsPanel.childCount; i++)
        {
            Destroy(mySlotDChipsPanel.GetChild(i).gameObject);
        }

        myCurrentUpgradeTransformSelected = null;
        myCurrentUpgradeSelected = null;
    }

    private void UpdateInventory()
    {
        Upgrade[] aType = myPlayerUpgrades.GetTypeAUpgrades();
        for (int i = 0; i < aType.Length; i++)
        {
            if(aType[i] != null)
            {
                Transform t = Instantiate(myInventoryChipsItemPrefab, mySlotAChipsPanel);
                if(myCurrentUpgradeTransformSelected == null)
                {
                    SetUpgradeSelected(t, aType[i], i);
                }
            }
        }

        Upgrade[] bType = myPlayerUpgrades.GetTypeBUpgrades();
        for (int i = 0; i < bType.Length; i++)
        {
            if (bType[i] != null)
            {
                Transform t = Instantiate(myInventoryChipsItemPrefab, mySlotBChipsPanel);
                if (myCurrentUpgradeTransformSelected == null)
                {
                    SetUpgradeSelected(t, bType[i], i);
                }
            }
        }

        Upgrade[] cType = myPlayerUpgrades.GetTypeCUpgrades();
        for (int i = 0; i < cType.Length; i++)
        {
            if (cType[i] != null)
            {
                Transform t = Instantiate(myInventoryChipsItemPrefab, mySlotCChipsPanel);
                if (myCurrentUpgradeTransformSelected == null)
                {
                    SetUpgradeSelected(t, cType[i], i);
                }
            }
        }

        Upgrade[] dType = myPlayerUpgrades.GetTypeDUpgrades();
        for (int i = 0; i < dType.Length; i++)
        {
            if (dType[i] != null)
            {
                Transform t = Instantiate(myInventoryChipsItemPrefab, mySlotDChipsPanel);
                if (myCurrentUpgradeTransformSelected == null)
                {
                    SetUpgradeSelected(t, dType[i], i);
                }
            }
        }

        myCurrentUpgradeTransformSelected.GetChild(0).gameObject.SetActive(true);

        myExperienceText.text = myPlayerStats.GetExperience().ToString();

        myCurrentWattText.text = "<b>" + myPlayerStats.GetCurrentWatt().ToString() + "</b> / " + myPlayerStats.GetMaxWatt().ToString();
        myWattSlider.value = (float)myPlayerStats.GetCurrentWatt() / myPlayerStats.GetMaxWatt();
    }

    private void SetUpgradeSelected(Transform aTransform, Upgrade anUpgrade, int anIndex)
    {
        myCurrentUpgradeTransformSelected = aTransform;
        myCurrentUpgradeSelected = anUpgrade;
        myUpgradeIndex = anIndex;

        mySelectedChipNameText.text = myCurrentUpgradeSelected.GetName();
        mySelectedChipDescText.text = myCurrentUpgradeSelected.GetDescription();
    }

    private void ShowInventoryChip()
    {
        myReplaceChipDescriptionObject.SetActive(true);

        myInventoryChipsObject.SetActive(true);

        for (int i = 0; i < myInventoryChipsPanel.childCount; i++)
        {
            Destroy(myInventoryChipsPanel.GetChild(i).gameObject);
        }

        for (int i = 0; i < myUpgradesInventory.Count; i++)
        {
            if (myUpgradesInventory[i] != null)
            {
                Transform t = Instantiate(myInventoryChipsItemPrefab, myInventoryChipsPanel);
                if(myCurrentUpgradeReplaceSelected == null)
                {
                    myCurrentUpgradeReplaceSelected = myUpgradesInventory[i];
                    myCurrentUpgradeReplaceTransformSelected = t;
                    myCurrentUpgradeReplaceTransformSelected.GetChild(0).gameObject.SetActive(true);
                    myUpgradeReplaceIndex = i;

                    myReplaceChipNameText.text = myCurrentUpgradeReplaceSelected.GetName();
                    myReplaceChipDescText.text = myCurrentUpgradeReplaceSelected.GetDescription();
                }
            }
        }
    }

    private void MoveReplaceCursor(bool aMoveRight)
    {
        bool found = false;

        if (aMoveRight && myUpgradesInventory.Count - 1 > myUpgradeReplaceIndex)
        {
            myUpgradeReplaceIndex++;
            found = true;
        }
        else if (!aMoveRight && myUpgradeReplaceIndex > 0)
        {
            myUpgradeReplaceIndex--;
            found = true;
        }

        if (found)
        {
            myCurrentUpgradeReplaceTransformSelected.GetChild(0).gameObject.SetActive(false);
            myCurrentUpgradeReplaceSelected = myUpgradesInventory[myUpgradeReplaceIndex];
            myCurrentUpgradeReplaceTransformSelected = myInventoryChipsPanel.GetChild(myUpgradeReplaceIndex);
            myCurrentUpgradeReplaceTransformSelected.GetChild(0).gameObject.SetActive(true);

            myReplaceChipNameText.text = myCurrentUpgradeReplaceSelected.GetName();
            myReplaceChipDescText.text = myCurrentUpgradeReplaceSelected.GetDescription();
        }
    }
}
