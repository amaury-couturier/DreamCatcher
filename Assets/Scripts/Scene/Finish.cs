using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private ShootPistol shootPistol; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PlayerObject" && shootPistol.enemiesLeft == 0)
            LevelManager.manager.FinishedGame();
        else if(other.gameObject.name == "PlayerObject" && shootPistol.enemiesLeft != 0)
            SceneManager.LoadScene("Game Over");
    }
}
