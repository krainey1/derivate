using System.Data.Common;
using UnityEngine;
//I love vectors
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public float detectionRadius = 10000f;
    public float moveSpeed       = 6.5f;
    public float dampTime        = 0.1f;

    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb   = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 targetPos  = player.position;
        Vector2 delta = targetPos - currentPos;
        float overlapZone = 2f;
        float dist = delta.magnitude;

        if (dist <= detectionRadius && dist > overlapZone)
        {
            //movin towards player 
            Vector2 newPos = Vector2.MoveTowards(currentPos, targetPos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

           //normalize direction
            Vector2 moveInput = delta.normalized;

            anim.SetFloat("Horizontal", moveInput.x, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Vertical", moveInput.y, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Speed", moveInput.sqrMagnitude);
        }
        else
        {
            // Out of range, get into idle
            anim.SetFloat("Horizontal", 0f, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Vertical", 0f, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Speed", -0.02f);
        }
    }
}


