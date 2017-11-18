using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.XR.iOS;

public class ChangeSceneTST : MonoBehaviour
{
	public GameObject m_MajorCharacterParent;

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

	public void ChangeTheScene()
	{
		GlobalForChangeScene.SetGlobalPosition (m_MajorCharacterParent.transform);
		SceneManager.LoadSceneAsync ("ChangeScene_Vice");
	}

	void InitCtrl()
	{
		ARSessionCtrl.Instance.StartARSession ();
		m_MajorCharacterParent.transform.position = new Vector3 (0f, 0f, 0f);
		m_MajorCharacterParent.SetActive (false);
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
			if (EventSystem.current.IsPointerOverGameObject ()) {
			} else {
				ray = mainCamera.ScreenPointToRay (Input.mousePosition);
				PlaceWithRaycast (ray);
			}
		}
	}

	void PlaceWithRaycast(Ray _ray)
	{
		if (Physics.Raycast (_ray, out hitInfo)) {
			Debug.DrawLine (_ray.origin, hitInfo.point); //只有scene中看到等射线
			hitGameObject = hitInfo.collider.gameObject;
			if (hitGameObject.tag == "Plane") {
				if (!m_MajorCharacterParent.activeSelf) {
					//确定主角摆放位置及角度
					m_MajorCharacterParent.transform.position = hitGameObject.transform.position;
					m_MajorCharacterParent.transform.rotation = Quaternion.Euler (new Vector3 (0, mainCamera.transform.eulerAngles.y, 0));
				}
			}
		}
	}
}