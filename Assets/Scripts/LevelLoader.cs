using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void onLoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
