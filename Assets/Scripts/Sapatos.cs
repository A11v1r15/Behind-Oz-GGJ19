﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sapatos : MonoBehaviour
{
	private GameObject player;
	// Start is called before the first frame update
	void Start ()
	{
		player = GameObject.Find ("Player");
	}

	void Update ()
	{
		GetComponent<SpriteRenderer> ().color = Color.HSVToRGB (Mathf.Cos(Time.frameCount*Mathf.PI/180)*0.5f+0.5f,1,1);
		if (Vector2.Distance (this.transform.position, player.transform.position) < 0.5f) {
			AuntM.singleton.ChangeSapatos ((MapGen.OZ) ? Random.Range (0, 5) : Random.Range (0, 4));
			Destroy (this.gameObject);
		}
	}
}
