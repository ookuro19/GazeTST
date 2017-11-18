using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToActor : MonoBehaviour {

	public GameObject m_hitSceneParent;

	// Use this for initialization
	void Start () {
		StopARSessionRemoveAnchor ();
		if (GlobalForChangeScene.IsPositionAssigned) {
			m_hitSceneParent.SetActive (true);
			m_hitSceneParent.transform.position = GlobalForChangeScene.globalPosition;
			m_hitSceneParent.transform.rotation = GlobalForChangeScene.globalRotation;
		}
	}

	void StopARSessionRemoveAnchor()
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
}
