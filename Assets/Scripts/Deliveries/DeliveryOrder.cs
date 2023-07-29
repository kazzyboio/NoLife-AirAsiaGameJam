using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryOrder : MonoBehaviour
{
    [HideInInspector]
    public GameObject attachedPoint, attachedPointer;

    [SerializeField]
    private float maxPatience, maxDeliveryTimer;
    [SerializeField]
    private int deliverScoreValue;

    private GameObject player;
    private Image patienceFillImage, deliverFillImage;
    private float currentPatience, currentDeliveryTimer, patienceFillAmount, deliverFillAmount;
    private bool countdown = false, fail = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentDeliveryTimer = maxDeliveryTimer;
        currentPatience = maxPatience;
        patienceFillImage = attachedPointer.transform.GetChild(0).transform.GetChild(2).GetComponent<Image>();
        deliverFillImage = attachedPointer.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();

        Invoke("Despawn", maxPatience);
    }

    private void Update()
    {
        currentPatience -= Time.deltaTime;
        patienceFillAmount = currentPatience / maxPatience;
        patienceFillImage.fillAmount = patienceFillAmount;

        if (countdown)
        {
            currentDeliveryTimer -= Time.deltaTime;
            deliverFillAmount = (maxDeliveryTimer - currentDeliveryTimer) / maxDeliveryTimer;
            deliverFillImage.fillAmount = deliverFillAmount;
        }

        if (currentDeliveryTimer <= 0f)
        {
            fail = false;
            player.GetComponent<PlayerMovement>().ApplySpeedBoost();
            ScoreManager.instance.currentScore += deliverScoreValue;

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
            currentDeliveryTimer = maxDeliveryTimer;
            countdown = false;
        }
    }

    private void Despawn()
    {
        DropPointManager.instance.noEmptyPoints = false;
        attachedPoint.GetComponent<DropPointBehaviour>().hasOrder = false;
        attachedPoint.GetComponent<DropPointBehaviour>().TriggerCooldown();

        if (fail)
        {
            StarRating.instance.UpdateRating(StarRating.instance.currentStars - 1);
        }
        else
        {
            StarRating.instance.UpdateRating(StarRating.instance.currentStars + 1);
            FoodInventory.instance.FoodDelivered();
        }

        Destroy(attachedPointer);
        Destroy(gameObject);
    }
}
