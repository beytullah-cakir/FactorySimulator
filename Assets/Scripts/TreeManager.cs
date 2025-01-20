using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    private float currentGrowTime = 0f; // Mevcut büyüme zamanı
    public bool isFullyGrown = false; // Ekinin tamamen büyüyüp büyümediğini kontrol eder
    public Slider growProgressBar; // Büyüme durumunu göstermek için bir slider (UI elementi)

    public int cropCount = 0; // Ekin sayısı

    public Crop crop; // Ekinin özelliklerini içeren Crop scriptable object

    CropInventory cropInventory;

    void Start()
    {
        if (growProgressBar != null)
        {
            growProgressBar.maxValue = crop.growTime;
            growProgressBar.value = currentGrowTime;
        }
        //Ürün envanteri oluştur
        cropInventory= CropInventory.Instance;
    }

    void Update()
    {
        // Eğer ekin tamamen büyümediyse büyüt
        if (!isFullyGrown)
        {
            Grow();
        }
    }

    public void Grow()
    {
        // Eğer büyüme tamamlanmadıysa zamanlayıcıyı çalıştır
        if (currentGrowTime < crop.growTime)
        {
            currentGrowTime += Time.deltaTime;

            // Eğer bir büyüme çubuğu (slider) varsa güncelle
            if (growProgressBar != null)
            {
                growProgressBar.value = currentGrowTime;
            }

            // Eğer büyüme süresi tamamlandıysa, büyümeyi tamamla
            if (currentGrowTime >= crop.growTime)
            {
                isFullyGrown = true;
                
            }
        }
    }

    public void Harvest()
    {
        if (isFullyGrown)
        {
            // Ekin sayısını artır
            cropInventory.AddProduct(crop.productName, cropCount);
            // Ekin sayısını sıfırla
            currentGrowTime = 0f;
            // Ekinin büyüme durumunu sıfırla
            isFullyGrown = false;

            // Slider'ı sıfırla
            if (growProgressBar != null)
            {
                growProgressBar.value = currentGrowTime;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Eğer ekin tamamen büyüdüyse hasat et
            Harvest();
        }
    }
}
