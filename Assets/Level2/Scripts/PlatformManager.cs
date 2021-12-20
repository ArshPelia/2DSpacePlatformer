using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	public static PlatformManager Instance = null;

	[SerializeField]
	GameObject platformPrefab;

	void Awake()
	{
		if (Instance == null) 
			Instance = this;
		else if (Instance != this)
			Destroy (gameObject);
		
	}
	// Use this for initialization
	void Start () {
		Instantiate (platformPrefab, new Vector2 (91.5f, -1.5f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(96.9f, 1.2f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(96.9f, -4.1f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(103.6f, -3f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(108.3f, .1f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(103.1f, 3.14f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(95.9f, 5.4f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(104.7f, 6.43f), platformPrefab.transform.rotation);
		Instantiate(platformPrefab, new Vector2(108.13f, 9.38f), platformPrefab.transform.rotation);

	}

	IEnumerator SpawnPlatform(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (2f);
		Instantiate (platformPrefab, spawnPosition, platformPrefab.transform.rotation);
	}

}
