using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private void OnMouseUp()
    {
        SceneManager.UnloadScene(2);
        StateManager.isFightFinished = true;
    }
}


