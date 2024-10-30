using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    static public bool isFightFinished = false;
    static public List<GameObject> deactivatedObjects = new List<GameObject>(new GameObject[] { });
    // Start is called before the first frame update
    void Start()
    {

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

    }
}
