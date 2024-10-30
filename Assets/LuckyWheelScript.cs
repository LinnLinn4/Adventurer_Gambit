
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LuckyWheelScript : MonoBehaviour
{
    public List<GameObject> toLook;
    public GameObject toLookObj;
    public GameObject line;


    static public String currentWinningObjectIndex = "0";
    static public bool rotating = false;
    // Start is called before the first frame update
    void Start()
    {
        var s = (Math.PI) / toLook.Count;
        var i = 0;
        const int radius = 150;
        foreach (var item in toLook)
        {
            GameObject q = Instantiate(item);
            q.transform.localScale = new Vector3(30f, 30f, 30f);
            q.transform.position = new Vector3((float)(Math.Sin(i * (Math.PI * 2) / toLook.Count) * radius), (float)(Math.Cos(i * (Math.PI * 2) / toLook.Count) * radius), 0f);
            q.name = (i + 1) + q.name;
            GameObject l = Instantiate(line);
            l.transform.localScale = new Vector3(200f, 2f, 1f);
            l.transform.position = new Vector3((float)(Math.Sin((i * (Math.PI * 2) / toLook.Count) - s) * 100), (float)(Math.Cos((i * (Math.PI * 2) / toLook.Count) - s) * 100), 0f);
            l.name = i.ToString();
            Vector3 dir = toLookObj.transform.position - q.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            q.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



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
        if (rotating)
        {
            foreach (Rigidbody2D ps in GetComponentsInChildren<Rigidbody2D>())
            {
                if (ps.gameObject != gameObject && ps.gameObject.name == "bg")
                {
                    if (ps.angularVelocity == 0)
                    {

                        rotating = false;
                        var wonItem = toLook.ElementAt(int.Parse(currentWinningObjectIndex));
                        if (wonItem.name == "player_attack")
                        {


                        }



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
                    float force = UnityEngine.Random.Range(10000f, 20000f);
                    ps.AddTorque(force);
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