using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    // Ürünleri satma süresi (bir ürün ne kadar sürede satılacak)
    public float sellTime = 5f;

    // Dükkanın istediği ürünler
    public Dictionary<string, int> requiredCrops = new Dictionary<string, int>();

    // Dükkanın kendi envanteri
    private Dictionary<string, int> buildInventory = new Dictionary<string, int>();

    private void Start()
    {
        // Dükkanın başlangıçta istediği ürünleri tanımla
        requiredCrops.Add("apple", 5);
        requiredCrops.Add("banana", 3);

        Debug.Log("Dükkan şu ürünleri istiyor:");
        foreach (var crop in requiredCrops)
        {
            Debug.Log($"{crop.Key}: {crop.Value} adet");
        }
    }

    // Oyuncudan ürün alacak
    public void DeliverCrops(Dictionary<string, int> playerInventory)
    {
        foreach (var requiredCrop in requiredCrops)
        {
            string productName = requiredCrop.Key;
            int requiredAmount = requiredCrop.Value;

            // Eğer oyuncunun envanterinde ürün varsa
            if (playerInventory.ContainsKey(productName))
            {
                int playerAmount = playerInventory[productName];
                int deliverAmount = Mathf.Min(playerAmount, requiredAmount); // Teslim edilecek miktar

                if (deliverAmount > 0)
                {
                    // Dükkanın envanterine ekle
                    if (buildInventory.ContainsKey(productName))
                    {
                        buildInventory[productName] += deliverAmount;
                    }
                    else
                    {
                        buildInventory[productName] = deliverAmount;
                    }

                    // Oyuncunun envanterinden çıkar
                    playerInventory[productName] -= deliverAmount;

                    Debug.Log($"{deliverAmount} adet {productName} teslim edildi.");
                }
            }
            else
            {
                Debug.LogWarning($"{productName} oyuncunun envanterinde yok!");
            }
        }

        
    }


    // Satış başlatma
    public void StartSelling()
    {
        if (buildInventory.Count > 0)
        {
            StartCoroutine(SellCropsCoroutine());
        }
        else
        {
            Debug.Log("Dükkanda satılacak ürün yok!");
        }
    }

    // Satış işlemi
    private void SellCrops()
    {
        foreach (var product in buildInventory)
        {
            Debug.Log($"{product.Key} satıldı: {product.Value} adet.");
        }

        buildInventory.Clear(); // Tüm ürünler satıldıktan sonra envanteri temizle
        Debug.Log("Dükkanın tüm ürünleri satıldı!");
    }

    private IEnumerator SellCropsCoroutine()
    {
        Debug.Log("Satış işlemi başladı...");
        yield return new WaitForSeconds(sellTime);
        SellCrops();
    }
}
