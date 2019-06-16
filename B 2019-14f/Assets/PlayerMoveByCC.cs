using UnityEngine;


public class PlayerMoveByCC : MonoBehaviour
{
    [HideInInspector] public float Inputvertical;
    [Range(0, 1)] [SerializeField] private float DZVertical;
    [Range(0, 1)] [SerializeField] private float DZHorizontal;
    public Animator animator, firearm;
    public CharacterController2D CC2D;
    public Joystick joystick;
    //public Image slider;
    float moveDirection, RandomStepSound;
    public float Runspeed = 100;
    bool crouch = false, cursorDangOTren;

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
        if (joystick.Horizontal > DZHorizontal || Input.GetAxisRaw("Horizontal") == 1)
        {
            moveDirection = 1 * Runspeed;
            GetRandomSound();
        }
        else if (joystick.Horizontal < -DZHorizontal || Input.GetAxisRaw("Horizontal") == -1)
        {
            moveDirection = -1 * Runspeed;
            GetRandomSound();
        }
        else
        {
            moveDirection = 0;
        }
        //jumping
        if (joystick.Vertical > DZVertical || Input.GetAxisRaw("Vertical") == 1)
        {
            crouch = false;
            cursorDangOTren = true;
            if (Inputvertical == 0)
            {
                Inputvertical = 1;
                animator.SetBool("isjumping", true);
            }
        }
        else if (joystick.Vertical < -DZVertical || Input.GetAxisRaw("Horizontal") == -1)
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
        //animator.SetFloat("climb", Mathf.Abs(Inputvertical));

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

    public void OnClimbing(bool IsClimbing)
    {

        animator.SetBool("isclimbing", IsClimbing);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("iscrouching", isCrouching);
        firearm.SetBool("iscrouching", isCrouching);
    }
    private void FixedUpdate()
    {

        CC2D.Move(moveDirection * Time.fixedDeltaTime, crouch, Inputvertical, cursorDangOTren);
        if (Inputvertical == 1)
        {
            Inputvertical = 2;
        }
    }
    public void GetRandomSound()
    {
        RandomStepSound = Random.Range(4, 8);
        FindObjectOfType<audiomanager>().Play("step " + RandomStepSound);
    }
}
