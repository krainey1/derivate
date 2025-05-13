
using UnityEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
//credit where credit is due I got most of this from a brackeys tutorial
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4f;

    public Rigidbody2D rb;

    public float maxHealth = 100;
    public float currentHealth = 0;
    public healthbar health;
    public Animator animator;
    Vector2 movement; //horizontal and vertical components ayo

    void Start()
    {
        currentHealth = maxHealth;
        health.SetMaxHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        health.SetHealth(currentHealth); //keep updating the bar as we ago along
        movement.x = Input.GetAxisRaw("Horizontal"); //left arrow -1 rightarrow 1 or as
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    public void setcurrhealth(float health, int choice)
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        if(choice == 1){
            StartCoroutine(FlashRed(s, 0.2f));
        }
        currentHealth = health;
    }

    public float getcurrhealth()
    {
        return currentHealth;
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //constant movement speed
    }

    private void OnCollisionStay2D(Collision2D collision) 
    {
        rb.linearVelocity = Vector2.zero;
    }

    public IEnumerator FlashRed(SpriteRenderer sr, float duration)
    {
        sr.color = new Color(1f, 0.4f, 0.4f); // red
        yield return new WaitForSeconds(duration);
        sr.color = Color.white; // reset 
    }
}
