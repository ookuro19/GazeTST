using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.XR.iOS;

public class ActorGenerateCtrl : MonoBehaviour
{
	// Prefabs.
	public GameObject[] prefabsNeedsActivation;
	public GameObject[] prefabsOnTimeline;

	GameObject[] objectsNeedsActivation;
	GameObject[] objectsOnTimeline;

	private Vector3 actorPosition;
	private Quaternion actorRotation;
	private Camera mainCamera;
	private GameObject hitGameObject;
	private Quaternion targetRotation;
	private Ray ray;
	private RaycastHit hitInfo;

	void Start()
	{
		mainCamera = Camera.main;
		InitCtrl ();
	}

	void Update()
	{
		#if UNITY_EDITOR
		PlaceWithClick();
		#else
		PlaceWithTouch();
		#endif
	}

	public void SetGlobalPosition()
	{
		GlobalForChangeScene.SetGlobalPosition (actorPosition,actorRotation);
	}

	void InitCtrl()
	{
		ARSessionCtrl.Instance.StartARSession ();
	}

	void PlaceWithTouch ()
	{
		//使用Raycast的方法获得碰撞点坐标
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				if (EventSystem.current.IsPointerOverGameObject (touch.fingerId)) {
				} else {
					ray = mainCamera.ScreenPointToRay (touch.position);
					PlaceWithRaycast (ray);
				}
			}
		}
	}

	void PlaceWithClick ()
	{		
		//使用Raycast的方法获得碰撞点坐标
		if (Input.GetMouseButtonDown(0)) {
			ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			PlaceWithRaycast (ray);
//			if (EventSystem.current.IsPointerOverGameObject ()) {
//				
//			} else {
//				
//			}
		}
	}

	void PlaceWithRaycast(Ray _ray)
	{
		if (Physics.Raycast (_ray, out hitInfo)) {
			Debug.DrawLine (_ray.origin, hitInfo.point); //只有scene中看到等射线
			hitGameObject = hitInfo.collider.gameObject;
			if (hitGameObject.tag == "Plane") {
				//确定主角摆放位置及角度
				actorPosition = hitGameObject.transform.position;
				actorRotation = Quaternion.Euler (new Vector3 (0, mainCamera.transform.eulerAngles.y, 0));
				HitSceneGenerator (actorPosition, actorRotation);
			}
		}
	}

	void HitSceneGenerator(Vector3 tPosition, Quaternion tRotation)
	{
		// Instantiate the prefabs.
		objectsNeedsActivation = new GameObject[prefabsNeedsActivation.Length];
		for (var i = 0; i < prefabsNeedsActivation.Length; i++)
			objectsNeedsActivation [i] = (GameObject)Instantiate (prefabsNeedsActivation [i], tPosition, tRotation);

		objectsOnTimeline = new GameObject[prefabsOnTimeline.Length];
		for (var i = 0; i < prefabsOnTimeline.Length; i++)
			objectsOnTimeline [i] = (GameObject)Instantiate (prefabsOnTimeline [i], tPosition, tRotation);
	}
}