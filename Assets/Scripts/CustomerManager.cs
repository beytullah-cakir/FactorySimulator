using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private Transform grocerPos, avoidPos;

    public float speed;

    public static bool isOrdered = false;
    

    int rnd, count;

    Crop currentCrop;



    void Awake()
    {
        
    }

    void Start()
    {
        grocerPos = GameManager.Instance.grocerPos;
        avoidPos = GameManager.Instance.avoidPos;

        rnd = Random.Range(0, GameManager.Instance.GetCurrentCrops().Count);
        count = Random.Range(1, 5);
        currentCrop = GameManager.Instance.GetCurrentCrops()[rnd];
        print($"müşteri {count} adet {currentCrop.productName} istiyor");

        
    }


    void Update()
    {
        if (!isOrdered) MoveToGrocer();
        else AvoidFromGrocer();

        GiveOrder();

    }




    void MoveToGrocer()
    {
        transform.position = Vector3.MoveTowards(transform.position, grocerPos.position, speed * Time.deltaTime);



    }

    void AvoidFromGrocer()
    {
        transform.position = Vector3.MoveTowards(transform.position, avoidPos.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, avoidPos.position) < 0.1f)
        {
            Destroy(gameObject);

        }

    }

    void GiveOrder()
    {
        if (Vector2.Distance(transform.position, grocerPos.position) < 0.1f)
        {

            StandManager.Instance.OrderCrop(currentCrop, count);


        }
    }
}
