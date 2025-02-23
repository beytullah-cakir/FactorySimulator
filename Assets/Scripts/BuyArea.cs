using UnityEngine;

public class BuyArea : MonoBehaviour
{
    public GameObject currentObject;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.BuyCrop(currentObject,this.gameObject);
        }
    }



}
