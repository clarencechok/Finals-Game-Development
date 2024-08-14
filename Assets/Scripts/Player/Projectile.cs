using UnityEngine;

public class Projectile : MonoBehaviour
{
    //determine speed of projectile variable
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    //this variable represent how many second the projectile is active
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;
    


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //check if our fireball hit anything
        //if true, will return and execute the rest of the code
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        //move the object on the x axis by movementSpeed
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    //this section will check if our fireball hit any other object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sets hit boolean to true
        hit = true;
        //this will disable the boxCollider
        boxCollider.enabled = false;
        //play the explode animation by setting off the explode trigger created
        anim.SetTrigger("explode");

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    //causes the fireball to fly either left or right
    //this method will also help reset the state of the fireball
    public void SetDirection(float _direction)
    {
        //set lifetime to 0 everytime we reset the direction of the fireball
        lifetime = 0;
        direction = _direction;
        //ensure that the fireball gameobject is set active
        gameObject.SetActive(true);
        hit = false;
        //boxCollider is enabled
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //help us deactivate the fireball after the explode animation has finished
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
