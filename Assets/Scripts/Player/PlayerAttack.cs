using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   //this float will tell us how much time is required before we can fire the next shot
    [SerializeField] private float attackCooldown;
    //this float tell us position in which the bullet will be fired
    [SerializeField] private Transform firePoint;
    //this array will contain all the 10 fireballs we created
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovement playerMovement;
    //this float will ensure that enough time have passed since last shot before attacking
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private AudioClip fireballSound;

    private void Awake()
    {
        //get references to the animator
        anim = GetComponent<Animator>();
        // get references to the playermovement
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //check if the left mouse button is pressed
        //check if enough time have passed to fire the next shot too
        //playerMovement.canAttack is to check that the player is in a state that he can attack
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            Attack();
        }
        //increase on every frame by time.deltatime
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        //play the attack animation when attacking
        anim.SetTrigger("attack");
        //when attack, the cooldowntimer will reset to 0
        cooldownTimer = 0;

        //everytime we attack we will take 1 of the fireball and reset the position to be the position of the firepoint
        fireballs[FindFireball()].transform.position = firePoint.position;
        //get the projectile component from the fireball and use SetDirection to set it in the direction in which the player is facing 
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        //loop through all the fireballs in the array
        for (int i = 0; i < fireballs.Length; i++)
        {
            //check if the fireball with index i is active in the hierarchy
            if (!fireballs[i].activeInHierarchy)
                //return the index to the attack method
                return i;
        }
        return 0;
    }
}
