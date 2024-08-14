using UnityEngine;
using System.Collections;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    //creation of the header 
    [Header("Firetrap Timers")]
    //this variable will tell us how much time is needed to activate the fire trap after player stepped on it
    [SerializeField] private float activationDelay;
    //this variable tell us how much time the firetrap will stay active after activated
    [SerializeField] private float activeTime;
    //variABLE To reference to animator
    private Animator anim;
    //variABLE To reference to sprite renderer
    private SpriteRenderer spriteRend;

    private bool triggered; //when the trap gets triggered
    private bool active; //when the trap is active and can hurt the player

    private Health playerHealth;

    private void Update()
    {
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);

        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if trap collided with the player
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
            {
                //if trap not triggered, we need to trigger it
                StartCoroutine(ActivateFiretrap());
            }
                

            if (active)
            {
                //hurt the player if the trap is active
                collision.GetComponent<Health>().TakeDamage(damage);
            }
                
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    //we use IEnumerator here to deal with delays
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red; 

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn the sprite back to its initial color
        active = true;
        anim.SetBool("activated", true);

        //Wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}