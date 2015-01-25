using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public float minTime = 5f; 
	public float maxTime = 10f;
	public float x = 6;
	public float minX = -2.5f;
	public float maxX = 5.5f;
	public float topY = 15f;
	public float z = 0.0f;
	public int count = 200;
	public GameObject prefab;
	public GameObject player;
	public float difficulty = 1f;
	
	public bool doSpawn = true;
	
	void Update() {
		maxX = player.transform.position.x+1.5f*x;
		minX = player.transform.position.x-x;
	}

	void Start() {
		difficulty *= 1.5f;
		x *= difficulty;
		Debug.Log ("Diff: "+difficulty.ToString ());
		StartCoroutine(Spawner());

	}
	
	IEnumerator Spawner() {
		while (doSpawn && count > 0) {
			for (int i=0; i<3; i++) {
				Vector3 v = new Vector3 (Random.Range (minX, maxX), topY, z);
				Instantiate (prefab, v, new Quaternion());
			}
			count--;
			yield return new WaitForSeconds (Random.Range (minTime, maxTime)/difficulty);
		}
	}
}
