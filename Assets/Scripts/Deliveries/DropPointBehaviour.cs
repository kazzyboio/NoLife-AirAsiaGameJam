using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPointBehaviour : MonoBehaviour
{
    [HideInInspector]
    public bool hasOrder = false, onCooldown = false;
    [HideInInspector]
    public float currentCooldown;

    [SerializeField]
    private float maxCooldown;

    private void Update()
    {
        if (onCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                currentCooldown = maxCooldown;
                onCooldown = false;
            }
        }
    }

    public void TriggerCooldown()
    {
        currentCooldown = maxCooldown;
        onCooldown = true;
    }
}
