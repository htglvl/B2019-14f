using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float jumptimecounter, move, nextfire = 0f,
     privatebulletleft, private_dashspeed_multiply = 1, checkceilingradius = 0.2f, checkgroundradius = 0.2f, m_CrouchSpeed = 1f;
    private bool m_FacingRight = true, grounded = false, crouch = false,
    isclimbing = false, m_wasCrouching = false;


    public float speed, Jumpforce, jumptime,
     firerate = 0.5f, reloadtime = 1.5f, bulletleft, climbspeed, Inputvertical = 0f, distance,
     RandomStepSound, autoJumpCheck, khoangCachDeBangNhau, autoJumpForce, horizontalmove = 0f;
    public bool jumping, autoJump = true;
    public Transform feetpos, headpos, firepos, fireposcrouch, itemchecker;
    public LayerMask whatisground, whatisladder, autoJumpCheckLayerMask;
    public Animator animator, firearm;
    public GameObject leftbullet, rightbullet;
    public Collider2D discollider;
    [Range(0, 1)] [SerializeField] private float CrouchSpeed = .36f;
    public Joystick joystick;
    public Image slider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firepos = transform.Find("firepos");
        fireposcrouch = transform.Find("crouch firepos");
        privatebulletleft = bulletleft;
        Physics2D.queriesStartInColliders = true;
    }
    public void Update()
    {
        slider.fillAmount = privatebulletleft / bulletleft;
        grounded = Physics2D.OverlapCircle(feetpos.position, checkgroundradius, whatisground);
        //move raw
        if (joystick.Horizontal > 0.2f || Input.GetAxisRaw("Horizontal") == 1)
        {
            move = 1;
        }
        else if (joystick.Horizontal < -0.2f || Input.GetAxisRaw("Horizontal") == -1)
        {
            move = -1;
        }
        else
        {
            move = 0;
        }
        //get jump raw
        if (joystick.Vertical > 0.4f || Input.GetAxisRaw("Vertical") == 1)
        {
            if (Inputvertical <= 0)
            {
                Inputvertical = 1;
            }
        }
        else if (joystick.Vertical < -0.4f || Input.GetAxisRaw("Horizontal") == -1)
        {
            Inputvertical = -1;
        }
        else
        {
            Inputvertical = 0;
        }
        //dash
        // jump
        if (grounded == true && Inputvertical == 1 && crouch == false)
        {
            rb.velocity = Vector2.up * Jumpforce;
            jumping = true;
            jumptimecounter = jumptime;
            animator.SetBool("isjumping", true);
            m_CrouchSpeed = 1f;
        }
        //jump higher if hold
        if (Inputvertical == 1 && jumping == true && crouch == false)
        {

            if (jumptimecounter > 0)
            {
                rb.velocity = Vector2.up * Jumpforce;
                jumptimecounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }
        if (Inputvertical != 1)
        {
            jumping = false;
        }
        //auto jump
        if (autoJump == true)
        {
            RaycastHit2D autoJumpUp = Physics2D.Raycast(headpos.position,
            transform.right * move, autoJumpCheck, autoJumpCheckLayerMask);
            RaycastHit2D autoJumpDown = Physics2D.Raycast(itemchecker.position, transform.right * move,
            autoJumpCheck + khoangCachDeBangNhau, autoJumpCheckLayerMask);
            Debug.DrawLine(itemchecker.position, itemchecker.position + transform.right * (autoJumpCheck + khoangCachDeBangNhau) * move
            , Color.white);
            Debug.DrawLine(headpos.position, headpos.position + transform.right * autoJumpCheck * move, Color.white);

            if (autoJumpDown.collider != null && autoJumpUp.collider == null)
            {
                rb.velocity = Vector2.up * Jumpforce;
                jumping = true;
                m_CrouchSpeed = 1f;
                animator.SetBool("isjumping", true);
            }
        }

        //crouch
        if (Inputvertical == -1 && isclimbing == false)
        {
            crouch = true;
            animator.SetBool("isjumping", false);
            firepos.position = new Vector2(firepos.position.x, fireposcrouch.position.y);
            m_CrouchSpeed = CrouchSpeed;
        }
        if (Inputvertical != -1)
        {

            if (Physics2D.OverlapCircle(headpos.position, checkceilingradius, whatisground) && isclimbing == false)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
        }
        //fire
        if (CrossPlatformInputManager.GetButtonDown("Fire1") && Time.time > nextfire && privatebulletleft > 0)
        {
            privatebulletleft--;
            nextfire = Time.time + firerate;

            fire();
            FindObjectOfType<audiomanager>().Play("usp sound");
            firearm.SetBool("isshooting", true);
            firearm.SetBool("isreloading", false);
        }
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            firearm.SetBool("isshooting", false);
            firearm.SetBool("isreloading", false);
        }
        if (privatebulletleft == 0 && CrossPlatformInputManager.GetButtonDown("Fire1") && Time.time > nextfire)
        {
            nextfire = Time.time + firerate + reloadtime;
            firearm.SetBool("isshooting", false);
            firearm.SetBool("isreloading", true);
            privatebulletleft = bulletleft;
        }
        //end fire
    }
    void FixedUpdate()
    {

        animator.SetFloat("speed", Mathf.Abs(horizontalmove));
        animator.SetFloat("climb", Mathf.Abs(Inputvertical));
        rb.velocity = new Vector2(horizontalmove, rb.velocity.y);
        horizontalmove = move * Time.fixedDeltaTime * speed * m_CrouchSpeed * private_dashspeed_multiply;
        Move(horizontalmove, crouch);
        //grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(feetpos.position, checkgroundradius, whatisground);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatisladder);

        if (hitinfo.collider != null)
        {
            isclimbing = true;
            m_CrouchSpeed = CrouchSpeed;
            //dua crouch speed vao day cho de dieu khien khi len thang
        }
        else
        {
            isclimbing = false;
            m_CrouchSpeed = 1;
            //dua crouch speed vao day cho de dieu khien khi len thang
        }
        if (isclimbing == true)
        {
            rb.velocity = new Vector2(horizontalmove, Inputvertical * climbspeed);
            rb.gravityScale = 0;
            animator.SetBool("isclimbing", true);

        }
        else
        {
            rb.gravityScale = 20;
            animator.SetBool("isclimbing", false);

        }
        if (discollider.enabled == false)
        {
            firearm.SetBool("iscrouching", true);
            animator.SetBool("iscrouching", true);
        }
        else
        {
            firearm.SetBool("iscrouching", false);
            animator.SetBool("iscrouching", false);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!Physics2D.OverlapCircle(firepos.position, checkceilingradius, whatisground) || Physics2D.OverlapCircle(feetpos.position, checkgroundradius, whatisground))
        {
            animator.SetBool("isjumping", false);
            Inputvertical = 2;
            jumping = false;
        }
    }




    public void Move(float move, bool crouch)
    {
        //tu day
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(headpos.position, checkceilingradius, whatisground))
            {
                crouch = true;
                firepos.position = new Vector2(firepos.position.x, fireposcrouch.position.y);
                jumping = false;
                m_CrouchSpeed = CrouchSpeed;
            }
            //den day kho qua chua optimize	
        }
        // If crouching
        if (crouch)
        {
            if (!m_wasCrouching)
            {
                m_wasCrouching = true;
            }
            // Disable one of the colliders when crouching
            if (discollider != null)
            { discollider.enabled = false; }
        }
        else
        {
            // Enable the collider when not crouching
            if (discollider != null)
                discollider.enabled = true;
            m_CrouchSpeed = 1f;
            if (m_wasCrouching)
            {
                m_wasCrouching = false;
            }

        }
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
            GetComponent<medkitscript>().directionleft = 1;
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
            GetComponent<medkitscript>().directionleft = -1;
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void fire()
    {
        if (m_FacingRight)
        {
            rightbullet.GetComponent<bulletcontroller>().speed =
            rightbullet.GetComponent<bulletcontroller>().speedRaw + horizontalmove;
            Instantiate(rightbullet, firepos.position, Quaternion.identity);
        }
        else
        {
            leftbullet.GetComponent<bulletcontroller>().speed =
            leftbullet.GetComponent<bulletcontroller>().speedRaw + horizontalmove;
            Instantiate(leftbullet, firepos.position, Quaternion.identity);
        }

    }

    public void GetRandomSound()
    {
        RandomStepSound = Random.Range(4, 8);
        FindObjectOfType<audiomanager>().Play("step " + RandomStepSound);
    }
}