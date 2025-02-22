using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
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
    private readonly List<Crop> cropsList = new();

    public int pos = 0;



    public List<Crop> GetCropList()
    {
        return cropsList;
    }



    public void SpawnCrop(int cropCount, Crop crop, GardenManager garden)
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


            Vector3 offset = new(0, pos + 2, -1);
            pos++;
            apple.transform.localPosition = offset;
            cropsList.Add(crop);





        }
        Debug.Log($"{crop.productName} eklendi! Envanterde {cropsList.Count}/{capacity} ürün var.");

        // Ağaçtan alınan ürün miktarını azalt
        garden.currentCropCount -= appleCount;





    }


    public void DestroyCrop(int count)
    {

        for (int i = 0; i < count; i++)
        {

            Destroy(backpackAnchor.GetChild(pos - 1).gameObject);
            cropsList.RemoveAt(0);
            pos--;

        }
    }







}
