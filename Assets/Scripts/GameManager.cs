using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    //ürün adetlerini tutan değişkenler
    public int appleCount;
    public int bananaCount;

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
