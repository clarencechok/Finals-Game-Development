using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //audio clip that will play when pick up checkpoint
    [SerializeField] private AudioClip checkpointSound;
    //this will store  our latest checkpoint
    private Transform currentCheckpoint;
    //reference to health to reset player health when respawn
    private Health playerHealth;
    //to help us gain access to UImanager script
    private UIManager uiManager;

    private void Awake()
    {
        //grab reference to playerHealth
        playerHealth = GetComponent<Health>();
        //return the first UI manager found
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        //checking of wether there are any checkpoints available left
        //if no available checkpoints
        if (currentCheckpoint == null)
        {
            //pop up of the game over screen
            uiManager.GameOver();

            //prevent the execution of the rest of the code that is present in this function
            return;
        }

        //this will move the player to the checkpoint position
        transform.position = currentCheckpoint.position;
        //restore player health and resetting of the animation
        playerHealth.Respawn();

        //move camera back to checkpoint
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    //activation of checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the object we collide with have a checkpoint tag
        if (collision.transform.tag == "Checkpoint")
        {
            //save the checkpoint that we collided with
            currentCheckpoint = collision.transform;
            //play the checkpoint sound
            SoundManager.instance.PlaySound(checkpointSound);
            //disable the 2d collider of the checkpoint
            collision.GetComponent<Collider2D>().enabled = false;
            //this line will help us trigger the animation for the checkpoint that is called appear
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }

}
