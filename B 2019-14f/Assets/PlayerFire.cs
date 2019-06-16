using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    public Image slider;
    public GameObject leftbullet, rightbullet;
    private GameObject bullet;
    public Animator firearm;
    public Transform fireposChuaC, fireposDaC;
    public Transform fireposFinal;
    public float fireRate, reloadTime, bulletleft;
    private float privatebulletleft, nextfire, Button;
    // Start is called before the first frame update
    void Start()
    {
        privatebulletleft = bulletleft;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.fillAmount = privatebulletleft / bulletleft;
        if (Time.time > nextfire)
        {
            firearm.SetBool("isReload", false);
        }
        if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > nextfire && privatebulletleft > 0)
        {
            privatebulletleft--;
            nextfire = Time.time + fireRate;
            RealFire();
            FindObjectOfType<audiomanager>().Play("usp sound");
        }
        if (privatebulletleft == 0 && CrossPlatformInputManager.GetButtonDown("Fire1") && Time.time > nextfire)
        {
            firearm.SetBool("isReload", true);
            nextfire = Time.time + fireRate + reloadTime;
            privatebulletleft = bulletleft;
        }
    }
    public void fire(bool m_FacingRight, float horizontalmove, bool Crouch)
    {
        if (Crouch)
        {
            fireposFinal.position = new Vector2(fireposChuaC.position.x, fireposDaC.position.y);
        }
        else
        {
            fireposFinal.position = new Vector2(fireposChuaC.position.x, fireposChuaC.position.y);
        }
        if (m_FacingRight)
        {
            rightbullet.GetComponent<bulletcontroller>().speed =
            rightbullet.GetComponent<bulletcontroller>().speedRaw + horizontalmove;
            bullet = rightbullet;
        }
        else
        {
            leftbullet.GetComponent<bulletcontroller>().speed =
            leftbullet.GetComponent<bulletcontroller>().speedRaw + horizontalmove;
            bullet = leftbullet;
        }

    }


    void RealFire()
    {
        Instantiate(bullet, fireposFinal.position, Quaternion.identity);
    }

}
