using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 1;
    public List<Crop> crops = new List<Crop>();

    //singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    private List<Crop> currentCropList = new List<Crop>();


    public List<Crop> GetCurrentCrops()
    {
        return currentCropList;
    }
    
    public void ManageCrops()
    {
        foreach (var crop in crops)
        {
            if (crop.level <= currentLevel && !currentCropList.Contains(crop))
            {
                currentCropList.Add(crop);
            }
        }
    }

    void Update()
    {
        ManageCrops();
        
        
    }
}
