using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    public void restart(string level)
    {
        Player.health = 25;
        Player.maxHealth = 25;
        Player.attackPower = 5;
        SceneManager.LoadScene(level);
        StateManager.deactivatedObjects.Clear();

    }
    public void continueNext(string level)
    {
        Player.health = 25;
        Player.maxHealth = 25;
        Player.attackPower = 5;
        SceneManager.LoadScene(level);
        StateManager.deactivatedObjects.Clear();
    }
    public void goHome()
    {
        Player.health = 25;
        Player.maxHealth = 25;
        Player.attackPower = 5;
        SceneManager.LoadScene("Home");
        StateManager.deactivatedObjects.Clear();
    }
}
