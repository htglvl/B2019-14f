using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthdisplayforplayer : MonoBehaviour {
     public Image healthBar, filterbar;
    public float Max_filter, filterlossovertime, filterhealovertime, 
    HealthLossOverTimeByFilter, filter = 0f;
    public bool notinhealingzone = true;
    //not yet
   // public GameObject destroyEffect;
   void Start()
   {
       filter = Max_filter;
   }
    void Update() 
    {
      if (notinhealingzone)
      {
        filter -= filterlossovertime * Time.deltaTime; 
      }else
      {
          filter += filterhealovertime * Time.deltaTime;
      }
                 
      
        
        if (filter <= 0)
        {
			    GetComponent<DestroyMySelf>().health -= HealthLossOverTimeByFilter * Time.deltaTime;
          filter = 0;
        }
        healthBar.fillAmount = GetComponent<DestroyMySelf>().health / GetComponent<DestroyMySelf>().Max_health;
		    filterbar.fillAmount = filter / Max_filter;   
      if (filter > Max_filter)
      {
          filter = Max_filter;
      }
    }

}
