using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    public Transform player;
    public Transform self;
    public Rigidbody2D  rb;

    public LayerMask playerLayer;

    private Vector2 spawnPos;
    public Vector2 topOffset, rightOffset, leftOffset;

    private float diff;
    public float atcRange;
    private float atcRangeOffSet;

//state determines what the creature is currently doing
//0 = idle, 1 = patrol, 2 = hunt
    public int state;

    public float speed;
    public float collisionRadius = 0.25f;

    public bool isHit;
    public bool istHiting;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = 0;
        atcRangeOffSet = atcRange/2;
        spawnPos = new Vector2(self.position.x, self.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        isHit = Physics2D.OverlapCircle((Vector2)transform.position + topOffset, collisionRadius, playerLayer);
        istHiting = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, playerLayer) 
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, playerLayer);

        if (isHit)
        {
           DestroyObject(gameObject);
        }


        diff = (player.position.x - self.position.x) + atcRangeOffSet;
        if (diff >= 0 && diff <= atcRange)
        {
            state = 2;
        }
        else {state = 0;}

        switch(state)
        {
            case 0:
            idle();
            break;
            case 1:
            patrol();
            break;
            case 2:
            hunt();
            break;
        }

        
    }

    private void idle()
    {
        rb.velocity = new Vector2(0, 0);
    }

    private void patrol()
    {

    }

    private void hunt()
    {
        int direction;

        if (player.position.x > self.position.x)
        {  
            direction = 1;
            }
        else 
        { 
            direction = -1;
            }

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        var positions = new Vector2[] { topOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + topOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

}
