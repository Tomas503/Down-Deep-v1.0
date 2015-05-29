using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {

	Text curLevel;

	// Use this for initialization
	void Start () {
		curLevel = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
		curLevel.text = "Level " + GameController.curentLevel.ToString();
	
	}
}
