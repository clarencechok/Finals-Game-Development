using UnityEngine;
using UnityEngine.UI;
public class SelectionArrow : MonoBehaviour
{
    //To get the y positions of all the options
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    //this variable will tell us currently which option is selected
    private int currentSelectedPosition;
    //this variable is to play the sound when we change options or move the arrow up and down
    [SerializeField] private AudioClip ChangeOptionSound;
    //this variable is to play the sound when 1 option is selected and we press enter
    [SerializeField] private AudioClip interactionSound;

    private void Awake()
    {
        //to gain access to rectTransform
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //check if up arrow key or W key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //move arrow up if either key is pressed down
            ChangingPosition(-1);
        }else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            ChangingPosition(1);
        }

        //interactions with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void ChangingPosition(int _change)
    {
        currentSelectedPosition += _change;

        //to check if there is change in option
        if (_change != 0)
        {
            SoundManager.instance.PlaySound(ChangeOptionSound);
        }

        //to solve the issue of current position of arrow being negative or larger than length of array
        if (currentSelectedPosition < 0)
        {
            currentSelectedPosition = options.Length - 1;
        }else if (currentSelectedPosition > options.Length - 1)
        {
            currentSelectedPosition = 0;
        }

        //moving up and down of the arrow
        rect.position = new Vector3(rect.position.x, options[currentSelectedPosition].position.y, 0);
    }

    private void Interact()
    {
        //play the interaction sound
        SoundManager.instance.PlaySound(interactionSound);

        //access button component on every option and call own function
        options[currentSelectedPosition].GetComponent<Button>().onClick.Invoke();
    }
}
