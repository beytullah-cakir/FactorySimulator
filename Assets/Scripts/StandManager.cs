using System.Collections.Generic;
using UnityEngine;

public class StandManager : MonoBehaviour
{
    Dictionary<Crop, int> transferredProducts = new Dictionary<Crop, int>(); // Standa aktarılan ürünleri saymak için
    public int capacity; // Standın alabileceği maksimum ürün sayısı
    private int currentStandCount = 0; // Şu anda standda bulunan ürün sayısı

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
            Debug.Log("Stand dolu! Daha fazla ürün eklenemez.");
            return;
        }

        List<Crop> crops = new List<Crop>(inventory.GetCropList()); // Listeyi kopyala
        int transferred = 0;

        foreach (Crop crop in crops)
        {
            if (transferred >= transferLimit)
                break; // Kapasite dolduysa dur

            inventory.GetCropList().RemoveAt(0); // Envanterden kaldır
            transferred++;
            currentStandCount++; // Standdaki ürün sayısını güncelle

            if (transferredProducts.ContainsKey(crop))
                transferredProducts[crop]++;
            else
                transferredProducts[crop] = 1;
        }

        // Sadece kapasite kadar fiziksel objeyi yok et
        inventory.DestroyProduct(transferred);

        // Konsola hangi ürünlerin aktarıldığını yaz
        foreach (var product in transferredProducts)
        {
            Debug.Log($"Standdaki ürün sayısı: {product.Key.productName} x{product.Value}");
        }
    }
}
