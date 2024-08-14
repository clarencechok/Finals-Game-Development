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

        //this part of code is responsible for wall jumping logic
        if(wallJumpCooldown > 0.2f)
        {
           
            //use body.velocity to change how fast the players moving and in what direction
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            //this if statement will check if player is on the wall and not grounded
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                //if the player jump to the wall, he will get stuck and unable to fall down
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 5;
            }

            //using IF input.getkey to check for space presses
            //input.getkey will only return true or false when key is pressed or when not pressed
            if (Input.GetKey(KeyCode.Space))
                //calling the jump function
                Jump();
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    //this section of code will help us optimise our jumping code
    private void Jump()
    {
        //allow us to handle the jump differently depending on wether we on ground or on wall
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        //check that the player is on the wall and is not grounded to perform our special jump
        else if(onWall() && !isGrounded())
        {
            //this if statement will check if the horizontal input is equal to 0
            if(horizontalInput == 0)
            {
                //0 in y axis to prevent player from being thrown upwards.
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                //flip the player in the opposite direction when he jumps away from the wall.
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                //add a force on the player's body that pushes him away from wall and upwards at same time
                //we access the body velocity creating a new vector2 and we get the direction  the player is facing and create force opposite it.
                //Mathf.Sign return the sign of the number. if get negative number it will return -1 and if positive number will be 1
                //the negative sign infront of Mathf.Sign will push the player away from the wall.
                //3 is the power in which the player will be pushed away from the wall.
                //6 is the magnitude of the force that pushes the player upwards.
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            //when the player is stuck to wall and not grounded, make the player wait abit before performing next jump
            wallJumpCooldown = 0;
        }
        
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
