using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StandManager : MonoBehaviour
{
    public static Dictionary<Crop, int> standInventory = new Dictionary<Crop, int>(); // Standa aktarılan ürünleri saymak için
    public int capacity; // Standın alabileceği maksimum ürün sayısı
    public int currentStandCount = 0; // Şu anda standda bulunan ürün sayısı

    public bool isPlayerInArea;
    public Transform standAnchor;

    public static StandManager Instance;

    AudioManager audioManager;


    void Start()
    {
        audioManager=AudioManager.Instance;
    }



    void Awake()
    {
        Instance = this;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = true;
            StartCoroutine(TransferProducts());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInArea = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator TransferProducts()
    {
        Inventory inventory = Inventory.Instance;

        if (inventory == null || inventory.GetCropList().Count == 0)
        {
            Debug.Log("Envanter boş, standa ürün verilemiyor!");
            yield break;
        }

        int transferLimit = capacity - currentStandCount;
        if (transferLimit <= 0)
        {
            yield break;
        }

        List<Crop> crops = new List<Crop>(inventory.GetCropList()); // Listeyi kopyala
        int transferred = 0;

        for (int i = crops.Count - 1; i >= 0; i--)
        {
            Crop crop = crops[i];

            if (transferred >= transferLimit || !isPlayerInArea)
                break; // Kapasite dolduysa veya oyuncu alanı terk ettiyse dur

            transferred++;
            currentStandCount++; // Standdaki ürün sayısını güncelle
            audioManager.PlaySound(audioManager.collectItem);

            if (standInventory.ContainsKey(crop))
                standInventory[crop]++;
            else
                standInventory[crop] = 1;

            // Oyuncunun sırtındaki ürünü al
            Transform cropObj = inventory.GetCropTransform();

            if (cropObj != null)
            {
                StartCoroutine(MoveCropToStand(cropObj.gameObject, transferred));
                inventory.RemoveCrop(crop); // Envanterden sil
            }

            yield return new WaitForSeconds(0.2f); // Ürünleri sırayla ekle
        }
    }


    private IEnumerator MoveCropToStand(GameObject cropObj, int index)
    {
        Vector3 startPos = cropObj.transform.position;
        Vector3 targetPos = standAnchor.position;

        float duration = 0.1f;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cropObj.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            yield return null;
        }

        Destroy(cropObj); // Standa ulaştığında ürünü yok et
    }


    public void OrderCrop(Crop crop, int count, CustomerManager customerManager)
    {
        if (standInventory.ContainsKey(crop) && count <= standInventory[crop])
        {
            audioManager.PlaySound(audioManager.payment);
            standInventory[crop] -= count;
            currentStandCount -= count;
            GameManager.Instance.coin += crop.orderCost * count;
            customerManager.isOrdered = false;

        }
    }
}
