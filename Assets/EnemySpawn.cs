using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject parent;


    static public GameObject enemy;

    [SerializeField]
    Slider healthbar;

    [SerializeField]
    Slider playerhealthbar;
    // Start is called before the first frame update
    void Start()
    {
        GameObject q = Instantiate(TileHilight.toKill);
        q.transform.localScale = new Vector3(20f, 20f, 20f);
        q.transform.localPosition = Vector3.zero;
        q.transform.rotation = Quaternion.identity;
        q.transform.SetParent(parent.transform, false);
        enemy = q;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = Math.Clamp(enemy.GetComponent<Enemy>().health / (float)enemy.GetComponent<Enemy>().maxHealth, 0, 1);
        playerhealthbar.value = Math.Clamp(Player.health / (float)Player.maxHealth, 0, 1);
    }
}
