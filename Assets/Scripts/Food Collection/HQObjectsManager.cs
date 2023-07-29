using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQObjectsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pointer, radius;

    private void Update()
    {
        //shows HQ pickup radius when you don't have max food in inventory
        if (FoodInventory.instance.foodCount >= FoodInventory.instance.maxFoodCount)
        {
            radius.SetActive(false);
        }
        else
        { 
            radius.SetActive(true);
        }

        //shows HQ quest pointer when you 0 food in inventory
        if (FoodInventory.instance.foodCount <= 0)
        {
            pointer.SetActive(true);
        }
        else
        {
            pointer.SetActive(false);
        }
    }
}
