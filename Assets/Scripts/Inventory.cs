using System.Collections;
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

    public int currentAmount = 0;



    public List<Crop> GetCropList()
    {
        return cropsList;
    }
    public Transform GetCropTransform()
    {
        if (backpackAnchor.childCount > 0)
        {
            return backpackAnchor.GetChild(backpackAnchor.childCount-1); // İlk ürünü al
        }
        return null;
    }
    public void RemoveCrop(Crop crop)
    {
        if (cropsList.Contains(crop))
        {
            cropsList.Remove(crop);
            currentAmount--;
        }
    }

    public IEnumerator SpawnCropWithDelay(int cropCount, Crop crop, GardenManager garden)
    {
        int currentCropCount = Mathf.Min(cropCount, capacity - cropsList.Count); // Kapasiteyi aşmamak için kontrol

        if (currentCropCount <= 0)
        {
            Debug.Log("Envanter dolu! Daha fazla ürün alamazsın.");
            yield break;
        }

        while(currentCropCount>0 && garden.isPlayerInArea)
        {
            GameObject currentObj = Instantiate(crop.gameObject, garden.transform.localPosition, Quaternion.identity);
            currentObj.transform.SetParent(backpackAnchor);
            StartCoroutine(MoveCropToBackpack(currentObj));
            cropsList.Add(crop);
            garden.currentCropCount--;
            yield return new WaitForSeconds(0.2f); // 0.1 saniye arayla ekle
            currentCropCount--;
        }

        if (garden.currentCropCount == 0)
        {
            garden.currentCropCount=garden.cropCount;
            garden.isFullyGrown = false;
            garden.currentGrowTime = 0;

            if (garden.growProgressBar != null)
            {
                garden.growProgressBar.value = 0;
            }
        }

    }
    private IEnumerator MoveCropToBackpack(GameObject currentObj)
    {
        Vector3 startPos = currentObj.transform.position;
        Vector3 targetPos = backpackAnchor.localPosition + new Vector3(0, currentAmount +2, -1);
        float duration = 0.1f; // Hareket süresi
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentObj.transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            yield return null;
        }

        // Hedefe ulaştıktan sonra çantaya ekle
        currentObj.transform.SetParent(backpackAnchor);
        currentAmount++;
    }




    public void DestroyCrop(int count)
    {

        for (int i = 0; i < count; i++)
        {

            Destroy(backpackAnchor.GetChild(currentAmount - 1).gameObject);
            cropsList.RemoveAt(0);
            currentAmount--;

        }
    }







}
