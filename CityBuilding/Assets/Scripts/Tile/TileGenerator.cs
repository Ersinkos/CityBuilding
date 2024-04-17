using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
	[SerializeField] GameObject TilePrefab;
	public int mapSize;

	private void Start()
	{
		GenerateTiles();
	}
	private void GenerateTiles()
	{
		for (int i = -mapSize; i <= mapSize; i += 2)
		{
			for (int j = -mapSize; j <= mapSize; j += 2)
			{
				Instantiate(TilePrefab, new Vector3((float)i, 0.007f, (float)j), Quaternion.identity);
			}
		}
	}

}
