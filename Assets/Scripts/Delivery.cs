using UnityEngine;

public class Delivery : MonoBehaviour
{
    private Build build;
    CropInventory cropInventory = CropInventory.Instance;

    private void Start()
    {
        build = GetComponentInParent<Build>(); // Bağlı olduğu bina
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu dükkanın trigger alanına girdi.");
            build.DeliverCrops(cropInventory.productStock); // Ürünleri teslim et
        }
    }
}
