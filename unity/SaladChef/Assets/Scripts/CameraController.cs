using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // main camera
    [SerializeField] private Camera cam;
    // Players List
    [SerializeField] private List<Transform> _players;
    // maximum zoom for field of view
    private float _maxZoom = 51.5f;
    // minumam  zoom for field of view
    private float _minZoom = 60f;
    // zoom limit this value related to z valu of camera
    private float _zoomLimit = 12f;
    // transition speed
    private float _speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void LateUpdate()
    {
        Zoom();
    }

    // Camera Zoom based on player distance
    private void Zoom()
    {
        float cameraZoom = Mathf.Lerp(_maxZoom, _minZoom, GetPlayerDistance() / _zoomLimit);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, cameraZoom, Time.deltaTime*_speed); 
    }

    private float GetPlayerDistance()
    {
        Bounds bound = new Bounds(_players[0].position, Vector3.zero);
        bound.Encapsulate(_players[0].position);
        bound.Encapsulate(_players[1].position);

        float zoom = bound.size.x > bound.size.y ? bound.size.x : bound.size.y;
        return zoom;
    }
}
