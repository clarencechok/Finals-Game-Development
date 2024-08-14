using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    //this variable will indicate how far this object will move
    [SerializeField] private float movementDistance;
    //this variable is for the damage of the enemy
    [SerializeField] private float damage;
    //speed of the movement variable
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        //range of movement of the object
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        //check if moving left or right
        if (movingLeft)
        {
            //if moving left we will check if the x position of the object is bigger than leftEdge
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }else
            {
                movingLeft = false;
            }
        }
        else
        {
            //if moving right
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }

    }

    //this will detect collisions with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check that the collision is with the game object tagged with player
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
