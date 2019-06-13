using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f, jumptime;                            // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f, climbspeed;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] private bool m_AirControl = true, autoJump = true;                            // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround, m_whatisladder;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Transform m_ItemCheck;
    [SerializeField] private Transform m_FirePosition;
    [HideInInspector] public bool jumping;  // For determining which way the player is currently facing.
    public PlayerMoveByCC PMBCC;
    public float autoJumpDistanceCheck = 0.5f, distanceCheckLadder = 0f;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    const float khoangCachDeBangNhau = -.2f;
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true, isclimbing;
    private Vector3 m_Velocity = Vector3.zero;
    private float jumptimecounter;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
                    
            }
        }
    }
    private void Update()
    {
        PMBCC.OnJumping(jumping);
    }


    public void Move(float move, bool crouch, float jump, bool cursor, bool climb)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump == 1)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_Grounded = false;
            jumping = true;
            jumptimecounter = jumptime;
        }
        //jump higher if hold
        if (cursor && jumping)
        {

            if (jumptimecounter > 0)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                jumptimecounter -= Time.fixedDeltaTime;
            }
            else
            {
                jumping = false;
            }
        }else
        {
            jumping = false;
        }
        if (autoJump == true)
        {
            RaycastHit2D autoJumpUp = Physics2D.Raycast(m_CeilingCheck.position,
            transform.right * move, autoJumpDistanceCheck, m_WhatIsGround);
            RaycastHit2D autoJumpDown = Physics2D.Raycast(m_ItemCheck.position, transform.right * move,
            autoJumpDistanceCheck + khoangCachDeBangNhau, m_WhatIsGround);
            Debug.DrawLine(m_ItemCheck.position, m_ItemCheck.position + transform.right * (autoJumpDistanceCheck + khoangCachDeBangNhau) * move
            , Color.white);
            Debug.DrawLine(m_CeilingCheck.position, m_CeilingCheck.position + transform.right * autoJumpDistanceCheck * move, Color.white);

            if (autoJumpDown.collider != null && autoJumpUp.collider == null)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                jumping = true;
            }
        }

        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, distanceCheckLadder, m_whatisladder);

        if (hitinfo.collider != null)
        {
            isclimbing = true;
            //dua crouch speed vao day cho de dieu khien khi len thang
        }
        else
        {
            isclimbing = false;
            //dua crouch speed vao day cho de dieu khien khi len thang
        }
        if (isclimbing == true)
        {
            m_Rigidbody2D.velocity = new Vector2(move *= m_CrouchSpeed, jump * climbspeed);
            m_Rigidbody2D.gravityScale = 0;
            //animator.SetBool("isclimbing", true);

        }
        else
        {
            m_Rigidbody2D.gravityScale = 20;
            //animator.SetBool("isclimbing", false);

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
}
