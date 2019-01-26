using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuntM : MonoBehaviour
{
	private Rigidbody2D rgdbd;
	private Animator    anmtr;
    // Start is called before the first frame update
    void Start()
	{
		rgdbd = GetComponent<Rigidbody2D> ();
		anmtr = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetKey ("a") || Input.GetKey (KeyCode.LeftArrow)) {
			this.transform.localScale = new Vector3 (-0.1f, 0.1f, 0.1f);
			rgdbd.AddForce (Vector2.left * 3);
		}
		if (Input.GetKey ("d") || Input.GetKey (KeyCode.RightArrow)) {
			this.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
			rgdbd.AddForce (Vector2.right * 3);
		}
		if (Input.GetKeyDown ("w") || Input.GetKeyDown (KeyCode.UpArrow)) {
			rgdbd.AddForce (Vector2.up * 300);
		}
		anmtr.SetBool("idle",(rgdbd.velocity == Vector2.zero));
    }

	void OnTriggerEnter(Collider other){
		Debug.Log ("Hei!");
	}
}
