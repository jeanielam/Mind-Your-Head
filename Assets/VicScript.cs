﻿using UnityEngine;
using System.Collections;

public class VicScript : MonoBehaviour 
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Application.LoadLevel("level2");
		}	
	}
}
