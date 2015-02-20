#pragma strict

private var perspectiveZoomSpeed : float = 0.1f;        // The rate of change of the field of view in perspective mode.
private var orthoZoomSpeed : float = 0.1f;        // The rate of change of the orthographic size in orthographic mode.
public var minZoom : float = 60.0f; 
public var smoothnes : float = 3f;

private var myFieldOfView;
private var cameraZoomSpot : GameObject;
private var controllerScript : ControllerScript;

function Awake(){
	myFieldOfView = camera.fieldOfView;
	cameraZoomSpot = GameObject.FindGameObjectWithTag("CameraZoomSpot");
	controllerScript = GameObject.Find("Camera").GetComponent("ControllerScript");
}
function Update()
{	
    // If there are two touches on the device...
    if(!controllerScript.running) return;
    if (Input.touchCount == 2)
    {	

        // Store both touches.
        var touchZero = Input.GetTouch(0);
        var touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
		
        // Find the difference in the distances between each frame.
        var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        // If the camera is orthographic...
        if (camera.isOrthoGraphic)
        {
            // ... change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 3f);
            if(camera.orthographicSize > 18f){
            	camera.orthographicSize = 18f;
            }
            //camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 18f, 1f);
        }
        else
        {
            // Otherwise change the field of view based on the change in distance between the touches.
           // camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
			camera.transform.position.z -= deltaMagnitudeDiff * perspectiveZoomSpeed;
            // Clamp the field of view to make sure it's between 0 and 180.
            //camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 60f, 142.0f);
            camera.transform.position.z = Mathf.Clamp(camera.transform.position.z, -20f, -3.5f);
           
        }
    }

     if (Input.GetAxis("Mouse ScrollWheel") > 0){
     	 if (camera.isOrthoGraphic)
        {
            // ... change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize--;

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 3f);
            if(camera.orthographicSize > 18f){
            	camera.orthographicSize = 18f;
            }
            //camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 18f, 1f);
        }
        else
        {
            // Otherwise change the field of view based on the change in distance between the touches.
           // camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
			camera.transform.position.z++;
            // Clamp the field of view to make sure it's between 0 and 180.
            //camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 60f, 142.0f);
            camera.transform.position.z = Mathf.Clamp(camera.transform.position.z, -20f, -3.5f);
           
        }
     
     }else if(Input.GetAxis("Mouse ScrollWheel") < 0){
      if (camera.isOrthoGraphic)
        {
            // ... change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize ++;

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Max(camera.orthographicSize, 3f);
            if(camera.orthographicSize > 18f){
            	camera.orthographicSize = 18f;
            }
            //camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 18f, 1f);
        }
        else
        {
            // Otherwise change the field of view based on the change in distance between the touches.
           // camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
			camera.transform.position.z --;
            // Clamp the field of view to make sure it's between 0 and 180.
            //camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 60f, 142.0f);
            camera.transform.position.z = Mathf.Clamp(camera.transform.position.z, -20f, -3.5f);
           
        }
     
     }
    
}

