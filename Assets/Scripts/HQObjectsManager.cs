using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQObjectsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pointer, radius;

    private void Update()
    {
        if (FoodInventory.instance.foodCount >= FoodInventory.instance.maxFoodCount)
        {
            pointer.SetActive(false);
            radius.SetActive(false);
        }
        else
        { 
            pointer.SetActive(true);
            radius.SetActive(true);
        }
    }
}
