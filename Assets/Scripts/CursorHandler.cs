using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorHandler : MonoBehaviour
{
    public InputAction click;
    public Transform playerPos;
    public PlayerController playerController;
    public Camera cam;
    public Transform lineParent;
    public LineRenderer lineRenderer;
    public AbilityHandler abilityHandler;
    public bool drawing = false;
    Vector2 rawMousePos;
    public GameObject qCurs;
    public Transform qCursPos;
    public GameObject realDirectionIndicator;
    void Start()
    {
        cam = GetComponent<Camera>();
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        qCurs.GetComponent<SpriteRenderer>().enabled = false;
        qCursPos = qCurs.GetComponent<Transform>();
    }

    void Update()
    {    
        rawMousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        qCurs.transform.position = rawMousePos;        
        // If Player is holding down mouse, and not currently drawing a line
        
        if(click.IsPressed() && !drawing) {
            
            drawing = true;
            StartCoroutine(StartVectorDraw());
        }
    }
    
    

    private IEnumerator StartVectorDraw()
    {
        // Release cursor and set initial position to character
        //Debug.Log(playerPos.position);
        Mouse.current.WarpCursorPosition(cam.WorldToScreenPoint(playerPos.position));
        qCurs.GetComponent<SpriteRenderer>().enabled = true;
        GameObject reticle;
        reticle = Instantiate(realDirectionIndicator, playerPos.position, Quaternion.identity) as GameObject;
        // Loop runs until mouse button is released.
        while(!click.WasReleasedThisFrame()) {
            reticle.transform.position = playerPos.position;
            qCursPos.localPosition = playerPos.position + Vector3.ClampMagnitude(qCursPos.localPosition, PlayerStats.action_range);
            Vector2 mousePosDelta = playerPos.position - qCursPos.localPosition;
            reticle.transform.position = (Vector2)playerPos.position + mousePosDelta;
            Debug.Log(playerPos.position + qCursPos.position);
            lineRenderer.SetPosition(0, playerPos.position);
            lineRenderer.SetPosition(1, qCursPos.localPosition);
            yield return null;
        }
        // When loop terminates, perform ability, then reset variables

        // Call attack function here.
        StartCoroutine(abilityHandler.PerformAbility(reticle.transform.position, PlayerStats.action_speed));

        //Debug.Log("not drawing");
        ResetLineVector();
        Destroy(reticle);
        drawing = false;
        qCurs.GetComponent<SpriteRenderer>().enabled = false;
        yield return null;
    }

    public IEnumerator PerformAction(Vector3 targetPosition, float action_speed) {
        playerController.castingAbility = true;
        playerController.OnDisable();        
        while(Vector3.Distance(playerPos.position, targetPosition) > 0.5f) {
            playerController.castingAbility=true;
            playerPos.position += (targetPosition - playerPos.position).normalized * action_speed * Time.deltaTime;
            yield return null;
        }
        playerController.castingAbility=false;
        playerController.OnEnable();
        yield return null;
    }

    void ResetLineVector() {
        lineRenderer.SetPosition(0, playerPos.position);
        lineRenderer.SetPosition(1, playerPos.position);
    }
    
    private void OnEnable() {
        click.Enable();
    }
    private void OnDisable() {
        click.Disable();
    }
}