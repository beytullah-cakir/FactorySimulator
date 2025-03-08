using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    private Transform grocerPos, avoidPos;

    public Slider waitSlider;

    public float waitTimer;
    private float currentWaitTimer = 0;

    public TextMeshProUGUI orderDeatil;

    public float speed;

    public bool isOrdered = false;


    public static int rnd, count;

    public static Crop currentCrop;

    public static CustomerManager Instance;

    private Animator anm;

    public Vector3 direction;




    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        waitSlider.maxValue = waitTimer;
        waitSlider.minValue = currentWaitTimer;

        rnd = Random.Range(0, GameManager.Instance.GetCurrentCrops().Count);
        count = Random.Range(5, 10);
        currentCrop = GameManager.Instance.GetCurrentCrops()[rnd];
        orderDeatil.text = $"{count}x {currentCrop.productName}";

        anm = GetComponent<Animator>();


    }

    public void WaitTimer()
    {
        if (currentWaitTimer < waitTimer)
        {
            currentWaitTimer += Time.deltaTime;

            if (waitSlider != null)
            {
                waitSlider.value = currentWaitTimer;
            }

            if (currentWaitTimer >= waitTimer)
            {
                isOrdered=false;
            }
        }
    }


    void Update()
    {

        if (!isOrdered)
        {
            MoveCustomer();
            speed = 8;

        }

        else if (isOrdered)
        {
            WaitTimer();
            StandManager.Instance.OrderCrop(currentCrop, count, this);
            speed = 0;
        }



    }




    void MoveCustomer()
    {

        transform.position += speed * Time.deltaTime * new Vector3(direction.x, direction.y, direction.z);
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
                isOrdered = true;

                break;
            case "End":
                GameManager.Instance.SpawnCustomer();
                Destroy(gameObject);
                break;
        }
    }



}
