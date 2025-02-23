using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GardenManager : MonoBehaviour
{
    private float currentGrowTime = 0f;
    public bool isFullyGrown = false;
    public Slider growProgressBar;

    public int cropCount; // Ağaçtaki toplam ürün miktarı

    public int currentCropCount;
    public Crop crop; 

    Inventory cropInventory;

    

    void Start()
    {
        if (growProgressBar != null)
        {
            growProgressBar.maxValue = crop.growTime;
            growProgressBar.value = currentGrowTime;
        }
        currentCropCount=cropCount;
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
            cropInventory.SpawnCrop(currentCropCount, crop, this);

            // Eğer hala ürün kaldıysa büyümeyi sıfırlama
            if (currentCropCount > 0)
            {
                return;
            }
            else
            {
                currentCropCount=cropCount;
                isFullyGrown = false;
                currentGrowTime = 0;

                if (growProgressBar != null)
                {
                    growProgressBar.value = currentGrowTime;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Harvest();
        }
    }

    
}
