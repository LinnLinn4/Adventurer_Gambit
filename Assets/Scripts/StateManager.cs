using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    static public bool isFightFinished = false;
    static public List<GameObject> deactivatedObjects = new List<GameObject>(new GameObject[] { });


    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject dialogScreen;
    public AudioSource src;
    public AudioClip winSound, loseSound;

    // static public string dialogString;
    bool showDialog = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void showDialogScreen(string text)
    {
        if (showDialog)
        {
            return;
        }
        dialogScreen.SetActive(true);
        GameObject.Find("Dialog_text").GetComponent<TextMeshProUGUI>().text = text;
        showDialog = true;
    }
    public void removeDialog()
    {
        showDialog = false;
        dialogScreen.SetActive(false);
    }
    public void showWinScreen()
    {
        winScreen.SetActive(true);
        src.clip = winSound;
        src.Play();
        deactivatedObjects.Clear();
    }
    public void showLoseScreen()
    {
        loseScreen.SetActive(true);
        src.clip = loseSound;
        src.Play();
        deactivatedObjects.Clear();
    }
    // Update is called once per frame
    void Update()
    {

        if (isFightFinished)
        {
            foreach (var o in deactivatedObjects)
            {
                o.SetActive(true);
            }
            isFightFinished = false;
            deactivatedObjects.Clear();
        }
        // if (dialogString != null)
        // {
        //     showDialogScreen(dialogString);
        //     dialogString = null;
        // }

    }
}
