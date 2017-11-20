using UnityEngine;
using System.Collections;

public class StageDirector : MonoBehaviour
{
    // Prefabs.
    public GameObject[] prefabsNeedsActivation;
    public GameObject[] prefabsOnTimeline;

    GameObject[] objectsNeedsActivation;
    GameObject[] objectsOnTimeline;

    void Awake()
    {
        // Instantiate the prefabs.
        objectsNeedsActivation = new GameObject[prefabsNeedsActivation.Length];
        for (var i = 0; i < prefabsNeedsActivation.Length; i++)
            objectsNeedsActivation[i] = (GameObject)Instantiate(prefabsNeedsActivation[i]);

        objectsOnTimeline = new GameObject[prefabsOnTimeline.Length];
        for (var i = 0; i < prefabsOnTimeline.Length; i++)
            objectsOnTimeline[i] = (GameObject)Instantiate(prefabsOnTimeline[i]);
    }

    void Update()
    {

    }
		
    public void EndPerformance()
    {
        Application.LoadLevel(0);
    }
}
