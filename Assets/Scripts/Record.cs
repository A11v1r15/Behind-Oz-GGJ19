using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
	{
		PlayerPrefs.SetInt ("Record", Mathf.Max (PlayerPrefs.GetInt ("Record"), PlayerPrefs.GetInt ("World")));
		PlayerPrefs.SetInt ("RecordOz", Mathf.Max (PlayerPrefs.GetInt ("RecordOz"), PlayerPrefs.GetInt ("Oz")));
		GameObject.FindObjectOfType<Text> ().text  =     "You've got to World: " + PlayerPrefs.GetInt ("World") + "\nThe Record is World: " + PlayerPrefs.GetInt ("Record") + ((PlayerPrefs.GetInt ("Record") == PlayerPrefs.GetInt ("World")) ? "\nCongratulations!" : "");
		GameObject.FindObjectOfType<Text> ().text += "\n\nYou've got to Oz " + PlayerPrefs.GetInt ("World") + " times\nThe Record is " + PlayerPrefs.GetInt ("Record") + ((PlayerPrefs.GetInt ("Record") == PlayerPrefs.GetInt ("World")) ? " times\nCongratulations!" : " times");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}