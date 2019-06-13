using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class enemyshooting : MonoBehaviour
{
    public float enemyfacingright, firerate = 0.5f, reloadtime = 1.5f, bulletleft, vision, thoiGianHetOverWatch, Pri_thoiGianHetOverWatch, TgDoiPhatDauTien, Pri_TgDoiPhatDauTien;
    public GameObject leftbullet, rightbullet;
    private float nextfire = 0f, privatebulletleft;
    public Transform fireposition;
    public Animator aimanim;
    void Start()
    {
        Physics2D.queriesStartInColliders = true;
        Pri_thoiGianHetOverWatch = thoiGianHetOverWatch;
        Pri_TgDoiPhatDauTien = TgDoiPhatDauTien;
    }

    void FixedUpdate()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(fireposition.position, fireposition.right * enemyfacingright, vision);
        if (hitinfo.collider != null && hitinfo.collider.CompareTag("Player"))
        {
            aimanim.SetBool("detectPlayer", true);
            GetComponent<enemymove>().speed = 0;
            if (Pri_TgDoiPhatDauTien <= 0)
            {
                Pri_TgDoiPhatDauTien = 0;
                if (GetComponent<enemymove>().daze == false)
                {
                    if (Time.time > nextfire && privatebulletleft > 0)
                    {
                        FindObjectOfType<audiomanager>().Play("ak sound");
                        privatebulletleft--;
                        nextfire = Time.time + firerate;
                        fire();
                    }
                    if (privatebulletleft == 0 && Time.time > nextfire)
                    {
                        privatebulletleft = bulletleft;
                        nextfire = Time.time + firerate + reloadtime;
                    }
                }
            }else
            {
                Pri_TgDoiPhatDauTien -= Time.deltaTime;
            }


            Debug.DrawLine(fireposition.position, hitinfo.point, Color.red);
        }
        else
        {
            if (Pri_thoiGianHetOverWatch <= 0)
            {
                aimanim.SetBool("detectPlayer", false);
                GetComponent<enemymove>().speed = 400;
                Pri_thoiGianHetOverWatch = thoiGianHetOverWatch;
                Pri_TgDoiPhatDauTien = TgDoiPhatDauTien;
            }
            else
            {
                Pri_thoiGianHetOverWatch -= Time.deltaTime;
            }
            Debug.DrawLine(fireposition.position, fireposition.position + transform.right * vision * enemyfacingright, Color.green);
        }

    }
    void fire()
    {
        if (enemyfacingright == 1)
        {
            Instantiate(rightbullet, fireposition.position, Quaternion.identity);
        }
        else
        {
            Instantiate(leftbullet, fireposition.position, Quaternion.identity);
        }
    }
}