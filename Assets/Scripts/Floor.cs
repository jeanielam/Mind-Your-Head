using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {
	
	public float duration = 0.5f;
	public float magnitude = 0.001f;
	
	public bool test = false;
	float timeToCall = 0f;
	
	void Start(){
		timeToCall = Time.time;
	}
	// -------------------------------------------------------------------------
	public void PlayShake() {
		
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	// -------------------------------------------------------------------------
	void Update() {
		
		if (Time.time > timeToCall) {
			PlayShake ();
			timeToCall = Time.time + Random.Range(5,10);
		}
		if (test) {
			test = false;
		}
	}
	
	// -------------------------------------------------------------------------
	IEnumerator Shake() {
		
		float elapsed = 0.0f;
		
		Vector3 originalCamPos = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Transform> ().position;
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;
			float x = (Random.value * 2.0f) - 1.0f + originalCamPos.x;
			float y = (Random.value * 2.0f) - 1.0f + originalCamPos.y;
			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
			
			yield return null;
		}
	}
}
