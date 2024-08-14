using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //this will indicate how much health will be restored to player
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for collision with player
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            //deactivate collectible after health increase
            gameObject.SetActive(false);

        }
    }
}
