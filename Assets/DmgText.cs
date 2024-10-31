using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    public bool dest = false;
    // Start is called before the first frame update
    void Start()
    {
        if (dest)
        {
            Destroy(gameObject, 1f);

        }
    }


}
