using UnityEngine;

public class frezzeYPos : MonoBehaviour
{
    public float yPos;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
