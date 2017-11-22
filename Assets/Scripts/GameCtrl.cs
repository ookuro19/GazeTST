using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour {

	public GameObject CharacterParent;
	Animator anima;
	// Use this for initialization
	void Start () {
		anima = CharacterParent.GetComponent<Animator> ();
		anima.SetBool ("Start", false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartDancing()
	{
		anima.SetBool ("Start", true);
	}

}
