using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalForChangeScene {

	//Global position of all hitScene
	private static Vector3 _globalPosition = Vector3.zero;
	public static Vector3 globalPosition
	{ 
		get { return _globalPosition; } 
		private set { _globalPosition = value; }
	}

	private static Quaternion _globalRotation;
	public static Quaternion globalRotation
	{ 
		get { return globalRotation; } 
		private set { globalRotation = value; }
	}

	//Does globalPosition assigned
	private static bool _IsPositionAssigned = false;
	public static bool IsPositionAssigned
	{ 
		get { return _IsPositionAssigned; } 
		private set { _IsPositionAssigned = value; }
	}

	public static void SetGlobalPosition(Transform tTransform)
	{
		IsPositionAssigned = true;
		globalPosition = tTransform.position;
		globalRotation = tTransform.rotation;
	}
}