using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tornado : MonoBehaviour
{
	private GameObject player;
    // Start is called before the first frame update
    void Start() {
		player = GameObject.Find ("Player");
    }

	void Update(){
		if (Vector2.Distance(this.transform.position, player.transform.position) < 0.5f) {
			if (MapGen.OZ) {
				SceneManager.LoadScene ("GameOver");
			} else {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
			PlayerPrefs.SetInt ("World", PlayerPrefs.GetInt("World", 0) + 1);
		}
	}
}
