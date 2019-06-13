using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class timeSlow : MonoBehaviour
{
    private float normalspeed, timeSlowSpeed, ThoiGianChamRaw;
    public float ChamThoiGian, ThoiGianCham, SoLanChamThoiGian;
    // Start is called before the first frame update
    void Start()
    {
        normalspeed = GetComponent<movement>().speed;
        ThoiGianChamRaw = ThoiGianCham;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ThoiGianChamRaw);
        //dau hoac de test cho de khi build phai thay bang dau va
        if (CrossPlatformInputManager.GetButtonDown("Fire1") || CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            TimeSlowDown();
        }
    }
    public void TimeSlowDown()
    {
        Debug.Log("ts");
        if (SoLanChamThoiGian > 0)
        {
            SoLanChamThoiGian --;
            if (ThoiGianChamRaw <= 0)
            {
                timeSlowSpeed = normalspeed;
                Time.timeScale = 1f;
                ThoiGianChamRaw = ThoiGianCham;
            }
            else
            {
                ThoiGianChamRaw -= Time.time;
                Time.timeScale = ChamThoiGian;
                timeSlowSpeed = normalspeed * (ChamThoiGian + 1);
            }
        }
    }
}
