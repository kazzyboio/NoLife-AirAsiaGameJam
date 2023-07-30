using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQObjectsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pointer, radius;
    private bool soundTrigger = true;

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
            AudioTrigger();
            soundTrigger = false;

            pointer.SetActive(true);
        }
        else
        {
            soundTrigger = true;

            pointer.SetActive(false);
        }
    }

    private void AudioTrigger()
    {
        if (soundTrigger)
        {
            AudioManager.instance.Play("RefillReq");
        }
    }
}
