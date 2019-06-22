using UnityEngine;
using UnityEngine.Events;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f, jumptime;                            // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;
    [SerializeField] private float climbspeedV, m_ClimbSpeedH;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] private bool m_AirControl = true, autoJump = true;                            // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround, m_whatisladder;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Transform m_ItemCheck;
    [HideInInspector] public bool jumping, CrouchOutSide;  // For determining which way the player is currently facing.
    public PlayerMoveByCC PMBCC;
    public PlayerFire PF;
    public float autoJumpDistanceCheck = 0.5f, distanceCheckLadder = 0f, HorizontalMove;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    const float khoangCachDeBangNhau = -.2f;
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    private float jumptimecounter;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnClimbEvent;
    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false, climb = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        if (OnClimbEvent == null)
            OnClimbEvent = new BoolEvent();

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
        PMBCC.OnClimbing(climb);
        PF.fire(m_FacingRight, HorizontalMove, CrouchOutSide);

    }
    public void Move(float move, bool crouch, float jump, bool cursor)
    {
        HorizontalMove = move ; // dua bien move ra ngoai de vao dc ham fire
        CrouchOutSide = crouch;// dua bien crouch ra ngoai de vao dc ham fire
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
            if (crouch && !climb)
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
                //right
                // ... flip the player.
                Flip();
                GetComponent<medkitscript>().directionleft = 1;
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
                //left
                GetComponent<medkitscript>().directionleft = -1;
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
        }
        else
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
                m_Rigidbody2D.AddForce(new Vector2(0f, 300f));
                jumping = true;
            }
        }

        if (jumping && crouch)
        {
            //if he is jumping and touch ceiling => stop jumping
            jumping = false;
        }
        if (jumping && climb)
        {
            jumping = false;
        }
        


        RaycastHit2D hitinfo = Physics2D.Raycast(m_GroundCheck.position, Vector2.up, distanceCheckLadder, m_whatisladder);
        Debug.DrawLine(m_GroundCheck.position, transform.position + transform.up * distanceCheckLadder , Color.blue);
        if (hitinfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitinfo.point, Color.white);
            climb = true;
            move *= m_CrouchSpeed;
            m_Rigidbody2D.velocity = new Vector2(move *= m_ClimbSpeedH, jump *= climbspeedV);
            m_Rigidbody2D.gravityScale = 0;
            //dua crouch speed vao day cho de dieu khien khi len thang
            //animator.SetBool("isclimbing", true);
            //OnClimbEvent.Invoke(true);
        }
        else
        {
            climb = false;
            m_Rigidbody2D.gravityScale = 20;
            //animator.SetBool("isclimbing", false);
            //dua crouch speed vao day cho de dieu khien khi len thang
            //OnClimbEvent.Invoke(false);
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
