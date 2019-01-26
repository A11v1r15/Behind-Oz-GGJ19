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
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
