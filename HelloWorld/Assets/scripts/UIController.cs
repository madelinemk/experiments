using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	Text myTextHello;

	// Use this for initialization
	void Start () {
		myTextHello = GameObject.Find("Canvas/TextHello").GetComponent<Text>();
		myTextHello.text = "hello world, what's up?";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
