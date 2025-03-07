using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //singleton
    public static GameManager Instance;
    //mevcut ürünlerin listesi
    private readonly List<Crop> currentCrops = new();


    //ürünlere ait olan bahçe veya ağaçların listesi
    public List<GameObject> crops;

    public GameObject customerObject;

    public Transform startPos, grocerPos, avoidPos;

    public List<GardenManager> cropList;

    public int upgradePlayerCapCost, upgradeStandCapCost;



    public int coin;

    void Awake()
    {
        Instance = this;
        ManageCrops();
    }




    //mevcut ürünlerin listesini döndürür
    public List<Crop> GetCurrentCrops()
    {
        return currentCrops;
    }

    //eğer ürünün leveli mevcut levelden küçükse mevcut ürünlerin listesine ekler
    public void ManageCrops()
    {
        foreach (var crop in crops)
        {
            Crop _crop = crop.GetComponent<GardenManager>().crop;
            if (crop.activeSelf && !currentCrops.Contains(_crop))
            {
                currentCrops.Add(_crop);
            }
        }
    }





    void Update()
    {
        ManageCrops();



    }

    void Start()
    {
        SpawnCustomer();

    }

    public void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerObject);
        customer.transform.position = startPos.position;

        CustomerManager newCustomer = customer.GetComponent<CustomerManager>();
        newCustomer.SetPositions(grocerPos, avoidPos);
        newCustomer.isOrdered = false;

    }




    public void BuyCrop(GameObject currentObject, GameObject area)
    {
        Crop currentCrop = currentObject.GetComponent<GardenManager>().crop;
        if (coin >= currentCrop.buyCost)
        {
            coin -= currentCrop.buyCost;
            currentObject.SetActive(true);
            Destroy(area);
        }

    }

    public void UpgradeCapacity(string info)
    {

        if (info == "player" && coin >= upgradePlayerCapCost)
        {
            Inventory.Instance.capacity += 5;
            coin -= upgradePlayerCapCost;
            upgradePlayerCapCost += 150;
        }


        else if (info == "stand" && coin >= upgradeStandCapCost)
        {
            coin -= upgradeStandCapCost;
            upgradeStandCapCost += 150;
            StandManager.Instance.capacity += 10;
        }



    }


    public void UpgradeCropOrderCost(GardenManager gardenManager)
    {        
        if (coin >= gardenManager.crop.upgradeCost)
        {            
            gardenManager.crop.orderCost += 5;
            coin -= gardenManager.crop.upgradeCost;
            if (gardenManager.crop.growTime > 1)
            {
                gardenManager.crop.growTime -= 1;
                gardenManager.growProgressBar.maxValue=gardenManager.crop.growTime;
            }
            gardenManager.crop.upgradeCost += 100;

        }

    }




}
