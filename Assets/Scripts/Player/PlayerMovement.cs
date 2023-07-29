using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 move;
    private Vector2 velocity;

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
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //move.x = joystick.Horizontal;
        //move.y = joystick.Vertical;

        //player rotation
        //float hAxis = move.x;
        //float vAxis = move.y;
        //float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0f, 0f, -zAxis + 180f);

        float hAxis = joystick.Horizontal;
        float vAxis = joystick.Vertical;

        Vector2 movementDirection = new Vector2(hAxis, vAxis).normalized;
        rb.velocity = movementDirection * normalSpeed;

        float angle = Mathf.Atan2(vAxis, hAxis) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360f;
        }
        if (angle >= 45f && angle < 135f)
        {
            animator.Play("Up");
        }
        else if (angle >= 135f && angle < 225f)
        { 
            animator.Play("Left");
        }   
        else if (angle >= 225f && angle< 315f)
        {
            animator.Play("Down");
        }
        else
        {
            animator.Play("Right");
            }
    }

    private void FixedUpdate()
    {
        if (isPointerDown)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            // momentum movement 1
            Vector2 move = new Vector2(joystick.Horizontal, joystick.Vertical);
            rb.AddForce(move * currentSpeed, ForceMode2D.Force);

            // momentum movement 2
            //velocity += move.normalized * currentSpeed * Time.fixedDeltaTime;
            //rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            //velocity *= 0.9f;

            // original movement
            //if (Time.time < speedChangeEndTime)
            //{
            //    rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
            //}
            //else
            //{
            //    rb.MovePosition(rb.position + move * normalSpeed * Time.fixedDeltaTime);
            //}
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
