using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public GameObject customerPrefab;
    public List<Transform> queuePoints = new List<Transform>();

    private List<GameObject> customerQueue = new List<GameObject>();

    public Transform target;

    private bool isMoving = false;

    private GameObject firstCustomer;

    private void Start()
    {
        AddCustomer();
    }

    public void AddCustomer()
    {
        foreach (var queuePoint in queuePoints)
        {

            GameObject customer = Instantiate(customerPrefab, queuePoint.position, Quaternion.identity);
            customerQueue.Add(customer);
        }
    }

    public void RemoveCustomer()
    {
        if (customerQueue.Count > 0)
        {
            firstCustomer = customerQueue.First();
            customerQueue.RemoveAt(0);

        }

    }

    void AlignCustomer()
    {
        foreach (var customer in customerQueue)
        {
            customer.transform.position = Vector3.MoveTowards(customer.transform.position, queuePoints[customerQueue.IndexOf(customer)].position, 2f * Time.deltaTime);
        }
    }

    void MoveToForward(GameObject customer)
    {
        customer.transform.position = Vector3.MoveTowards(customer.transform.position, target.position, 2f * Time.deltaTime);

        if (Vector3.Distance(customer.transform.position, target.position) < 0.1f)
        {
            customerQueue.Add(customer);
            customer.transform.position = queuePoints[queuePoints.Count - 1].position;
            isMoving = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveCustomer();
            isMoving = true;
        }
        AlignCustomer();
        if (isMoving)
            MoveToForward(firstCustomer);
    }


}
