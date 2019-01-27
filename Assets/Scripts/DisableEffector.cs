using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEffector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (AuntM.singleton.currentS == 0) {
			this.GetComponent<AreaEffector2D> ().enabled = false;
		} else {
			this.GetComponent<AreaEffector2D> ().enabled = true;
		}
    }
}
