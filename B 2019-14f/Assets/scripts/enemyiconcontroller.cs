using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyiconcontroller : MonoBehaviour
{
    public SpriteRenderer amaze, Overwatch;
    public float amazeTrans, OverwatchTrans, FirstShootDelay, DazeTimeDelay;
    // Start is called before the first frame update
    void Start()
    {
        FirstShootDelay = GetComponent<enemyshooting>().TgDoiPhatDauTien;
        DazeTimeDelay = GetComponent<enemymove>().dazetime;
        Overwatch.color = new Color (1, 0, 0, 1);
        amaze.color = new Color (1, 1, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        amazeTrans = GetComponent<enemymove>().pri_dazetime / DazeTimeDelay;
        if (GetComponent<enemymove>().daze == false)
        {
            amazeTrans = GetComponent<enemyshooting>().Pri_TgDoiPhatDauTien /FirstShootDelay;    
        }
        
        amaze.color = new Color (1, 1, 0, amazeTrans);
    }
}
