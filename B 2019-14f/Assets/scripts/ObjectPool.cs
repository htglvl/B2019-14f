using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
	public GameObject PoolObject;
	public int poolAmount;
	List<GameObject> pooledObjects;
	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < poolAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(PoolObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}	
	}
	public GameObject GetPooledObject(){
		for (int i = 0; i < pooledObjects.Count ; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
		{
				return pooledObjects[i];
			}
		}
		GameObject obj = (GameObject)Instantiate(PoolObject);
		obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj; 
	}
}
