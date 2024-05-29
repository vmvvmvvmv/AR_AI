using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectOnPlane : MonoBehaviour
{   
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arObject;
    private const float scaleMin = .1f;
    private const float scaleMax = 1.5f;
    private const float AngleMin = 0f;
    private const float AngleMax = 360f;
    private float scale = 1.0f;
    private float angle = 0.0f;

    private void Awake() {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    public void UpdateScale(float sliderValue)  
    {
        scale = sliderValue * (scaleMax - scaleMin) + scaleMin;

        if(arObject) 
        {
            arObject.transform.localScale = Vector3.one * scale;
        }
        
    }
    public void UpdateRotate(float sliderValue) 
    {
        angle = sliderValue * (AngleMax - AngleMin) + AngleMin;

        if(arObject) 
        {
            //arObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            arObject.transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
        }
    }
    // Start is called before the first frame update
    void Update()
    {
        if(Input.touchCount == 0) {
            return;
        }

        if(arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon)) {
            var hitPose = hits[0].pose;

            if (!arObject) 
            {
                arObject = Instantiate(arRaycastManager.raycastPrefab, hitPose.position, hitPose.rotation);
                arObject.transform.localScale = Vector3.one * scale;
                arObject.transform.rotation = Quaternion.Euler(new Vector3(0,angle,0));
            }
            else 
            {
                arObject.transform.position = hitPose.position;
                arObject.transform.rotation = hitPose.rotation;
            }
            //
        }
        
    }

    // Update is called once per frame
    
}
