using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class TrackingQualityCtrl : MonoBehaviour {

	public Slider trackingQuality_Slider;

	private UnityARSessionNativeInterface m_Session; //From Unity ARKit plugins
	private UnityARGeneratePlane m_PlaneGenerator;
	private bool IsScanning = true;
	private ARTrackingState curTrackingState;
	private float trackingQuality = 0f;
	private float trackingQualityLimit = 0f;

	// Use this for initialization
	void Start () {
		m_Session = UnityARSessionNativeInterface.GetARSessionNativeInterface ();
		m_PlaneGenerator = gameObject.GetComponent<UnityARGeneratePlane> ();
		trackingQuality_Slider.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (trackingQuality_Slider.gameObject.activeSelf) {
			#if !UNITY_EDITOR
			trackingQuality_Slider.value = TrackingQuality ();
			#else
			trackingQuality_Slider.gameObject.SetActive (false);
			#endif
		}
	}

	public float TrackingQuality()
	{
		//UnityARSessionNativeInterface.GetTrackingState() is defined by myself
		curTrackingState = UnityARSessionNativeInterface.GetTrackingState ();

		if (curTrackingState == ARTrackingState.ARTrackingStateNotAvailable) {
			trackingQualityLimit = 0f;
		}else if (curTrackingState == ARTrackingState.ARTrackingStateLimited) {
			trackingQualityLimit = 0.1f;
		}else{
			trackingQualityLimit = 0.7f;
			//UnityARSessionNativeInterface.GetAnchorsNum() is defined by myself
			if(m_PlaneGenerator.GetAnchorsNum() > 0)
			{
				if (trackingQuality > 0.98f) {
					trackingQuality_Slider.gameObject.SetActive (false);
				}
				trackingQualityLimit = 1f;
			}
		}
		trackingQuality += Random.Range (0.02f, 0.05f);
		trackingQuality = Mathf.Clamp (trackingQuality, 0, trackingQualityLimit);
		return trackingQuality;
	}
}
