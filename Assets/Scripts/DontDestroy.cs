using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	private static DontDestroy _instance;

	public static DontDestroy Instance
	{
		get
		{
			if (!_instance) {
				_instance = GameObject.FindObjectOfType (typeof(DontDestroy)) as DontDestroy;
				if(!_instance)
				{
					GameObject am = new GameObject ("DontDestroy");
					_instance = am.AddComponent (typeof(DontDestroy)) as DontDestroy;
				}
			}
			return _instance;
		}
	}

	void Awake()  
	{  
		//此脚本永不消毁，并且每次进入初始场景时进行判断，若存在重复的则销毁  
		if (_instance == null)  
		{  
			_instance = this;  
			DontDestroyOnLoad(this);  
		} 
		else if (this != _instance)  
		{  
			Destroy(gameObject);  
		}
	}  
}
