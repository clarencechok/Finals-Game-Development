using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the enenmy collided with the player by checking the tag
        if (collision.tag == "Player")
            //access the health component and decreases health using takedamage function
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}