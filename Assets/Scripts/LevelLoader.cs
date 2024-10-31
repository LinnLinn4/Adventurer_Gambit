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
    public void onLoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void onLoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void onLoadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
}
