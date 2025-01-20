using UnityEngine;

public class CropBasket : MonoBehaviour
{
    public int maxCropCount = 10;//sepetin alabileceği maksimum ürün sayısı
    public int currentCropCount = 0;//sepetin içindeki ürün sayısı

    //sepete ürün eklemek için kullanılan fonksiyon
    public void AddCrop()
    {
        if (currentCropCount < maxCropCount)
        {
            currentCropCount++;
        }
    }
    
    //sepetten ürün çıkarmak için kullanılan fonksiyon
    public void RemoveCrop()
    {
        if (currentCropCount > 0)
        {
            currentCropCount--;
        }
    }
}
