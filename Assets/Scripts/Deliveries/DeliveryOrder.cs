using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryOrder : MonoBehaviour
{
    [HideInInspector]
    public GameObject attachedPoint, attachedPointer, attachedCustomer;

    [SerializeField]
    private float maxPatience, maxDeliveryTimer;
    [SerializeField]
    private int deliverScoreValue;

    [SerializeField]
    private GameObject player;
    private Image patienceFillImage, deliverFillImage;
    private float currentPatience, currentDeliveryTimer, patienceFillAmount, deliverFillAmount;
    private bool countdown = false, fail = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentDeliveryTimer = maxDeliveryTimer;
        currentPatience = maxPatience;
        deliverFillImage = attachedPointer.transform.GetChild(2).GetComponent<Image>();
        patienceFillImage = attachedPointer.transform.GetChild(3).GetComponent<Image>();

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

            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && FoodInventory.instance.foodCount > 0)
        {
            AudioManager.instance.Play("Delivering");
            countdown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Stop("Delivering");
            currentDeliveryTimer = maxDeliveryTimer;
            deliverFillImage.fillAmount = 0f;
            countdown = false;
        }
    }

    private void Despawn()
    {
        AudioManager.instance.Stop("Delivering");

        DropPointManager.instance.noEmptyPoints = false;
        attachedPoint.GetComponent<DropPointBehaviour>().hasOrder = false;
        attachedPoint.GetComponent<DropPointBehaviour>().TriggerCooldown();

        if (fail)
        {
            AudioManager.instance.Play("DeliverFail");

            attachedCustomer.GetComponent<Animator>().SetBool("Angry", true);

            StarRating.instance.deliverCombo = 0;
            StarRating.instance.UpdateRating(StarRating.instance.currentStars - 1);
        }
        else
        {
            AudioManager.instance.Play("Delivered");

            ScoreManager.instance.currentScore += deliverScoreValue;
            StarRating.instance.deliverCombo++;

            attachedCustomer.GetComponent<Animator>().SetBool("Happy", true);

            player.GetComponent<PlayerMovement>().ApplySpeedBoost();
            FoodInventory.instance.FoodDelivered();
        }

        Destroy(attachedPointer);
        Destroy(attachedCustomer, 1.5f);
        Destroy(gameObject);
    }
}
