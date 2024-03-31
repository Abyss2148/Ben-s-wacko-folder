using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerContorler : MonoBehaviour
{

    public LayerMask groundLayer;
    public LayerMask creatureLayer;


    public Rigidbody2D  rb;

    public Vector2 bottomOffset, rightOffset, leftOffset;


    public float speed = 7;
    public float jumpForce = 7;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float collisionRadius = 0.25f;
    public float slideSpeed = 5;


    private bool isGrounded;
    private bool onWall;
    private bool onRightWall;
    private bool onLeftWall;
    private bool wallSlide;
    private bool isDJumped;
    private bool enemyContact;

    private int wallSide;
    

 


    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        

        
    

        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) 
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        enemyContact = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, creatureLayer) 
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, creatureLayer);

        wallSide = onRightWall ? -1 : 1;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        walk(dir);

        if (!onWall || isGrounded)
        {
            wallSlide = false;
            
        }

        if (enemyContact){
            //player damage/death logic
            DestroyObject(gameObject);
        }

        if (isGrounded)
        {
            isDJumped = false;
        }

        if(onWall && !isGrounded)
        {
            wallSlide = true;
            WallSlide();
        }
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump(Vector2.up);
            isGrounded = false;
        }
        if (Input.GetButtonDown("Jump") && !isGrounded && !isDJumped)
        {
             jump(Vector2.up);
             isDJumped = true;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

     private void WallSlide()
    {

        // bool pushingWall = false;
        // if((rb.velocity.x > 0 && onRightWall) || (rb.velocity.x < 0 && onLeftWall))
        // {
        //     pushingWall = true;
        // }
        // float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
    }

    

}
