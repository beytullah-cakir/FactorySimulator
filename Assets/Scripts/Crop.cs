using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "Farming/Crop", order = 1)]
public class Crop : ScriptableObject
{
    public string productName;
    public float growTime = 10f;
    public int level;
    public GameObject gameObject;

    public int orderCost;

    public int buyCost;

   
}
