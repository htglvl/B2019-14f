using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerMoveByCC : MonoBehaviour
{
    [HideInInspector] public float Inputvertical;
    [SerializeField] private LayerMask m_whatisladder; 
    public Animator animator, firearm;
    public CharacterController2D CC2D;
    public Joystick joystick;
    //public Image slider;
    float moveDirection, climbspeed;
    public float Runspeed = 100;
    bool crouch = false, cursorDangOTren, Climb, isclimbing;
    public float distanceCheckLadder = 0f;

    //Transform firepos, fireposcrouch, itemchecker, FinalFirePosition;
    // Start is called before the first frame update
    void Start()
    {
        /* rb = GetComponent<Rigidbody2D>();
        firepos = transform.Find("firepos");
        fireposcrouch = transform.Find("crouch firepos");
        FinalFirePosition = transform.Find("FinalFirePosition");
        //privatebulletleft = bulletleft;
        Physics2D.queriesStartInColliders = true;
        joystick = GetComponent<FloatingJoystick>();*/

    }

    // Update is called once per frame
    void Update()
    {
        //move
        if (joystick.Horizontal > 0.4f || Input.GetAxisRaw("Horizontal") == 1)
        {
            moveDirection = 1 * Runspeed;
        }
        else if (joystick.Horizontal < -0.4f || Input.GetAxisRaw("Horizontal") == -1)
        {
            moveDirection = -1 * Runspeed;
        }
        else
        {
            moveDirection = 0;
        }
        //jumping
        if (joystick.Vertical > 0.4f || Input.GetAxisRaw("Vertical") == 1)
        {
            crouch = false;
            cursorDangOTren = true;
            if (Inputvertical == 0)
            {
                Inputvertical = 1;
                animator.SetBool("isjumping", true);
            }
        }
        else if (joystick.Vertical < -0.4f || Input.GetAxisRaw("Horizontal") == -1)
        {
            Inputvertical = -1;
            crouch = true;
            cursorDangOTren = false;
        }else
        {
            Inputvertical = 0;
            crouch = false;
            cursorDangOTren = false;
        }

        animator.SetFloat("speed", Mathf.Abs(moveDirection));

    }
    public void OnLanding()
    {
        animator.SetBool("isjumping", false);
        
    }
    public void OnJumping(bool IsJumping)
    {
        if (IsJumping)
        {
            animator.SetBool("isjumping", true);
        }
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("iscrouching", isCrouching);
    }
    private void FixedUpdate()
    {

        CC2D.Move(moveDirection * Time.fixedDeltaTime, crouch, Inputvertical, cursorDangOTren, Climb);
        if (Inputvertical == 1)
        {
            Inputvertical = 2;
        }

        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.up, distanceCheckLadder, m_whatisladder);

        if (hitinfo.collider != null)
        {
            isclimbing = true;
           // m_CrouchSpeed = CrouchSpeed;
            //dua crouch speed vao day cho de dieu khien khi len thang
        }
        else
        {
            isclimbing = false;
           // m_CrouchSpeed = 1;
            //dua crouch speed vao day cho de dieu khien khi len thang
        }
    }
    
}
