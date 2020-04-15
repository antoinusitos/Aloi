using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;
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
    private int myUpgradeCategoryIndex = 0;

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
    private Text mySelectedChipHeatText = null;

    [SerializeField]
    private Text mySelectedChipWattText = null;

    [SerializeField]
    private Text myReplaceChipNameText = null;

    [SerializeField]
    private Text myReplaceChipDescText = null;

    [SerializeField]
    private Text myReplaceChipHeatText = null;

    [SerializeField]
    private Text myReplaceChipWattText = null;

    [SerializeField]
    private Sprite myTypeASprite = null;
    [SerializeField]
    private Sprite myTypeBSprite = null;
    [SerializeField]
    private Sprite myTypeCSprite = null;
    [SerializeField]
    private Sprite myTypeDSprite = null;

    private bool myCanModify = false;
    private bool myInWorkbench = false;

    private void Start()
    {
        myPlayerUpgrades = GetComponent<PlayerUpgrades>();
        myPlayerStats = GetComponent<PlayerStats>();
        myUpgradesInventory.Add(UpgradesManager.GetInstance().GetUpgrade(1));
    }

    private void Update()
    {
        if(!myInWorkbench && Input.GetButtonDown("Inventory"))
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

        if(myCanModify && Input.GetButtonDown("Jump"))
        {
            if(!myIsReplacing)
            {
                if (myCurrentUpgradeTransformSelected != null && myCurrentUpgradeSelected != null)
                {
                    ShowInventoryChip();
                }
            }
            else
            {
                ReplaceChips();
            }
        }
        else if (myCanModify && Input.GetButtonDown("Cancel"))
        {
            if(myIsReplacing)
            {
                myIsReplacing = false;
                HideInventoryChip();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(myIsReplacing)
            {
                MoveReplaceCursor(true);
            }
            else
            {
                MoveSelectCursor(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (myIsReplacing)
            {
                MoveReplaceCursor(false);
            }
            else
            {
                MoveSelectCursor(false);
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
                t.GetComponent<Image>().sprite = myTypeASprite;
                if(myCurrentUpgradeTransformSelected == null)
                {
                    SetUpgradeSelected(t, aType[i], i, 0);
                }
            }
        }

        Upgrade[] bType = myPlayerUpgrades.GetTypeBUpgrades();
        for (int i = 0; i < bType.Length; i++)
        {
            if (bType[i] != null)
            {
                Transform t = Instantiate(myInventoryChipsItemPrefab, mySlotBChipsPanel);
                t.GetComponent<Image>().sprite = myTypeBSprite;
                if (myCurrentUpgradeTransformSelected == null)
                {
                    SetUpgradeSelected(t, bType[i], i, 1);
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
                    SetUpgradeSelected(t, cType[i], i, 2);
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
                    SetUpgradeSelected(t, dType[i], i, 3);
                }
            }
        }

        if (myCurrentUpgradeTransformSelected == null)
            return;

        myCurrentUpgradeTransformSelected.GetChild(0).gameObject.SetActive(true);

        myExperienceText.text = myPlayerStats.GetExperience().ToString();

        myCurrentWattText.text = "<b>" + myPlayerStats.GetCurrentWatt().ToString() + "</b> / " + myPlayerStats.GetMaxWatt().ToString();
        myWattSlider.value = (float)myPlayerStats.GetCurrentWatt() / myPlayerStats.GetMaxWatt();
    }

    private void ReplaceChips()
    {
        if (myUpgradeCategoryIndex == 0)
        {
            Upgrade[] upgrades = myPlayerUpgrades.GetTypeAUpgrades();
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (upgrades[i] == myCurrentUpgradeSelected)
                {
                    myPlayerUpgrades.SetUpgradeTypeA(myCurrentUpgradeReplaceSelected, i);
                    for (int j = 0; j < myUpgradesInventory.Count; j++)
                    {
                        if (myUpgradesInventory[j] == myCurrentUpgradeReplaceSelected)
                        {
                            myUpgradesInventory[j] = myCurrentUpgradeSelected;
                        }
                    }
                    break;
                }
            }
        }
        else if (myUpgradeCategoryIndex == 1)
        {
            Upgrade[] upgrades = myPlayerUpgrades.GetTypeBUpgrades();
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (upgrades[i] == myCurrentUpgradeSelected)
                {
                    myPlayerUpgrades.SetUpgradeTypeB(myCurrentUpgradeReplaceSelected, i);
                    for (int j = 0; j < myUpgradesInventory.Count; j++)
                    {
                        if (myUpgradesInventory[j] == myCurrentUpgradeReplaceSelected)
                        {
                            myUpgradesInventory[j] = myCurrentUpgradeSelected;
                        }
                    }
                    break;
                }
            }
        }
        else if (myUpgradeCategoryIndex == 2)
        {
            Upgrade[] upgrades = myPlayerUpgrades.GetTypeCUpgrades();
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (upgrades[i] == myCurrentUpgradeSelected)
                {
                    myPlayerUpgrades.SetUpgradeTypeC(myCurrentUpgradeReplaceSelected, i);
                    for (int j = 0; j < myUpgradesInventory.Count; j++)
                    {
                        if (myUpgradesInventory[j] == myCurrentUpgradeReplaceSelected)
                        {
                            myUpgradesInventory[j] = myCurrentUpgradeSelected;
                        }
                    }
                    break;
                }
            }
        }
        else if (myUpgradeCategoryIndex == 3)
        {
            Upgrade[] upgrades = myPlayerUpgrades.GetTypeDUpgrades();
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (upgrades[i] == myCurrentUpgradeSelected)
                {
                    myPlayerUpgrades.SetUpgradeTypeD(myCurrentUpgradeReplaceSelected, i);
                    for (int j = 0; j < myUpgradesInventory.Count; j++)
                    {
                        if (myUpgradesInventory[j] == myCurrentUpgradeReplaceSelected)
                        {
                            myUpgradesInventory[j] = myCurrentUpgradeSelected;
                        }
                    }
                    break;
                }
            }
        }

        myPlayerStats.AddCurrentWatt(-myCurrentUpgradeSelected.GetWatt());
        myPlayerStats.AddCurrentWatt(myCurrentUpgradeReplaceSelected.GetWatt());

        Upgrade temp = myCurrentUpgradeSelected;
        myCurrentUpgradeSelected = myCurrentUpgradeReplaceSelected;
        myCurrentUpgradeReplaceSelected = temp;

        HideInventoryChip();
        SetUpgradeSelected(myCurrentUpgradeTransformSelected, myCurrentUpgradeSelected, myUpgradeIndex, myUpgradeCategoryIndex);

        myCurrentWattText.text = "<b>" + myPlayerStats.GetCurrentWatt().ToString() + "</b> / " + myPlayerStats.GetMaxWatt().ToString();
        myWattSlider.value = (float)myPlayerStats.GetCurrentWatt() / myPlayerStats.GetMaxWatt();
    }

    private void SetUpgradeSelected(Transform aTransform, Upgrade anUpgrade, int anIndex, int aCategoryIndex)
    {
        myUpgradeCategoryIndex = aCategoryIndex;

        myCurrentUpgradeTransformSelected = aTransform;
        myCurrentUpgradeSelected = anUpgrade;
        myUpgradeIndex = anIndex;

        mySelectedChipNameText.text = myCurrentUpgradeSelected.GetName();
        mySelectedChipDescText.text = myCurrentUpgradeSelected.GetDescription();
        mySelectedChipHeatText.text = myCurrentUpgradeSelected.GetHeat().ToString();
        mySelectedChipWattText.text = myCurrentUpgradeSelected.GetWatt().ToString();
    }

    private void ShowInventoryChip()
    {
        myInventoryChipsObject.SetActive(true);

        for (int i = 0; i < myInventoryChipsPanel.childCount; i++)
        {
            Destroy(myInventoryChipsPanel.GetChild(i).gameObject);
        }

        UpgradeType typeSeached = UpgradeType.TYPEA;
        if (myUpgradeCategoryIndex == 1)
            typeSeached = UpgradeType.TYPEB;
        else if (myUpgradeCategoryIndex == 2)
            typeSeached = UpgradeType.TYPEC;
        else if (myUpgradeCategoryIndex == 3)
            typeSeached = UpgradeType.TYPED;

        for (int i = 0; i < myUpgradesInventory.Count; i++)
        {
            if (myUpgradesInventory[i] != null && myUpgradesInventory[i].GetMyType() == typeSeached)
            {
                myIsReplacing = true;
                Transform t = Instantiate(myInventoryChipsItemPrefab, myInventoryChipsPanel);
                if(myUpgradeCategoryIndex == 1)
                {
                    t.GetComponent<Image>().sprite = myTypeBSprite;
                }
                else if (myUpgradeCategoryIndex == 2)
                {
                    t.GetComponent<Image>().sprite = myTypeCSprite;
                }
                else if (myUpgradeCategoryIndex == 3)
                {
                    t.GetComponent<Image>().sprite = myTypeDSprite;
                }
                if (myCurrentUpgradeReplaceSelected == null)
                {
                    myCurrentUpgradeReplaceSelected = myUpgradesInventory[i];
                    myCurrentUpgradeReplaceTransformSelected = t;
                    myCurrentUpgradeReplaceTransformSelected.GetChild(0).gameObject.SetActive(true);
                    myUpgradeReplaceIndex = i;

                    myReplaceChipDescriptionObject.SetActive(true);

                    myReplaceChipNameText.text = myCurrentUpgradeReplaceSelected.GetName();
                    myReplaceChipDescText.text = myCurrentUpgradeReplaceSelected.GetDescription();
                    myReplaceChipHeatText.text = myCurrentUpgradeReplaceSelected.GetHeat().ToString();
                    myReplaceChipWattText.text = myCurrentUpgradeReplaceSelected.GetWatt().ToString();
                }
            }
        }
    }

    private void HideInventoryChip()
    {
        myReplaceChipDescriptionObject.SetActive(false);

        myInventoryChipsObject.SetActive(false);

        myCurrentUpgradeReplaceTransformSelected = null;
        myCurrentUpgradeReplaceSelected = null;

        myIsReplacing = false;
    }

    private void MoveSelectCursor(bool aMoveRight)
    {
        Upgrade[] upgrades = null;

        if(myUpgradeCategoryIndex == 0)
        {
            upgrades = myPlayerUpgrades.GetTypeAUpgrades();

            if (aMoveRight)
            {
                if ((myUpgradeIndex == 1 || myUpgradeIndex == 3) && upgrades[myUpgradeIndex - 1] != null)
                {
                    myUpgradeIndex--;
                }
                else
                {
                    ChangeSelectedCategory(out upgrades, 1);
                }
            }
            else
            {
                if ((myUpgradeIndex == 0 || myUpgradeIndex == 2) && upgrades[myUpgradeIndex + 1] != null)
                {
                    myUpgradeIndex++;
                }
            }

        }
        else if (myUpgradeCategoryIndex == 1)
        {
            upgrades = myPlayerUpgrades.GetTypeBUpgrades();
            if (!aMoveRight)
            {
                if ((myUpgradeIndex == 1 || myUpgradeIndex == 3) && upgrades[myUpgradeIndex - 1] != null)
                {
                    myUpgradeIndex--;
                }
                else
                {
                    ChangeSelectedCategory(out upgrades, -1);
                }
            }
            else
            {
                if ((myUpgradeIndex == 0 || myUpgradeIndex == 2) && upgrades[myUpgradeIndex + 1] != null)
                {
                    myUpgradeIndex++;
                }
            }
        }
        else if (myUpgradeCategoryIndex == 2)
        {
            upgrades = myPlayerUpgrades.GetTypeCUpgrades();
            if (aMoveRight)
            {
                if ((myUpgradeIndex == 1 || myUpgradeIndex == 3) && upgrades[myUpgradeIndex - 1] != null)
                {
                    myUpgradeIndex--;
                }
                else
                {
                    ChangeSelectedCategory(out upgrades, 1);
                }
            }
            else
            {
                if ((myUpgradeIndex == 0 || myUpgradeIndex == 2) && upgrades[myUpgradeIndex + 1] != null)
                {
                    myUpgradeIndex++;
                }
            }
        }
        else if (myUpgradeCategoryIndex == 3)
        {
            upgrades = myPlayerUpgrades.GetTypeDUpgrades();
            if (!aMoveRight)
            {
                if ((myUpgradeIndex == 1 || myUpgradeIndex == 3) && upgrades[myUpgradeIndex - 1] != null)
                {
                    myUpgradeIndex--;
                }
                else
                {
                    ChangeSelectedCategory(out upgrades, -1);
                }
            }
            else
            {
                if ((myUpgradeIndex == 0 || myUpgradeIndex == 2) && upgrades[myUpgradeIndex + 1] != null)
                {
                    myUpgradeIndex++;
                }
            }
        }

        myCurrentUpgradeTransformSelected.GetChild(0).gameObject.SetActive(false);
        myCurrentUpgradeSelected = upgrades[myUpgradeIndex];

        Transform slot = mySlotAChipsPanel;
        if (myUpgradeCategoryIndex == 1)
        {
            slot = mySlotBChipsPanel;
        }
        else if (myUpgradeCategoryIndex == 2)
        {
            slot = mySlotCChipsPanel;
        }
        else if (myUpgradeCategoryIndex == 3)
        {
            slot = mySlotDChipsPanel;
        }

        myCurrentUpgradeTransformSelected = slot.GetChild(myUpgradeIndex);
        myCurrentUpgradeTransformSelected.GetChild(0).gameObject.SetActive(true);

        SetUpgradeSelected(myCurrentUpgradeTransformSelected, myCurrentUpgradeSelected, myUpgradeIndex, myUpgradeCategoryIndex);
    }

    private void ChangeSelectedCategory(out Upgrade[] someUpgrade, int aSens)
    {
        if(aSens == 1 && (myUpgradeCategoryIndex == 1 || myUpgradeCategoryIndex == 3))
        {
            someUpgrade = myUpgradeCategoryIndex == 1 ? myPlayerUpgrades.GetTypeBUpgrades() : myPlayerUpgrades.GetTypeDUpgrades();
            return;
        }
        else if (aSens == -1 && (myUpgradeCategoryIndex == 0 || myUpgradeCategoryIndex == 2))
        {
            someUpgrade = myUpgradeCategoryIndex == 0 ? myPlayerUpgrades.GetTypeAUpgrades() : myPlayerUpgrades.GetTypeCUpgrades();
            return;
        }

        int categoryTemp = myUpgradeCategoryIndex + aSens;
        Upgrade[] temp = null;

        if (categoryTemp == 0)
        {
            temp = myPlayerUpgrades.GetTypeAUpgrades();
        }
        else if (categoryTemp == 1)
        {
            temp = myPlayerUpgrades.GetTypeBUpgrades();
        }
        else if (categoryTemp == 2)
        {
            temp = myPlayerUpgrades.GetTypeCUpgrades();
        }
        else if (categoryTemp == 3)
        {
            temp = myPlayerUpgrades.GetTypeDUpgrades();
        }

        if(temp[0] == null)
        {
            if (aSens == 1 && (categoryTemp == 1 || categoryTemp == 3))
            {
                someUpgrade = categoryTemp == 1 ? myPlayerUpgrades.GetTypeBUpgrades() : myPlayerUpgrades.GetTypeDUpgrades();
                return;
            }
            else if (aSens == -1 && (categoryTemp == 0 || categoryTemp == 2))
            {
                someUpgrade = categoryTemp == 0 ? myPlayerUpgrades.GetTypeAUpgrades() : myPlayerUpgrades.GetTypeCUpgrades();
                return;
            }
            else
            {
                someUpgrade = null;
                Debug.LogError("Out of Scope", this);
            }
        }
        else
        {
            myUpgradeIndex = 0;
            myUpgradeCategoryIndex = categoryTemp;
            someUpgrade = temp;
        }
    }

    private void MoveReplaceCursor(bool aMoveRight)
    {
        if(myCurrentUpgradeReplaceTransformSelected == null)
        {
            return;
        }

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
            myReplaceChipHeatText.text = myCurrentUpgradeReplaceSelected.GetHeat().ToString();
            myReplaceChipWattText.text = myCurrentUpgradeReplaceSelected.GetWatt().ToString();
        }
    }

    public void SetInWorkBench(bool aNewState)
    {
        myCanModify = aNewState;
        myInWorkbench = aNewState;

        myInventoryOpen = aNewState;
        myInventory.SetActive(aNewState);

        if (aNewState)
            UpdateInventory();
        else
            CloseInventory();
    }
}
