using System;
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
    standCapacity

    ;

    public GameObject upgradePanel;

    public bool isOpenUpgradePanel;



    public static UIManager Instance;

    Inventory cropInventory;
    GameManager gameManager;

    StandManager standManager;


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

        standCapacity.text = $"{standManager.currentStandCount}/{standManager.capacity}";



    }

    void Demo()
    {
        foreach (var garden in gameManager.cropList)
        {
            Crop currentCrop = garden.crop;
            switch (currentCrop.productName)
            {
                case "apple":
                    appleCost.text = CostText(currentCrop.orderCost);
                    appleAmount.text = AmountText(garden.cropCount);
                    break;
                case "pear":
                    pearCost.text = CostText(currentCrop.orderCost);
                    pearAmount.text = AmountText(garden.cropCount);
                    break;
                case "tomato":
                    tomatoCost.text = CostText(currentCrop.orderCost);
                    tomatoAmount.text = AmountText(garden.cropCount);
                    break;
                case "potato":
                    potatoCost.text = CostText(currentCrop.orderCost);
                    potatoAmount.text = AmountText(garden.cropCount);
                    break;



            }
        }
    }

    string CostText(int info)
    {
        return $"Cost: {info}";
    }

    string AmountText(int info)
    {
        return $"Amount: {info}";
    }


    public void UpgradePanel()
    {
        isOpenUpgradePanel = !isOpenUpgradePanel;
        upgradePanel.SetActive(isOpenUpgradePanel);
    }

}
