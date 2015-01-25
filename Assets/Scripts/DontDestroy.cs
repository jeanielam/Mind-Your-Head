using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	public static DontDestroy me;

	void Awake () 
	{
		if (me == null) 
		{
			me = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else 
			Destroy(gameObject);
	}
}
