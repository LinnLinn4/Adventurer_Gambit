using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileHilight : MonoBehaviour
{
    static public GameObject toKill;
    static public Vector3? toMove;

    private void Update()
    {
        if (toMove != null)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = (Vector3)toMove;
            toMove = null;
        }
        var oldPos = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
        var newPos = gameObject.transform.localPosition;
        var xDiff = oldPos.x - newPos.x;
        var yDiff = oldPos.y - newPos.y;

        bool isFogActive = !((xDiff <= 1.16f && xDiff >= -1.16f) && (yDiff <= 1.16f && yDiff >= -1.16f));
        bool isFogActive2 = !((xDiff <= 2.4f && xDiff >= -2.4f) && (yDiff <= 2.4f && yDiff >= -2.4f));



        bool isTeleportActive = false;
        foreach (var p in Player.powerUps)
        {
            if (p == "teleport")
            {
                isTeleportActive = true;
                break;
            }
        }
        bool t = isTeleportActive ? (!isFogActive || !isFogActive2) : !isFogActive;
        if (t)
        {
            foreach (SpriteRenderer ps in GetComponentsInChildren<SpriteRenderer>())
            {
                if (ps.gameObject != gameObject && ps.gameObject.name == "fog")
                {
                    ps.enabled = true;
                    ps.color = Color.Lerp(ps.color, new Color(1f, 1f, 1f, 0f), 5 * Time.deltaTime);
                    break;
                }

            }
        }
        else
        {
            foreach (SpriteRenderer ps in GetComponentsInChildren<SpriteRenderer>())
            {
                if (ps.gameObject != gameObject && ps.gameObject.name == "fog")
                {
                    ps.enabled = true;
                    ps.color = Color.Lerp(ps.color, new Color(1f, 1f, 1f, 1f), 5 * Time.deltaTime);
                    break;
                }

            }
        }
    }

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
        var oldPos = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
        var newPos = gameObject.transform.localPosition;
        var xDiff = oldPos.x - newPos.x;
        var yDiff = oldPos.y - newPos.y;


        bool isTeleportActive = false;
        foreach (var p in Player.powerUps)
        {
            if (p == "teleport")
            {
                isTeleportActive = true;
                break;
            }
        }
        bool t = isTeleportActive ? (NearlyEqual((Math.Abs(xDiff) + Math.Abs(yDiff)), 1.15f, 0.3) || NearlyEqual((Math.Abs(xDiff) + Math.Abs(yDiff)), 2.3f, 0.3)) : NearlyEqual((Math.Abs(xDiff) + Math.Abs(yDiff)), 1.15f, 0.3);

        if (t)
        {

            if (Math.Round(xDiff) <= 0)
            {
                Player.powerUps.Remove("teleport");
                bool isEnemy = onClickTile();
                if (!isEnemy)
                {
                    GameObject.FindGameObjectWithTag("Player").transform.position = gameObject.transform.position;
                }


            }
        }

    }
    bool onClickTile()
    {
        foreach (SpriteRenderer ps in GetComponentsInChildren<SpriteRenderer>())
        {
            if (ps.gameObject != gameObject && ps.gameObject.tag == "Enemy")
            {
                onClickEnemyTile(ps.gameObject);
                return true;
            }
            if (ps.gameObject != gameObject && ps.gameObject.tag == "PowerUp")
            {
                Player.powerUps.Add(ps.gameObject.name);
                Destroy(ps.gameObject);
                return true;
            }
            if (ps.gameObject != gameObject && (ps.gameObject.name == "Trap" || ps.gameObject.name == "Buff" || ps.gameObject.name == "WheelTile"))
            {
                StateManager.deactivatedObjects.Add(GameObject.FindGameObjectWithTag("MainCamera"));
                StateManager.deactivatedObjects.Add(GameObject.FindGameObjectWithTag("GameScreen"));
                foreach (var o in StateManager.deactivatedObjects)
                {
                    o.SetActive(false);
                }
                TileHilight.toMove = ps.gameObject.transform.position;
                SceneManager.LoadScene("RandomWheel", LoadSceneMode.Additive);
                return true;
            }
            if (ps.gameObject != gameObject && ps.gameObject.name == "CheckPoint")
            {
                Player.health = Player.maxHealth;
                return false;
            }
        }
        return false;
    }

    void onClickEnemyTile(GameObject g)
    {
        StateManager.deactivatedObjects.Add(GameObject.FindGameObjectWithTag("MainCamera"));
        StateManager.deactivatedObjects.Add(GameObject.FindGameObjectWithTag("GameScreen"));
        foreach (var o in StateManager.deactivatedObjects)
        {
            o.SetActive(false);
        }
        toKill = g;
        SceneManager.LoadScene("FightScene", LoadSceneMode.Additive);
        Debug.Log("Enemy");
    }
    public static bool NearlyEqual(double a, double b, double epsilon)
    {
        const double MinNormal = 2.2250738585072014E-308d;
        double absA = Math.Abs(a);
        double absB = Math.Abs(b);
        double diff = Math.Abs(a - b);

        if (a.Equals(b))
        { // shortcut, handles infinities
            return true;
        }
        else if (a == 0 || b == 0 || absA + absB < MinNormal)
        {
            // a or b is zero or both are extremely close to it
            // relative error is less meaningful here
            return diff < (epsilon * MinNormal);
        }
        else
        { // use relative error
            return diff / (absA + absB) < epsilon;
        }
    }

}
