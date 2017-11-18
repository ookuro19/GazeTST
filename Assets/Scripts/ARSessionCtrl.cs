using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class ARSessionCtrl : MonoBehaviour
{
	private UnityARSessionNativeInterface m_Session; //From Unity ARKit plugins
	private ARKitWorldTrackingSessionConfiguration m_config; //Unity ARKit plugins
	private UnityARSessionRunOption m_RunOption; //Unity ARKit plugins

	private static ARSessionCtrl _instance;
	public static ARSessionCtrl Instance
	{
		get
		{
			if (!_instance) {
				_instance = GameObject.FindObjectOfType (typeof(ARSessionCtrl)) as ARSessionCtrl;
				if(!_instance)
				{
					GameObject am = new GameObject ("ARSessionCtrl");
					_instance = am.AddComponent (typeof(ARSessionCtrl)) as ARSessionCtrl;
				}
			}
			return _instance;
		}
	}

	void Start()
	{
		m_Session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

	}

	/// <summary>
	/// Starts the AR session.
	/// </summary>
	public void StartARSession()
	{
		#if !UNITY_EDITOR
		m_config.alignment = UnityARAlignment.UnityARAlignmentGravityAndHeading;
		m_config.planeDetection = UnityARPlaneDetection.Horizontal;
		m_config.enableLightEstimation = true;
		m_config.getPointCloudData = true;
		m_RunOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking;
		m_Session.RunWithConfigAndOptions (m_config, m_RunOption);
		#endif
	}

	/// <summary>
	/// Stops the AR session.
	/// Change config.planeDetection from 'Horizontal' to 'one'None'
	/// Change config.getPointCloudData form 'true' to 'false'
	/// </summary>
	public void StopAndResetARSession()
	{
		#if !UNITY_EDITOR
		m_config.alignment = UnityARAlignment.UnityARAlignmentGravityAndHeading;
		m_config.planeDetection = UnityARPlaneDetection.None;
		m_config.enableLightEstimation = true;
		m_config.getPointCloudData = false;
		m_RunOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking;
		m_Session.RunWithConfigAndOptions (m_config, m_RunOption);
		#endif
	}

	/// <summary>
	/// Stop the AR session and remove the exist anchors.
	/// </summary>
	public void StopARSessionRemoveAnchor()
	{
		#if !UNITY_EDITOR
		m_config.alignment = UnityARAlignment.UnityARAlignmentGravityAndHeading;
		m_config.planeDetection = UnityARPlaneDetection.None;
		m_config.enableLightEstimation = true;
		m_config.getPointCloudData = false;
		m_RunOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors;
		m_Session.RunWithConfigAndOptions (m_config, m_RunOption);
		#endif
	}

	void OnGUI()
	{
		#if !UNITY_EDITOR
		GUI.Box (new Rect (0, 500, 300, 60), UnityARSessionNativeInterface.GetTrackingState());
		#endif
	}
}
