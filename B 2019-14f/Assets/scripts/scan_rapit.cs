using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class scan_rapit : MonoBehaviour
{
    AstarData data = AstarPath.active.data;
    GridGraph gg;
    public float repeat;
    public Transform Cameraholder;
    // Start is called before the first frame update
     
    void Start()
    {
        //gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
        InvokeRepeating("Scan", 0f, repeat);
    }
    // Update is called once per frame
    void Update()
    {
        //gg.center = Cameraholder.transform.position;
    }
    void Scan()
    {
        //gg.center = new Vector3(10, 0, 0);
        // Updates internal size from the above values
        //gg.SetDimensions(width, depth, nodeSize);
        // Scans all graphs
        AstarPath.active.Scan();
    }
    private void Awake()
    {
        float nodeSize = 1f;
        int depth = 50;
        int width = 50;
        AstarData data = AstarPath.active.data;
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
        gg.rotation = new Vector3(90, 0, 0);
        gg.SetDimensions(width, depth, nodeSize);
        gg.center = new Vector3(10, 0, 0);
        gg.open = true;

    }
    void Start()
    {
        //gg = data.AddGraph(typeof(GridGraph)) as GridGraph;
        InvokeRepeating("Scan", 0f, repeat);

    }
    void Scan()
    {
        AstarPath.active.Scan();
    }
}