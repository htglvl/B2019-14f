using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Le : MonoBehaviour {
	[System.Serializable]
	public class DropCurrency
	{
		public string name;
		public Texture2D item;
		public int dropRarity;
	}

	public List <DropCurrency> loottable = new List<DropCurrency> ();
	private Texture2D map;
	public ColorToPrefab[] colorMappings;
	// Use this for initialization
	public void Start () {
		caclculateLoot();
		GenerateLevel();
	}
	
	public void GenerateLevel(){
		for (int x = 0; x < map.width; x++)
		{
			for (int y = 0; y < map.height; y++)
			{
				GenerateTile(x,y);
			}
		}
	}
	void GenerateTile(int x, int y){
		Color pixelColor =  map.GetPixel(x,y);
		if (pixelColor.a == 0)
		{
			//thepixel is transparent
			return;

		}
		foreach (ColorToPrefab colorMapping in colorMappings)
		{
			if (colorMapping.color.Equals(pixelColor))
			{
				Vector2 position = new Vector2(x + transform.position.x ,y + transform.position.y );
				Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
			}
		} 
	}
	public void caclculateLoot()
	{
		int itemweight = 0;
		for (int i = 0; i < loottable.Count; i++)
		{
			itemweight += loottable [i].dropRarity;
		}
		int RandomValue = Random.Range (0, itemweight);
		for (int j = 0; j < loottable.Count; j++)
		{
			if (RandomValue <= loottable[j].dropRarity)
			{
			map = loottable[j].item;
			return;
			}
			RandomValue -=loottable[j].dropRarity;
		}
	}
}
