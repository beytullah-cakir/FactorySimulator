using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI
    coin,
    appleCost,
    appleAmount,
    pearCost,
    pearAmount,
    tomatoCost,
    tomatoAmount,
    potatoCost,
    potatoAmount,
    standCapacity,
    standDetails,
    playerCapacity,
    unlockTomatoGarden,
    unlockPotatoGarden,
    unlockPearTree,
    upgradeApple,
    upgradeTotamo,
    upgradePotato,
    upgradePear,
    upgradePlayerCap,
    upgradeStandCap

    ;

    public GameObject upgradePanel;

    public bool isOpenUpgradePanel;



    public static UIManager Instance;

    Inventory cropInventory;
    GameManager gameManager;

    StandManager standManager;
   
    Crop crops;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        cropInventory = Inventory.Instance;
        gameManager = GameManager.Instance;
        standManager = StandManager.Instance;
        
        

    }

    void Update()
    {
        coin.text = $"{GameManager.Instance.coin}";
        playerCapacity.text = $"{cropInventory.capacity}";
        upgradePlayerCap.text=$"{gameManager.upgradePlayerCapCost}$";
        upgradeStandCap.text=$"{gameManager.upgradeStandCapCost}$";
        


        UpgradeMenuTexts();
        StandDetails();

    }

    void UpgradeMenuTexts()
    {
        standCapacity.text = $"{standManager.capacity}";


        foreach (var garden in gameManager.cropList)
        {
            Crop currentCrop = garden.crop;
            switch (currentCrop.productName)
            {
                case "apple":
                    appleCost.text = CostText(currentCrop.orderCost);
                    appleAmount.text = GrowTime(currentCrop.growTime);
                    upgradeApple.text=$"{currentCrop.upgradeCost}$";                 
                    break;
                case "pear":
                    pearCost.text = CostText(currentCrop.orderCost);
                    pearAmount.text = GrowTime(currentCrop.growTime);
                    unlockPearTree.text=$"{currentCrop.buyCost}$";
                    upgradePear.text=$"{currentCrop.upgradeCost}$";
                    break;
                case "tomato":
                    tomatoCost.text = CostText(currentCrop.orderCost);
                    tomatoAmount.text = GrowTime(currentCrop.growTime);
                    unlockTomatoGarden.text=$"{currentCrop.buyCost}$";
                    upgradeTotamo.text=$"{currentCrop.upgradeCost}$";
                    break;
                case "potato":
                    potatoCost.text = CostText(currentCrop.orderCost);
                    potatoAmount.text = GrowTime(currentCrop.growTime);
                    unlockPotatoGarden.text=$"{currentCrop.buyCost}$";
                    upgradePotato.text=$"{currentCrop.upgradeCost}$";
                    break;

            }
        }
    }


    public void StandDetails()
    {

        standDetails.text = $"{standManager.currentStandCount}/{standManager.capacity}\n";

        foreach (var entry in StandManager.standInventory)
        {
            standDetails.text += $"{entry.Key.productName}:{entry.Value}\n";
        }

    }

    string CostText(int info)
    {
        return $"Cost: {info}$";
    }

    string GrowTime(float info)
    {
        return $"GrowTime: {info}sn";
    }


    public void UpgradePanel()
    {
        isOpenUpgradePanel = !isOpenUpgradePanel;
        upgradePanel.SetActive(isOpenUpgradePanel);
    }

}
