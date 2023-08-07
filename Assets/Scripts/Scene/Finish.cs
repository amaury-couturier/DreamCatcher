using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private ShootPistol shootPistol; 

    private void Start()
    {
        // Initialize the shootPistol object by finding the ShootPistol component on the PlayerObject
        shootPistol = GameObject.Find("RayGun").GetComponent<ShootPistol>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PlayerObject")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            if (shootPistol != null)
            {
                if (shootPistol.enemiesLeft <= 0)
                    LevelManager.manager.FinishedGame();
                else
                    SceneManager.LoadScene("Game Over");
            }
            else
            {
                Debug.LogError("ShootPistol component not found.");
            }
        }
    }
}
