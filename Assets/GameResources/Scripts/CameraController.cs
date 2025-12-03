using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 25f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 100f;
    public float minY = 25f;
    public float maxY = 100f;


    private bool doMovement = true;
    
    void Update()
    {
        #region Disabled when Game Over
        if (GameManager.isGameOver)
        {
            this.enabled = false;
            return;
        }

        #endregion
        #region Enable/Disable Camera Movement
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }
        #endregion

        #region Camera Panning
        //Move up Camera
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        //Move down Camera
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        //Move right Camera
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        //Move left Camera
        if (Input.GetKey("a") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        #region Scroll Zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 cameraPosition = transform.position;
        
        cameraPosition.y -= scroll * 500 * scrollSpeed * Time.deltaTime;
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minY, maxY);


        transform.position = cameraPosition;
        #endregion
    }
}
