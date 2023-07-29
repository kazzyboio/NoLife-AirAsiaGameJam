using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollect : MonoBehaviour
{
    [SerializeField]
    private float pickupTimer;

    private float currentPickupTimer;
    private bool countdown;

    private void Start()
    {
        currentPickupTimer = pickupTimer;
    }

    private void Update()
    {
        Debug.Log(countdown);

        if (countdown && FoodInventory.instance.foodCount < FoodInventory.instance.maxFoodCount)
        { 
            currentPickupTimer -= Time.deltaTime;
        }

        if (currentPickupTimer <= 0f)
        {
            FoodInventory.instance.FoodCollected();
            currentPickupTimer = pickupTimer;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && FoodInventory.instance.foodCount < FoodInventory.instance.maxFoodCount)
        {
            countdown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            countdown = false;
            currentPickupTimer = pickupTimer;
        }
    }
}
