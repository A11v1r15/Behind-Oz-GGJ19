using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class AuntM : MonoBehaviour
{
	private Rigidbody2D rgdbd;
	private Animator anmtr;
	private static bool S0 = false;
	private static bool S1 = false;
	private static bool S2 = false;
	private static bool S3 = false;
	private static bool S4 = false;
	public int currentS = -1;
	public static AuntM singleton;

	public List<Button> buttons;

	// Start is called before the first frame update
	void Start ()
	{
		rgdbd = GetComponent<Rigidbody2D> ();
		anmtr = GetComponent<Animator> ();
		singleton = GetComponent<AuntM> ();
		foreach (var item in buttons) {
			item.gameObject.SetActive (false);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey ("a") || Input.GetKey (KeyCode.LeftArrow)) {
			this.transform.localScale = new Vector3 (-0.1f, this.transform.localScale.y, 0.1f);
			rgdbd.AddForce (Vector2.left * 3);
		}
		if (Input.GetKey ("d") || Input.GetKey (KeyCode.RightArrow)) {
			this.transform.localScale = new Vector3 (0.1f, this.transform.localScale.y, 0.1f);
			rgdbd.AddForce (Vector2.right * 3);
		}
		if (Input.GetKeyDown ("w") || Input.GetKeyDown (KeyCode.UpArrow)) {
			if (currentS == 3 || GameObject.Find ("Plataformas").GetComponent<Tilemap> ().GetSprite ((GameObject.FindObjectOfType<Grid> ().LocalToCell (this.transform.position + new Vector3 (0, -0.5f, 0)))) != null) {
				rgdbd.AddForce (Vector2.up * 300);
			}
		}
		if (currentS == 4 && Input.GetKeyDown (KeyCode.Space)) {
			       if (Input.GetKey ("a") || Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.SetPositionAndRotation (GameObject.FindObjectOfType<Grid> ().CellToLocal (GameObject.FindObjectOfType<Grid> ().LocalToCell (this.transform.position) + new Vector3Int (-2, 0, 0)), Quaternion.identity);
			} else if (Input.GetKey ("d") || Input.GetKey (KeyCode.RightArrow)) {
				this.transform.SetPositionAndRotation (GameObject.FindObjectOfType<Grid> ().CellToLocal (GameObject.FindObjectOfType<Grid> ().LocalToCell (this.transform.position) + new Vector3Int (2, 0, 0)), Quaternion.identity);
			} else if (Input.GetKey ("w") || Input.GetKey (KeyCode.UpArrow)) {
				this.transform.SetPositionAndRotation (GameObject.FindObjectOfType<Grid> ().CellToLocal (GameObject.FindObjectOfType<Grid> ().LocalToCell (this.transform.position) + new Vector3Int (0, 2, 0)), Quaternion.identity);
			} else if (Input.GetKey ("s") || Input.GetKey (KeyCode.DownArrow)) {
				this.transform.SetPositionAndRotation (GameObject.FindObjectOfType<Grid> ().CellToLocal (GameObject.FindObjectOfType<Grid> ().LocalToCell (this.transform.position) + new Vector3Int (0, -2, 0)), Quaternion.identity);
			}
		}
		if (S0 && Input.GetKeyDown (KeyCode.Alpha1)) {
			ChangeSapatos (0);
		}
		if (S1 && Input.GetKeyDown (KeyCode.Alpha2)) {
			ChangeSapatos (1);
		}
		if (S2 && Input.GetKeyDown (KeyCode.Alpha3)) {
			ChangeSapatos (2);
		}
		if (S3 && Input.GetKeyDown (KeyCode.Alpha4)) {
			ChangeSapatos (3);
		}
		if (S4 && Input.GetKeyDown (KeyCode.Alpha5)) {
			ChangeSapatos (4);
		}
		if (S4 && Input.GetKeyDown (KeyCode.Alpha0)) {
			ChangeSapatos (-1);
		}
		if (S0) {
			buttons [0].gameObject.SetActive (true);
		}
		if (S1) {
			buttons [1].gameObject.SetActive (true);
		}
		if (S2) {
			buttons [2].gameObject.SetActive (true);
		}
		if (S3) {
			buttons [3].gameObject.SetActive (true);
		}
		if (S4) {
			buttons [4].gameObject.SetActive (true);
		}
		switch (currentS) {
		case 0:
			this.GetComponent<SpriteRenderer> ().color = Color.cyan;
			break;
		case 1:
			this.GetComponent<SpriteRenderer> ().color = Color.yellow;
			break;
		case 2:
			this.GetComponent<SpriteRenderer> ().color = Color.blue;
			break;
		case 3:
			this.GetComponent<SpriteRenderer> ().color = Color.green;
			break;
		case 4:
			this.GetComponent<SpriteRenderer> ().color = Color.red;
			break;
		default:
			this.GetComponent<SpriteRenderer> ().color = Color.white;
			break;
		}
		/*if (rgdbd.velocity.y < 0) {
			this.transform.localScale = new Vector3 (this.transform.localScale.x, -0.1f, 0.1f);
		} else {
			this.transform.localScale = new Vector3 (this.transform.localScale.x, 0.1f, 0.1f);
		}*/
		anmtr.SetBool ("idle", (rgdbd.velocity == Vector2.zero));
	}

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("Hei!");
	}

	public void ChangeSapatos (int i)
	{
		currentS = i;
		switch (i) {
		case 0:
			AuntM.S0 = true;
			break;
		case 1:
			AuntM.S1 = true;
			break;
		case 2:
			AuntM.S2 = true;
			break;
		case 3:
			AuntM.S3 = true;
			break;
		case 4:
			AuntM.S4 = true;
			break;
		}
	}
}

//GameObject.Find("Plataformas").GetComponent<Tilemap>().GetSprite((GameObject.FindObjectOfType<Grid> ().LocalToCell(this.transform.position + new Vector3(0,-0.5f,0)))).name