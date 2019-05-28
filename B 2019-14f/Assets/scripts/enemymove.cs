using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemymove : MonoBehaviour
{
    public float speed, distance, wallreturn, dazetime, grounded, tocDo = 400;
    public bool movingright = false, daze = false;
    public Transform grounddetection, walldetection;
    public LayerMask whatiswall, whatisground, groundLayer;
    private Rigidbody2D rb2D;
    public float direction = -1, pri_dazetime, idleOrWalk, waitIdleTime, walkingTime, priWalkingTime, WaitFlipTime,
    RandomStepSound;
    public Animator leganim;

    private float Pri_waitFlipTime;
    // Use this for initialization
    void Start()
    {
        //Physics2D.queriesStartInColliders = false;
        rb2D = GetComponent<Rigidbody2D>();
        pri_dazetime = dazetime;
        priWalkingTime = walkingTime;
        idleOrWalk = Random.Range (0,2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (daze == true)
        {
            speed = 0;
            pri_dazetime -= Time.deltaTime;
            Debug.Log("daze");
        }
        if (pri_dazetime <= 0)
        {
            speed = tocDo;
            pri_dazetime = dazetime;
            daze = false;
            idleOrWalk = 1;
        }

        if (idleOrWalk == 0)
        {
            	rb2D.velocity = new Vector2(0,0);
                pri_dazetime = waitIdleTime;
                idleOrWalk = 2;
        }else
        {
            if (priWalkingTime >= 0 )
            {
                priWalkingTime -= Time.deltaTime;
                rb2D.velocity = new Vector2(speed * Time.deltaTime * direction, rb2D.velocity.y);
                leganim.SetFloat("Speed", Mathf.Abs(speed));
                RaycastHit2D groundInfo = Physics2D.Raycast(grounddetection.position, Vector2.down, distance, whatisground);
                Debug.DrawRay(grounddetection.position, new Vector2(0, -distance), Color.red);
                Debug.DrawRay(walldetection.position, new Vector2(0, -wallreturn), Color.black);
                Debug.DrawRay(transform.position, new Vector2(0, -grounded), Color.green);
                RaycastHit2D wallInfo = Physics2D.Raycast(walldetection.position, Vector2.down, wallreturn, whatiswall);
                RaycastHit2D groundcenter = Physics2D.Raycast(transform.position, Vector2.down, grounded, groundLayer);
                if (groundInfo.collider == false || wallInfo.collider == true)
                {
                    if (movingright == true)
                    {
                        direction = -1f;
                        GetComponent<enemyshooting>().enemyfacingright = -1;
                        Flip();
                    }
                    else
                    {
                        direction = 1f;
                        GetComponent<enemyshooting>().enemyfacingright = 1;
                        Flip();
                    }
                }
            }
            else
            {
                priWalkingTime = walkingTime;
                idleOrWalk = 0;
            }    
        }           
    }
    private void Flip()

    {
        if (Pri_waitFlipTime <= 0 )
        {
            Pri_waitFlipTime = WaitFlipTime;
            movingright = !movingright;
            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;    
        }else
        {
            Pri_waitFlipTime -= Time.time;
        }
        // Switch the way the player is labelled as facing.
        

    }
    public void GetRandomSound()
    {
            RandomStepSound = Random.Range(4, 8);
            FindObjectOfType<audiomanager>().Play("step " + RandomStepSound);    
    }
}
