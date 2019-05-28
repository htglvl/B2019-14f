using UnityEngine;

public class DestroyMySelf : MonoBehaviour {
    public float health = 0f;
    public float Max_health = 100f;
    public bool haveitem = false, isplayer = false, particle = false, CanBeDestroy = true;

    //not yet
   // public GameObject destroyEffect;
  void Start()
  {
      health = Max_health;
  }
  void Update() {
    if (CanBeDestroy == true)
    {
      if (health <= 0)
      {
        health = 0;
        Destroy(gameObject);
        if (haveitem == true)
        {
          GetComponent<DropItem>().caclculateLoot();    
        }
        if (isplayer == true)
        {
          FindObjectOfType<gamemanager>().endgame();
          GameObject.FindGameObjectWithTag("OneWayPlatform").SetActive(false);
        }
          //not yet
          //Instantiate(destroyEffect, transform.position, Quaternion.identity);
      }else if (particle == true)
      {
        Destroy(gameObject, 1f );
      }  
      if (health >= Max_health)
      {
        health = Max_health;
      }
    }        
  }
}

