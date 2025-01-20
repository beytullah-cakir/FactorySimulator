using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{

    public TextMeshProUGUI cropDetails;
    private string cropName;
    private int cropCount;

    void Start()
    {
        RandomCrops();
    }

    private void RandomCrops()
    {
        int rnd = Random.Range(0, GameManager.Instance.GetCurrentCrops().Count); 
        cropName = GameManager.Instance.GetCurrentCrops()[rnd].productName;
        cropCount = Random.Range(1, 8);
        ShowCrops();

    }

    void ShowCrops()
    {
        cropDetails.text = cropName + " " + cropCount + " adet";
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GreenGrocer"))
        {
            Debug.Log("Oyuncu müşterinin trigger alanına girdi.");
            RandomCrops();
        }
    }
}
