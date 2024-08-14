using UnityEngine;

public class CameraController : MonoBehaviour
{
    //this section of code responsible for room camera movement

    //this variable help us determine how fast the camera can move around
    [SerializeField] private float speed;
    //this variable will tell the camera which position to go
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //this section of code responsible for following of the player

    //this is reference to player transform as this is the object we will be following
    [SerializeField] private Transform player;
    //this variable will help us tweak how far the camera is able to look forward
    [SerializeField] private float aheadDistance;
    //this variable is for the speed in which the camera will look forward
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

    private void Update()
    {
        //this line of code is responsible for room camera movement
        //change the position of the camera transform
        //the first parameter for smoothdamp is the current position of the camera
        //the second parameter is the destination
        //the third parameter is the rate of change of position of the camera
        //the fourth parameter is the speed of the movement with framerate independent
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity,speed);

        //this section of code will help with player following
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        //assign the value of lookAhead
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    //this method help us change the destination of the camera
    public void MoveToNewRoom(Transform _newRoom)
    {
        //take the x position of the new room and assign it to the current x variable
        currentPosX = _newRoom.position.x;

    }
}
