using System.Collections.Generic;
using UnityEngine;

public class CropInventory
{
   
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
    private Dictionary<string, int> productStock = new Dictionary<string, int>();

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

    // Tüm ürünlerin stok durumunu döndür
    public Dictionary<string, int> GetAllProducts()
    {
        return new Dictionary<string, int>(productStock);
    }

    // Toplam ürün çeşitliliğini döndür (Kaç farklı ürün olduğunu)
    public int GetTotalProductTypes()
    {
        return productStock.Count;
    }

    // Toplam stok miktarını döndür (Tüm ürünlerin toplamı)
    public int GetTotalProductCount()
    {
        int total = 0;
        foreach (var product in productStock.Values)
        {
            total += product;
        }
        return total;
    }
}
