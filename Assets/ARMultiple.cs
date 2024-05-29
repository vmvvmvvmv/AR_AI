using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARMultiple : MonoBehaviour
{   
    [SerializeField]
    ARRaycastManager arRaycastManager;

    GameObject selectedPrefab;

    private static List<ARRaycastHit> arHits = new List<ARRaycastHit>();

    public void SetSelectedPrefab(GameObject selectedPrefab) {
        this.selectedPrefab = selectedPrefab;
    }

    private void Awake() {
        selectedPrefab = arRaycastManager.raycastPrefab;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) 
            return;
        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = touch.position;

        if(arRaycastManager.Raycast(touchPosition, arHits, TrackableType.PlaneWithinPolygon)) {
            Pose hitPose = arHits[0].pose;
            Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
        }

        


    }
}
