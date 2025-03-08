using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI
        coin, appleCost, appleAmount, pearCost, pearAmount,
        tomatoCost, tomatoAmount, potatoCost, potatoAmount,
        standCapacity, standDetails, playerCapacity,
        unlockTomatoGarden, unlockPotatoGarden, unlockPearTree,
        upgradeApple, upgradeTomato, upgradePotato, upgradePear,
        upgradePlayerCap, upgradeStandCap;

    public GameObject upgradePanel;
    public bool isOpenUpgradePanel;

    public static UIManager Instance;

    private Inventory cropInventory;
    private GameManager gameManager;
    private StandManager standManager;

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
        if (gameManager == null || cropInventory == null || standManager == null) return;

        UpdateUI();
    }

    void UpdateUI()
    {
        coin.text = $"{gameManager.coin}$";
        playerCapacity.text = $"{cropInventory.capacity}";
        upgradePlayerCap.text = $"{gameManager.upgradePlayerCapCost}$";
        upgradeStandCap.text = $"{gameManager.upgradeStandCapCost}$";
        standCapacity.text = $"{standManager.capacity}";

        UpdateCropInfo();
        UpdateStandDetails();
    }

    void UpdateCropInfo()
    {
        foreach (var garden in gameManager.cropList)
        {
            if (garden.crop == null) continue;

            Crop currentCrop = garden.crop;
            string costText = CostText(currentCrop.orderCost);
            string growTimeText = GrowTime(currentCrop.growTime);
            string buyCostText = $"{currentCrop.buyCost}$";
            string upgradeCostText = $"{currentCrop.upgradeCost}$";

            switch (currentCrop.productName.ToLower())
            {
                case "apple":
                    appleCost.text = costText;
                    appleAmount.text = growTimeText;
                    upgradeApple.text = upgradeCostText;
                    break;
                case "pear":
                    pearCost.text = costText;
                    pearAmount.text = growTimeText;
                    unlockPearTree.text = buyCostText;
                    upgradePear.text = upgradeCostText;
                    break;
                case "tomato":
                    tomatoCost.text = costText;
                    tomatoAmount.text = growTimeText;
                    unlockTomatoGarden.text = buyCostText;
                    upgradeTomato.text = upgradeCostText;
                    break;
                case "potato":
                    potatoCost.text = costText;
                    potatoAmount.text = growTimeText;
                    unlockPotatoGarden.text = buyCostText;
                    upgradePotato.text = upgradeCostText;
                    break;
            }
        }
    }

    void UpdateStandDetails()
    {
        standDetails.text = $"{standManager.currentStandCount}/{standManager.capacity}\n";

        foreach (var entry in StandManager.standInventory)
        {
            if (entry.Key != null)
            {
                standDetails.text += $"{entry.Key.productName}: {entry.Value}\n";
            }
        }
    }

    string CostText(int cost) => $"Cost: {cost}$";
    string GrowTime(float time) => $"GrowTime: {time} sec";

    public void ToggleUpgradePanel()
    {
        isOpenUpgradePanel = !isOpenUpgradePanel;
        upgradePanel.SetActive(isOpenUpgradePanel);
    }
}
