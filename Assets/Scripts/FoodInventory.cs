using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInventory : MonoBehaviour
{
    public static FoodInventory instance;

    [HideInInspector]
    public int foodCount = 0;

    public int maxFoodCount;

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
    }

    private void Update()
    {
        Debug.Log(foodCount);
    }

    public void FoodCollected()
    {
        foodCount++;
    }
}
