using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelWinItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Wheel.rotating)
        {
            Wheel.currentWinningObjectIndex = collision.gameObject.name;
        }
        if (LuckyWheelScript.rotating)
        {
            LuckyWheelScript.currentWinningObjectIndex = collision.gameObject.name;
        }
    }


}
