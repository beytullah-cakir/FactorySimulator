using System.Collections.Generic;
using UnityEngine;

public class CropInventory
{
    // Singleton tasarım deseni
    private static CropInventory instance;
    public static CropInventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CropInventory();
            }
            return instance;
        }
    }

    public Dictionary<string, int> productStock = new Dictionary<string, int>();// Ürün adı ve stok miktarı

    // Ürün ekleme veya mevcut ürüne stok artırma
    public void AddProduct(string productName, int amount)
    {
        if (productStock.ContainsKey(productName))
        {
            productStock[productName] += amount;
        }
        else
        {
            productStock[productName] = amount; // Yeni ürün olarak ekle
        }

    }

    // Ürün stoktan çıkarma
    public bool RemoveProduct(string productName, int amount)
    {
        if (productStock.ContainsKey(productName) && productStock[productName] >= amount)
        {
            productStock[productName] -= amount; // Stoğu azalt

            // Stok sıfırın altına düşerse ürünü kaldır
            if (productStock[productName] <= 0)
            {
                productStock.Remove(productName);
            }
            return true; // Başarıyla çıkarıldı
        }
        else
        {
            return false; // Çıkarma işlemi başarısız
        }
    }

    // Belirli bir ürünün mevcut stok miktarını döndür
    public int GetProductCount(string productName)
    {
        if (productStock.ContainsKey(productName))
        {
            return productStock[productName];
        }
        else
        {
            return 0; // Ürün mevcut değilse 0 döndür
        }
    }

}
