using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    // Singleton
    public static Inventory Instance;

    void Awake()
    {
        Instance = this;
    }

    public Transform backpackAnchor; // Elmaların sırtında dizileceği nokta
    public int capacity = 5;
    public static List<Crop> cropsList = new List<Crop>();



    public List<Crop> GetCropList()
    {
        return cropsList;
    }



    public void SpawnApple(int cropCount, Crop crop, GardenManager garden)
    {
        int appleCount = Mathf.Min(cropCount, capacity - cropsList.Count); // Kapasiteyi aşmamak için kontrol

        if (appleCount <= 0)
        {
            Debug.Log("Envanter dolu! Daha fazla ürün alamazsın.");
            return;
        }

        for (int i = 0; i < appleCount; i++)
        {

            GameObject apple = Instantiate(crop.gameObject, backpackAnchor.position, Quaternion.identity);
            apple.transform.SetParent(backpackAnchor);

            // Elmalar üst üste dizilir
            Vector3 offset = new Vector3(0, cropsList.Count * 0.5f, 0);
            apple.transform.localPosition = offset;

            cropsList.Add(crop);
        }
        Debug.Log($"{crop.productName} eklendi! Envanterde {cropsList.Count}/{capacity} ürün var.");

        // Ağaçtan alınan ürün miktarını azalt
        garden.currentCropCount -= appleCount;





    }


    public void DestroyProduct(int count)
    {
        

        for (int i = 0; i <count; i++) // Belirlenen kadar obje yok et
        {
            if (backpackAnchor.childCount > 0)
            {
                ;
                Destroy(backpackAnchor.GetChild(i).gameObject);
            }
        }
    }







}
