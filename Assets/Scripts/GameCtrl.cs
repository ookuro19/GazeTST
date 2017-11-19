using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour {

	public GameObject CharacterParent;

	// Use this for initialization
	void Start () {
//		Invoke("InstantiateCharacterParent", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InstantiateCharacterParent()
	{
		Instantiate (CharacterParent, Vector3.forward * 3f, Quaternion.identity);
	}
}
