using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorHandler : MonoBehaviour
{
    public InputAction click;
    public Transform player;
    public Camera cam;
    public Transform lineParent;
    public LineRenderer lineRenderer;
    public bool drawing = false;
    Vector2 mousePos;
    [SerializeField]
    public GameObject realDirectionIndicator;
    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {            
        // If Player is holding down mouse, and not currently drawing a line
        if(click.IsPressed() && !drawing) {
            //Start drawing
            drawing = true;
            StartCoroutine(StartVectorDraw());
        }
    }
    
    private IEnumerator PerformAction(Vector3 targetPosition, float action_speed) {
        float timer = 0;
        while(action_speed > timer) {
            player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, 5.0f);
            timer += Time.deltaTime;
        }
        yield return null;
    }

    private IEnumerator StartVectorDraw()
    {
        // Release cursor and set initial position to character
        Mouse.current.WarpCursorPosition(cam.WorldToScreenPoint(player.position));
        Cursor.visible = true;
        GameObject reticle;
        reticle = Instantiate(realDirectionIndicator, player.position, Quaternion.identity) as GameObject;
        // Loop runs until mouse button is released.
        while(!click.WasReleasedThisFrame()) {
            //Debug.Log("drawing");
            lineRenderer.SetPosition(0, player.position);
            reticle.transform.position = player.position;
            mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePosDelta = (Vector2)player.position - mousePos;
            reticle.transform.position = (Vector2)player.position + mousePosDelta;
            lineRenderer.SetPosition(1, mousePos); 
            yield return null;
        }
        // When loop terminates, perform action, then reset variables
        StartCoroutine(PerformAction(reticle.transform.position, 100));
        //Debug.Log("not drawing");
        ResetLineVector();
        Destroy(reticle);
        drawing = false;
        Cursor.visible = false;
        yield return null;
    }

    void ResetLineVector() {
        lineRenderer.SetPosition(0, player.position);
        lineRenderer.SetPosition(1, player.position);
    }
    
    private void OnEnable() {
        click.Enable();
    }
    private void OnDisable() {
        click.Disable();
    }
}