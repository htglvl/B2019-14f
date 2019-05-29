using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyiconcontroller : MonoBehaviour
{
    public SpriteRenderer amaze, Overwatch, up, bottom;
    public DestroyMySelf health;
    public float amazeTrans, OverwatchTrans, FirstShootDelay, DazeTimeDelay, fullHealth, currentHealth, uptrans, bottomtrans, red;
    // Start is called before the first frame update
    void Start()
    {
        Overwatch.color = new Color (1, 0, 0, 1);
        FirstShootDelay = GetComponent<enemyshooting>().TgDoiPhatDauTien;
        DazeTimeDelay = GetComponent<enemymove>().dazetime;
        amaze.color = new Color (1, 1, 0, 1);
        fullHealth = health.Max_health;
        currentHealth = health.health;
        fullHealth = health.Max_health;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = health.health;
        red =  currentHealth/fullHealth;
        up.color = new Color (1, red, red, 1);
        bottom.color = new Color (1, red, red, 1);
        amazeTrans = GetComponent<enemymove>().pri_dazetime / DazeTimeDelay;
        if (GetComponent<enemymove>().daze == false)
        {
            amazeTrans = GetComponent<enemyshooting>().Pri_TgDoiPhatDauTien /FirstShootDelay;    
        }
        amaze.color = new Color (1, 1, 0, amazeTrans);

    }
}
