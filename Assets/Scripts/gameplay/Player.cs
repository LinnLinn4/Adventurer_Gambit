using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    static public int health = 100;
    static public int maxHealth = 100;

    static public int attackPower = 10;

    static public List<string> powerUps = new();

    // Start is called before the first frame update
    void Start()
    {

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
