using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _cam;
    private CameraArea[] cameraAreas;
    public Transform startPosition;
    public float lerpSpeedMultiplier = 5.0f;
    private Vector3 _targetPosition;

    private void Awake()
    {
        // Get Scene areas
        GetSceneCameraAreas();
    }

    private void Start()
    {
        _cam = Camera.main;
        
        // Set camera default target
        SetCameraTarget(startPosition);
        

        // Setup listeners for camera areas
        SetupAreaListeners();
    }

    private void Update()
    {
        // Update camera position
        HandleCameraPosition();
    }

    private void OnDisable()
    {
        // Remove listeners when scene is over
        DisableAreaListeners();
    }

    private void GetSceneCameraAreas()
    {
        // Find all area objects
        GameObject[] areaObjects = GameObject.FindGameObjectsWithTag("CameraArea");
        
        // initialize area array
        cameraAreas = new CameraArea[areaObjects.Length];

        // get cameraarea component and set appropriately
        for (int i = 0; i < areaObjects.Length; i++)
        {
            CameraArea cameraArea = areaObjects[i].GetComponent<CameraArea>();
            cameraAreas[i] = cameraArea;
        }
    }

    // Set all listeners for camera areas
    private void SetupAreaListeners()
    {
        foreach (CameraArea cameraArea in cameraAreas)
        {
            cameraArea.OnEntry.AddListener(SetCameraTarget);
        }
    }

    // Remove all listeners for camera areas
    private void DisableAreaListeners()
    {
        foreach (CameraArea cameraArea in cameraAreas)
        {
            cameraArea.OnEntry.RemoveListener(SetCameraTarget);
        }
    }

    private void HandleCameraPosition()
    {
        // break from logic if camera is at the target position
        if (_cam.transform.position == _targetPosition)
            return;
        
        // lerp to targetposition
        _cam.transform.position = Vector3.Lerp(_cam.transform.position, _targetPosition, Time.deltaTime * lerpSpeedMultiplier);
    }

    // sets target position to a given position, sets z to cam.pos.z 
    private void SetCameraTarget(Transform target)
    {
        _targetPosition = target.position;
        _targetPosition.z = _cam.transform.position.z;
    }

}
