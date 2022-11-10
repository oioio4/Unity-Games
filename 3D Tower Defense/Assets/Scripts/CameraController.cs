using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool canMove = true;
    private float panSpeed = 30f;
    private float panBorderThickness = 10f;
    private Vector2 zoomRange = new Vector2(-10, 70);
    private float CurrentZoom = 0;
    private float ZoomZpeed = 4f;
    private float ZoomRotation = 1.5f;
    private Vector2 zoomAngleRange = new Vector2(10, 70);
    private Vector3 InitPos;
    private Vector3 InitRotation;

    void Start() {
        InitPos = transform.position;
        InitRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameEnded) {
            this.enabled = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            canMove = !canMove;
        }
        if (!canMove) {
            return;
        }
        if ( Input.GetMouseButton(2)) {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed * (Input.mousePosition.x - Screen.width * 0.5f) / (Screen.width * 0.5f), Space.World);
            transform.Translate(Vector3.forward * Time.deltaTime * panSpeed * (Input.mousePosition.y - Screen.height * 0.5f) / (Screen.height * 0.5f), Space.World);
        }
        else {
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
            else if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness) {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
            else if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness) {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
        }

        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomZpeed;
 
        CurrentZoom = Mathf.Clamp( CurrentZoom, zoomRange.x, zoomRange.y );
 
        transform.position = new Vector3( transform.position.x, transform.position.y - (transform.position.y - (InitPos.y + CurrentZoom)) * 0.1f, transform.position.z );
 
        float x = transform.eulerAngles.x - (transform.eulerAngles.x - (InitRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
        x = Mathf.Clamp( x, zoomAngleRange.x, zoomAngleRange.y );
 
        transform.eulerAngles = new Vector3( x, transform.eulerAngles.y, transform.eulerAngles.z );

    }
}
