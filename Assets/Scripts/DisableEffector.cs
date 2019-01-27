using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisableEffector : MonoBehaviour
{
	char key;

	// Start is called before the first frame update
	void Start ()
	{
		key = (char)Random.Range ('a','z');
		this.gameObject.GetComponentInChildren<TextMeshProUGUI> ().text = key.ToString ().ToUpper();
	}

	// Update is called once per frame
	void Update ()
	{
		if (AuntM.singleton.currentS == 0 || Input.GetKey (key.ToString())) {
				this.GetComponent<AreaEffector2D> ().enabled = true;
			} else {
				this.GetComponent<AreaEffector2D> ().enabled = false;
			}
	}
}
