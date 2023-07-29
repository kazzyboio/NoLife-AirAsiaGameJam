using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    private Rigidbody2D rb;
    private Vector2 move;
    public float normalSpeed = 5f; // normal speed 
    public float tarSlowMultiplier = 0.25f; // tar speed
    public float speedBoostMultiplier = 2f; // speed boost
    public float speedBoostDuration = 2f;

    private float currentSpeed; 
    private float speedChangeEndTime;

    public static bool isPointerDown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        //player rotation
        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, -zAxis + 180f);
    }

    private void FixedUpdate()
    {
        if (isPointerDown)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            if (Time.time < speedChangeEndTime)
            {
                rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + move * normalSpeed * Time.fixedDeltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("PowerUp")) 
        //{
        //    ApplySpeedBoost();

        //    Destroy(other.gameObject);
        //}
        
        if (other.CompareTag("Tar")) 
        {
            ApplyTarSlowdown();
        }
    }

    public void ApplySpeedBoost()
    {
        currentSpeed = normalSpeed * speedBoostMultiplier; 
        speedChangeEndTime = Time.time + speedBoostDuration; 
    }

    private void ApplyTarSlowdown()
    {
        currentSpeed = normalSpeed * tarSlowMultiplier; 
        speedChangeEndTime = Time.time + speedBoostDuration; 
    }
}
