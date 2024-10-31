using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    static public int health = 25;
    static public int maxHealth = 25;

    static public int attackPower = 5;

    static public List<string> powerUps = new();

    // Start is called before the first frame update
    void Start()
    {
        health = 25;
        maxHealth = 25;
        attackPower = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void removePowerUp(string name)
    {
        powerUps.Remove(name);
    }
}
