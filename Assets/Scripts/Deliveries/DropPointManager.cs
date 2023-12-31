using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointManager : MonoBehaviour
{
    public static DropPointManager instance;

    [HideInInspector]
    public bool noEmptyPoints = false;

    [SerializeField]
    private List<GameObject> DropOffPoints = new List<GameObject>(), Customers = new List<GameObject>();
    [SerializeField]
    private GameObject questPointerPrefab, deliveryRequestPrefab;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private float startSpawnSpeed, minSpawnSpeed, speedIncreaseAmount, firstSpawn;
    private float spawnSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (GameObject drop in GameObject.FindGameObjectsWithTag("Dropoff"))
        {
            DropOffPoints.Add(drop);
        }
    }

    void Start()
    {
        spawnSpeed = Mathf.Clamp(startSpawnSpeed, minSpawnSpeed, startSpawnSpeed);

        InvokeRepeating("SpawnDropOff", firstSpawn, spawnSpeed);
    }

    public void IncreaseSpeed()
    {
        CancelInvoke("SpawnDropOff");

        spawnSpeed -= speedIncreaseAmount;
        spawnSpeed = Mathf.Clamp(spawnSpeed, minSpawnSpeed, startSpawnSpeed);

        InvokeRepeating("SpawnDropOff", spawnSpeed, spawnSpeed);
    }

    private void SpawnDropOff()
    {
        //randomize spawnpoint of order
        int ranDropNum = Random.Range(0, DropOffPoints.Count);

        //check if there are available dropoff points (not on cooldown/doesn't already have an order)
        for (int i = 0; i < DropOffPoints.Count; i++)
        {
            if (!DropOffPoints[i].GetComponent<DropPointBehaviour>().hasOrder && !DropOffPoints[i].GetComponent<DropPointBehaviour>().onCooldown)
            {
                break;
            }

            if (i == DropOffPoints.Count - 1)
            {
                noEmptyPoints = true;
                Debug.Log("No empty points!");
            }
        }

        //re-randomize drop off point if current point already has order/is on cooldown
        while ((DropOffPoints[ranDropNum].GetComponent<DropPointBehaviour>().hasOrder || DropOffPoints[ranDropNum].GetComponent<DropPointBehaviour>().onCooldown) && !noEmptyPoints)
        {
            ranDropNum = Random.Range(0, DropOffPoints.Count);
        }

        if (!noEmptyPoints)
        {
            AudioManager.instance.Play("DeliverReq");

            int ranCustNum = Random.Range(0, Customers.Count);

            GameObject thisPointer = Instantiate(questPointerPrefab, transform.position, Quaternion.identity);
            thisPointer.transform.SetParent(canvas.transform);
            thisPointer.transform.SetAsFirstSibling();
            thisPointer.transform.localScale = Vector3.one;
            thisPointer.GetComponent<QuestPointer>().target = DropOffPoints[ranDropNum].transform;

            GameObject thisRequest = Instantiate(deliveryRequestPrefab, DropOffPoints[ranDropNum].transform.position, Quaternion.identity);
            thisRequest.GetComponent<DeliveryOrder>().attachedPoint = DropOffPoints[ranDropNum];
            thisRequest.GetComponent<DeliveryOrder>().attachedPointer = thisPointer;

            GameObject thisCustomer = Instantiate(Customers[ranCustNum], DropOffPoints[ranDropNum].transform.position, Quaternion.identity);
            thisRequest.GetComponent<DeliveryOrder>().attachedCustomer = thisCustomer;

            DropOffPoints[ranDropNum].GetComponent<DropPointBehaviour>().hasOrder = true;

            IncreaseSpeed();
        }
    }
}
