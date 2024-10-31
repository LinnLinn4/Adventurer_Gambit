
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuckyWheelScript : MonoBehaviour
{
    public List<GameObject> toLook1;
    public List<GameObject> toLook2;
    public List<GameObject> toLook3;

    public GameObject toLookObj;
    public GameObject line;


    static public String currentWinningObjectIndex = "0";
    static public bool rotating = false;

    static public int useList = 0;

    // Start is called before the first frame update
    void Start()
    {
        var toLook = useList == 0 ? toLook1 : useList == 1 ? toLook2 : toLook3;
        var s = (Math.PI) / toLook.Count;
        var i = 0;
        const int radius = 150;
        foreach (var item in toLook)
        {
            GameObject q = Instantiate(item);
            q.transform.localScale = new Vector3(80f, 80f, 80f);
            q.transform.position = new Vector3((float)(Math.Sin(i * (Math.PI * 2) / toLook.Count) * radius), (float)(Math.Cos(i * (Math.PI * 2) / toLook.Count) * radius), 0f);
            q.name = (i + 1) + q.name;
            GameObject l = Instantiate(line);
            l.transform.localScale = new Vector3(200f, 2f, 1f);
            l.transform.position = new Vector3((float)(Math.Sin((i * (Math.PI * 2) / toLook.Count) - s) * 100), (float)(Math.Cos((i * (Math.PI * 2) / toLook.Count) - s) * 100), 0f);
            l.name = i.ToString();
            Vector3 dir = toLookObj.transform.position - q.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // q.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



            Vector3 dir1 = toLookObj.transform.position - l.transform.position;
            float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
            l.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.forward);


            foreach (CanvasRenderer ps in GetComponentsInChildren<CanvasRenderer>())
            {
                if (ps.gameObject != gameObject && ps.gameObject.name == "bg")
                {
                    l.transform.SetParent(ps.transform, false);
                    q.transform.SetParent(ps.transform, false);
                    break;
                }

            }

            i++;
        }


    }

    void Update()
    {
        var toLook = useList == 0 ? toLook1 : useList == 1 ? toLook2 : toLook3;

        if (rotating)
        {
            foreach (Rigidbody2D ps in GetComponentsInChildren<Rigidbody2D>())
            {
                if (ps.gameObject != gameObject && ps.gameObject.name == "bg")
                {
                    if (ps.angularVelocity == 0)
                    {

                        rotating = false;
                        var dString = "";
                        var wonItem = toLook.ElementAt(int.Parse(currentWinningObjectIndex));
                        if (wonItem.name == "hp_plus")
                        {
                            Player.health += 5;
                            dString = "You fall into a trap. You lost 5 HP!";

                        }
                        else if (wonItem.name == "hp_minus")
                        {
                            Player.health -= 5;

                            dString = "You found a potion. You gain 5 HP!";
                        }
                        else if (wonItem.name == "attack_plus")
                        {
                            Player.attackPower += 1;

                            dString = "You found a new sword. Your attack power got increased!";

                        }
                        else if (wonItem.name == "attack_minus")
                        {
                            Player.attackPower -= 1;

                            dString = "You lost your sword. Your attack power got reduced!";
                        }

                        SceneManager.UnloadScene("RandomWheel");
                        StateManager.isFightFinished = true;
                        GameObject.Find("StateManager").GetComponent<StateManager>().showDialogScreen(dString);


                    }
                    break;
                }
            }
        }
    }


    public void rotate()
    {
        if (!rotating)
        {
            currentWinningObjectIndex = "0";
            foreach (Rigidbody2D ps in GetComponentsInChildren<Rigidbody2D>())
            {
                if (ps.gameObject != gameObject && ps.gameObject.name == "bg")
                {
                    ps.centerOfMass = new Vector2(0, 0);
                    ps.angularDrag = 1 + UnityEngine.Random.Range(0, 1f);
                    ps.gameObject.transform.rotation = Quaternion.identity;
                    float force = UnityEngine.Random.Range(0, 2000f) + 200f;
                    Debug.Log(force);
                    ps.AddTorque(force, ForceMode2D.Impulse);
                    StartCoroutine(h());
                    break;
                }
            }
        }
    }
    IEnumerator h()
    {
        yield return new WaitForSeconds(1);
        rotating = true;
    }

}
