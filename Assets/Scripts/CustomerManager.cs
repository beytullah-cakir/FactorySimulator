using System.Linq.Expressions;
using TMPro;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private Transform grocerPos, avoidPos;

    public TextMeshProUGUI orderDeatil;

    public float speed;

    public bool isOrdered = false;


    public static int rnd, count;

    public static Crop currentCrop;

    public static CustomerManager Instance;

    private Animator anm;

    public Vector3 direction = Vector3.left;




    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rnd = Random.Range(0, GameManager.Instance.GetCurrentCrops().Count);
        count = Random.Range(5, 10);
        currentCrop = GameManager.Instance.GetCurrentCrops()[rnd];
        orderDeatil.text = $"{count}x {currentCrop.productName}";

        anm = GetComponent<Animator>();


    }


    void Update()
    {
        if (!isOrdered)
        {
            MoveToGrocer();
        }

        else AvoidFromGrocer();


    }




    void MoveToGrocer()
    {
        //transform.position = Vector3.MoveTowards(transform.position, grocerPos.position, speed * Time.deltaTime);
        transform.position += speed * Time.deltaTime * new Vector3(direction.x, 0, direction.z);        
        anm.SetBool("Idle", false);
    }

    void AvoidFromGrocer()
    {
        transform.position = Vector3.MoveTowards(transform.position, avoidPos.position, speed * Time.deltaTime);
        anm.SetBool("Idle", false);

    }

    public void SetPositions(Transform grocer, Transform avoid)
    {
        grocerPos = grocer;
        avoidPos = avoid;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Stand":
                anm.SetBool("Idle", true);
                StandManager.Instance.OrderCrop(currentCrop, count);
                break;
            case "End":
                GameManager.Instance.SpawnCustomer();
                Destroy(gameObject);
                break;
        }
    }



}
