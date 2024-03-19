using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerContorler : MonoBehaviour
{

    private Collision coll;
    public Rigidbody2D  rb;


    public float speed = 7;
    public float jumpForce = 7;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public int score;
    private bool isGrounded;
    private bool controlable;

 


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();

        score = 0;
    

        isGrounded = true;
        controlable = true;
    }

    // Update is called once per frame
    void Update()
    {
        

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        {walk(dir);
        

        if (Input.GetButtonDown("Jump") && isGrounded && controlable)
        {
            jump(Vector2.up);
            isGrounded = false;
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        
        
    }

    private void walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void jump(Vector2 dir)
    {
    rb.velocity = (new Vector2(rb.velocity.x, 0));
    rb.velocity = (dir * jumpForce);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        // if (col.gameObject.tag == "Wall"){
        //     rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
        // }
        
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0,0);
        
        controlable = false;
        
    }


}
