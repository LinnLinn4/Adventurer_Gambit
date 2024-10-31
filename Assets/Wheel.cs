using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wheel : MonoBehaviour
{
    public List<GameObject> toLook;
    public GameObject toLookObj;
    public GameObject line;
    public GameObject playerDmgIndicator;
    public GameObject enemyDmgIndicator;
    public GameObject dmgText;

    public GameObject playerChar;
    public AudioSource src;
    public AudioClip attackedSound;

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
            q.transform.localScale = new Vector3(80f, 80f, 80f);
            q.transform.position = new Vector3((float)(Math.Sin(i * (Math.PI * 2) / toLook.Count) * radius), (float)(Math.Cos(i * (Math.PI * 2) / toLook.Count) * radius), 0f);
            q.name = (i + 1) + q.name;
            GameObject l = Instantiate(line);
            l.transform.localScale = new Vector3(200f, 2f, 1f);
            l.transform.position = new Vector3((float)(Math.Sin((i * (Math.PI * 2) / toLook.Count) - s) * 100), (float)(Math.Cos((i * (Math.PI * 2) / toLook.Count) - s) * 100), 0f);
            l.name = i.ToString();
            Vector3 dir = toLookObj.transform.position - q.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // q.transform.rotation = Quaternion.AngleAxis(q.transform.rotation.z, Vector3.forward);



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
                        var enemyDead = false;
                        var playerDead = false;

                        rotating = false;
                        var enemy = EnemySpawn.enemy.GetComponent<Enemy>();
                        var wonItem = toLook.ElementAt(int.Parse(currentWinningObjectIndex));
                        if (wonItem.name == "player_attack")
                        {
                            GameObject dmg = Instantiate(dmgText);
                            dmg.GetComponent<DmgText>().dest = true;
                            dmg.GetComponent<TextMeshProUGUI>().text = "- 10";

                            enemy.health -= Player.attackPower;
                            enemyDead = enemy.health <= 0;
                            dmg.transform.SetParent(enemyDmgIndicator.transform, false);
                            EnemySpawn.enemy.GetComponent<Animator>().SetTrigger("got_attacked");
                            src.clip = attackedSound;
                            src.Play();
                        }
                        else if (wonItem.name == "enemy_attack")
                        {
                            GameObject dmg = Instantiate(dmgText);

                            dmg.GetComponent<DmgText>().dest = true;
                            dmg.GetComponent<TextMeshProUGUI>().text = "- 10";

                            Player.health -= enemy.attackPower;
                            playerDead = Player.health <= 0;
                            dmg.transform.SetParent(playerDmgIndicator.transform, false);
                            playerChar.GetComponent<Animator>().SetTrigger("got_attacked");
                            src.clip = attackedSound;
                            src.Play();
                        }
                        else if (wonItem.name == "player_damage_double")
                        {
                            Player.attackPower = Player.attackPower * 2;
                        }

                        if (enemyDead)
                        {
                            StartCoroutine(enemyDeadFun());
                        }
                        if (playerDead)
                        {
                            GameObject.Find("StateManager").GetComponent<StateManager>().showLoseScreen();
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
    IEnumerator enemyDeadFun()
    {
        yield return new WaitForSeconds(1);
        TileHilight.toMove = TileHilight.toKill.transform.position;
        Destroy(TileHilight.toKill);
        TileHilight.toKill = null;
        SceneManager.UnloadScene("FightScene");
        StateManager.isFightFinished = true;
    }
}
