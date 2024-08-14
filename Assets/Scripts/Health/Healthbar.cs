using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] Image totalhealthBar;
    [SerializeField] Image currenthealthBar;


    private void Start()
    {
        //total health bar will only be updated once whenn we start the game
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        //to make the current health bar update continuously
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
