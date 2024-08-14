using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    //create a reference to the player's rigidbody
    private Rigidbody2D body;
    //creating reference to our animator
    private Animator anim;
    //creating a reference for our BoxCollider2D component
    private BoxCollider2D boxCollider;
    //this variable is responsible for creating of delays between walljump
    private float wallJumpCooldown;
    //to help us access the horizontalInput inside jump method
    private float horizontalInput;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Coyote Time")]
    //this variable will help us determine the time that player can hang in air before the player perform the jump
    [SerializeField] private float coyoteTime;
    //this variable will show how much time passed since player ran off the edge of an object
    private float coyoteCounter;

    //creating an awake method
    private void Awake()
    {
        //using getcomponent to access rigidbody
        body = GetComponent<Rigidbody2D>();
        //using getcomponent to access animator
        anim = GetComponent<Animator>();
        //using getcomponent to access BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //creating update method for left and right
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        //to make the player flip left and right animation when moving left/right
        //check if the player is moving right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        //check if the player is moving left
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);

        //Setting of the animator parameters
        //our parameter in the animation sector is called run thats why i put run here
        anim.SetBool("run", horizontalInput != 0);
        //this will give the animator information wether the player is on the ground or not
        anim.SetBool("grounded", isGrounded());

        //jumping
        //make sure we call the jump method only once with GetKeyDown
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        //implementation of jump height that can be adjusted
        //check the release of space key using GetKeyUp method
        if(Input.GetKeyUp(KeyCode.Space))
        {
            //check if body y velocity is bigger than 0 and this code will result in a smaller jump
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        //check if the player is on wall
        if(onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 6;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            //checking wether the player is grounded or not
            if (isGrounded())
            {
                //reset the counter when the player is on the ground
                coyoteCounter = coyoteTime;
            }else
            {
                //player not grounded
                //start decreasing the counter whenever the player walk off the edge
                coyoteCounter -= Time.deltaTime;
            }
        }
    }

    //this section of code will help us optimise our jumping code
    private void Jump()
    {
        //check if the counter is smaller than 0 and the player not on the wall, dont perform next few code
        if (coyoteCounter < 0 && !onWall())
        {
            return;
        }
        //playing of the jumpSound music
        SoundManager.instance.PlaySound(jumpSound);

        //check if player is on the wall
        if (onWall())
        {
            WallJump();
        }else
        {
            //check if player is grounded
            //apply jump force if grounded
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                //check if player not grounded and the counter is larger than 0 then do a normal jump
                if (coyoteCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
                //reset the counter to prevent the occurence of double jumpings.
                coyoteCounter = 0;
            }
        }
    }

    private void WallJump()
    {

    }

    //this section of code will tell if our player is grounded or not
    private bool isGrounded()
    {
        //Raycast create a visual line from point of origin to a certain direction, if the line intercepts with an object that has a collider on it, it will returnn true otherwise false
        //boxCollider.bounds.center give us the center of the box collider and boxCollider.bounds.size give us the size
        //Vector2.down will help us check what is underneath the players
        //0 is the angle as we do not want to rotate then box
        //0.1f represent how far underneath the player we want to position the virtual box for BoxCast.
        //the last parameter is the layer mask that tell the boxcast to only look for colliders in a specific layer and ignore other ones
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //this section will handle the wall jumping mechanics
    private bool onWall()
    {
        //new Vector2 will help us check left and right instead of down because we are no longer doing the ground
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        //define when exactly a player will be able to attack
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}