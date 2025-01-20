using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //ürün adetlerini gösteren textler
    public TextMeshProUGUI appleCountText;
    public TextMeshProUGUI bananaCountText;

    //singleton
    public static UIManager Instance;

    CropInventory cropInventory;    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //ürün envanterini oluştur
        cropInventory = CropInventory.Instance;
    }

    private void Update()
    {
        UpdateProductCount();
    }

    public void UpdateProductCount()
    {
        //ürün adetlerini güncelle
        if (appleCountText != null && bananaCountText != null)
        {
            appleCountText.text = $"Apple: {cropInventory.GetProductCount("apple")}";
            bananaCountText.text = $"Banana: {cropInventory.GetProductCount("banana")}";
        }
    }
}
