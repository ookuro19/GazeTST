using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	private static DontDestroyOnLoad _instance;

	public static DontDestroyOnLoad Instance
	{
		get
		{
			if (!_instance) {
				_instance = GameObject.FindObjectOfType (typeof(DontDestroyOnLoad)) as DontDestroyOnLoad;
				if(!_instance)
				{
					GameObject am = new GameObject ("DontDestroyOnLoad");
					_instance = am.AddComponent (typeof(DontDestroyOnLoad)) as DontDestroyOnLoad;
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
