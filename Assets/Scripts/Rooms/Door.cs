using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    //to detect collision with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the object we collide with have the tag equal to player
        if (collision.tag == "Player")
        {
            //we need to know which direction the player is coming from
            //therefore we checking if the player x position is smaller than the door's x position
            if (collision.transform.position.x < transform.position.x)
            {
                //if this is true, we know that the player is coming from the left and we tell the camera to move to the right
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                cam.MoveToNewRoom(previousRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
            }
        }
    }
}
