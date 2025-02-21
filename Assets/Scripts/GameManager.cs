using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //mevcut level
    public int currentLevel = 1;

    //singleton
    public static GameManager Instance;
    //mevcut ürünlerin listesi
    private readonly List<Crop> currentCrops=new();


    //ürünlere ait olan bahçe veya ağaçların listesi
    public List<GameObject> crops;

    void Awake()
    {
        Instance = this;
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
            if (_crop.level <= currentLevel && !currentCrops.Contains(_crop))
            {
                currentCrops.Add(_crop);
                crop.SetActive(true);
            }
        }
    }


    public bool IsActive(Crop crop)
    {
        return crop.level <= currentLevel;
    }



    void Update()
    {
        ManageCrops();
        if (Input.GetKeyDown(KeyCode.Space)) currentLevel++;

    }
}
