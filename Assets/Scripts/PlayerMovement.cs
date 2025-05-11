
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 7f;

    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 prev;

    Vector2 movement; //horizontal and vertical components ayo

    void Start()
    {
        prev = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        prev = rb.position;
        movement.x = Input.GetAxisRaw("Horizontal"); //left arrow -1 rightarrow 1 or as
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

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
}
