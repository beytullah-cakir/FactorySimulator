using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GardenManager : MonoBehaviour
{
    public float currentGrowTime = 0f;
    public bool isFullyGrown = false;
    public Slider growProgressBar;

    public int cropCount; // Ağaçtaki toplam ürün miktarı

    public int currentCropCount;
    public Crop crop;

    Inventory cropInventory;

    public Transform spawnPoint;


    public bool isPlayerInArea = false;  // Alanın içinde olup olmadığını kontrol etmek için bir flag


    void Start()
    {
        if (growProgressBar != null)
        {
            growProgressBar.maxValue = crop.growTime;
            growProgressBar.value = currentGrowTime;
        }
        currentCropCount = cropCount;
        cropInventory = Inventory.Instance;
    }

    void Update()
    {
        if (!isFullyGrown)
        {
            Grow();
        }

       
    }

    public void Grow()
    {
        if (currentGrowTime < crop.growTime)
        {
            currentGrowTime += Time.deltaTime;

            if (growProgressBar != null)
            {
                growProgressBar.value = currentGrowTime;
            }

            if (currentGrowTime >= crop.growTime)
            {
                isFullyGrown = true;
            }
        }
    }

    public void Harvest()
    {
        if (isFullyGrown && currentCropCount > 0)
        {
            StartCoroutine(cropInventory.SpawnCropWithDelay(currentCropCount, crop, this));
            
        }
    }


    // Eğer oyuncu alanın içine girerse ürün toplayabilir
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea=true;
            Harvest();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea=false;
        }
    }


}
