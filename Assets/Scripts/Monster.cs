using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
	private GameObject player;
	private Rigidbody2D rgdbd;
	int direction;

    // Start is called before the first frame update
    void Start()
	{
		rgdbd = GetComponent<Rigidbody2D> ();    
		player = GameObject.Find ("Player");    
		StartCoroutine (walk ());
    }

    // Update is called once per frame
    void Update()
	{
		rgdbd.AddForce (Vector2.right * 1.5f * direction);
		if (Vector2.Distance(this.transform.position, player.transform.position) < 0.3f) {
			if (player.GetComponent<Rigidbody2D> ().velocity.y < -0.1) {
				Destroy (this.gameObject);
			} else {
				if (MapGen.OZ) {
					SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
					PlayerPrefs.SetInt ("World", PlayerPrefs.GetInt("World", 0) + 1);
				} else {
					player.transform.SetPositionAndRotation (GameObject.FindObjectOfType<Grid> ().CellToLocal (new Vector3Int (3, -3, 0)) + new Vector3 (0.45f, 0.45f, 0), Quaternion.identity);
				}
			}
		}
    }

	IEnumerator walk (){
		direction = 1;
		yield return new WaitForSeconds (2);
		direction = -1;
		yield return new WaitForSeconds (2);
		StartCoroutine (walk ());
	}
}
