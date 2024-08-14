using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //this will indicate how much health will be restored to player
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check for collision with player
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            //deactivate collectible after health increase
            gameObject.SetActive(false);

        }
    }
}
