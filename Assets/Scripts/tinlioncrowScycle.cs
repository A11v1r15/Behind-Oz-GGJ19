using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tinlioncrowScycle : MonoBehaviour
{
	int direction = 50;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine (walk ());
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate (0, 0, Time.deltaTime * direction);
	}

	IEnumerator walk (){
		direction *= -1;
		yield return new WaitForSeconds (1);
		StartCoroutine (walk ());
	}
}
