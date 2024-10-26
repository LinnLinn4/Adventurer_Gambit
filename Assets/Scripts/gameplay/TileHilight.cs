using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHilight : MonoBehaviour
{
    void OnMouseOver()
    {
        foreach (SpriteRenderer ps in GetComponentsInChildren<SpriteRenderer>())
        {
            if (ps.gameObject != gameObject && ps.gameObject.name == "hilight")
            {
                ps.enabled = true;
                break;
            }

        }

    }

    void OnMouseExit()
    {

        foreach (SpriteRenderer ps in GetComponentsInChildren<SpriteRenderer>())
        {
            if (ps.gameObject != gameObject && ps.gameObject.name == "hilight")
            {
                ps.enabled = false;
                break;
            }

        }
    }

    private void OnMouseUp()
    {
        var oldPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        var newPos = gameObject.transform.position;
        Debug.Log((Math.Abs(oldPos.x - newPos.x) + Math.Abs(oldPos.y - newPos.y)).ToString());

        if ((Math.Abs(oldPos.x - newPos.x) + Math.Abs(oldPos.y - newPos.y)).ToString() == "1.15")
        {
            Debug.Log(Math.Abs((oldPos.x - newPos.x)) + Math.Abs((oldPos.y - newPos.y)));

            GameObject.FindGameObjectWithTag("Player").transform.position = gameObject.transform.position;
        }

    }

}
