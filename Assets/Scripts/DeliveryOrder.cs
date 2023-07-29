using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryOrder : MonoBehaviour
{
    [SerializeField]
    private float patienceTimer, deliveryTimer;
    [HideInInspector]
    public GameObject attachedPoint, attachedPointer;

    private float currentDeliveryTimer;
    private bool countdown = false;
    private bool fail = true;

    private void Start()
    {
        currentDeliveryTimer = deliveryTimer;

        Invoke("Despawn", patienceTimer);
    }

    private void Update()
    {
        if (countdown)
        {
            currentDeliveryTimer -= Time.deltaTime;
        }

        if (currentDeliveryTimer <= 0f)
        {
            fail = false;

            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && FoodInventory.instance.foodCount > 0)
        {
            countdown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            currentDeliveryTimer = deliveryTimer;
            countdown = false;
        }
    }

    private void Despawn()
    {
        DropPointManager.instance.noEmptyPoints = false;
        attachedPoint.GetComponent<DropPointBehaviour>().hasOrder = false;

        if (fail)
        {
            StarRating.instance.UpdateRating(StarRating.instance.currentStars - 1);
        }
        else
        {
            StarRating.instance.UpdateRating(StarRating.instance.currentStars + 1);
            FoodInventory.instance.foodCount--;
        }

        Destroy(attachedPointer);
        Destroy(gameObject);
    }
}
