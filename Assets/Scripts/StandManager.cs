using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StandManager : MonoBehaviour
{
    public static Dictionary<Crop, int> standInventory = new Dictionary<Crop, int>(); // Standa aktarılan ürünleri saymak için
    public int capacity; // Standın alabileceği maksimum ürün sayısı
    public int currentStandCount = 0; // Şu anda standda bulunan ürün sayısı

    public static StandManager Instance;

    

    void Awake()
    {
        Instance = this;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TransferProducts();
        }
    }

    void TransferProducts()
    {
        Inventory inventory = Inventory.Instance;

        if (inventory == null || inventory.GetCropList().Count == 0)
        {
            Debug.Log("Envanter boş, standa ürün verilemiyor!");
            return;
        }

        // Standın kalan kapasitesini hesapla
        int transferLimit = capacity - currentStandCount;
        if (transferLimit <= 0)
        {
            return;
        }

        List<Crop> crops = new List<Crop>(inventory.GetCropList()); // Listeyi kopyala
        int transferred = 0;

        foreach (Crop crop in crops)
        {
            if (transferred >= transferLimit)
                break; // Kapasite dolduysa dur

            
            transferred++;
            currentStandCount++; // Standdaki ürün sayısını güncelle

            if (standInventory.ContainsKey(crop))
                standInventory[crop]++;
            else
                standInventory[crop] = 1;
        }

        // Sadece kapasite kadar fiziksel objeyi yok et
        inventory.DestroyCrop(transferred);

        
        
    }


    public void OrderCrop(Crop crop, int count)
    {
        if (standInventory.ContainsKey(crop) && count<=standInventory[crop])
        {
            for (int i = 0; i < count; i++)
            {
                standInventory[crop]--;
                currentStandCount--;
                GameManager.Instance.coin+=crop.orderCost;
            }
                    
            CustomerManager.Instance.isOrdered=true;
        }
    }
}
