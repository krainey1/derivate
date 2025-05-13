using System.Data.Common;
using System.Collections;
using UnityEngine;
//I love vectors
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public PlayerMovement p1;

    public float detectionRadius = 10f;
    public float moveSpeed       = 8f;
    public float dampTime        = 0.1f;
    public float separationRadius = 0.8f;
    public float repulsionStrength = 10f;

    private Vector2 offset;

    private Rigidbody2D rb;
    private Animator anim;

    void Awake() //initialize when app starts
    {
        rb   = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        offset = Random.insideUnitCircle * 1.5f;
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 targetPos  = player.position;
        targetPos += offset; //aim at slightly dif areas trying to avoid bottleneck
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
        else if(dist <= overlapZone)
        { 
            float health = p1.getcurrhealth();
            health -= 0.05f;
            p1.setcurrhealth(health); //start the slow drain
            //do the anim thing but for rn just slip into idle
            StartCoroutine(FlashRed(p1.GetComponent<SpriteRenderer>(), 0.2f));
            anim.SetFloat("Horizontal", 0f, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Vertical", 0f, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Speed", -0.02f);
        }
        else
        {
            // Out of range, get into idle
            anim.SetTrigger("Idle");
            anim.SetFloat("Horizontal", 0f, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Vertical", 0f, dampTime, Time.fixedDeltaTime);
            anim.SetFloat("Speed", -0.02f);
        }
        SeparateEnemies();
    }

    public IEnumerator FlashRed(SpriteRenderer sr, float duration)
    {
        sr.color = new Color(1f, 0.4f, 0.4f); // red
        yield return new WaitForSeconds(duration);
        sr.color = Color.white; // reset 
    }
    void SeparateEnemies()
    {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, separationRadius);
        foreach (Collider2D other in nearby)
        {
            if (other == null || other.gameObject == this.gameObject) continue;
            if (!other.CompareTag("Enemy")) continue;

            Vector2 dir = (Vector2)(transform.position - other.transform.position);
            float dist = dir.magnitude;
            if (dist == 0) continue;

            // Repel slightly to avoid overlap
            Vector2 repel = dir.normalized * (repulsionStrength * Time.fixedDeltaTime);
            transform.position += (Vector3)repel;
        }
        
    }
}


