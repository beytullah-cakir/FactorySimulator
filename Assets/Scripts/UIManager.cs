using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI appleCountText;
    public TextMeshProUGUI bananaCountText;

    public static UIManager Instance;

    Inventory cropInventory;

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
        cropInventory = Inventory.Instance;
    }

}
