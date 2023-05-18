//
// CursorHandler.cs
// Developers: Evan Bilotta
//
// This script is responsible for handling each ability and its movements.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHandler : MonoBehaviour
{
    public PlayerController playerController;
    public Transform playerPos;
    public InputAction selectAction1;
    public InputAction selectAction2;
    public InputAction selectAction3;
    public InputAction selectAction4;
    public enum Ability {
        Ability1=1,
        Ability2=2,
        Ability3=3,
        Ability4=4
    }

    public GameObject reticle;
    public GameObject attackIndicator;
    public Ability currentAbility;
    private void OnEnable() {
        selectAction1.Enable();
        selectAction2.Enable();
        selectAction3.Enable();
        selectAction4.Enable();
    }
    private void OnDisable() {
        selectAction1.Disable();
        selectAction2.Disable();
        selectAction3.Disable();
        selectAction4.Disable();
    }
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerPos = GetComponent<Transform>();
        currentAbility = Ability.Ability1;
    }

    // Update is called once per frame
    void Update()
    {
        SelectAbility();
    }

    public IEnumerator PerformAbility(Vector3 targetPosition, float action_speed) {
        playerController.OnDisable();
        switch(currentAbility) {
            case Ability.Ability1: 
                while(Vector3.Distance(playerPos.position, targetPosition) > 0.25f) {
                    playerController.castingAbility=true;
                    playerPos.position += (targetPosition - playerPos.position).normalized * action_speed * Time.deltaTime;
                    yield return null;
                }
                DisplayAbilityFX(targetPosition);
            break;

            case Ability.Ability2:
            break;

            case Ability.Ability3:
            break;

            case Ability.Ability4:
            break;
            
            default: break;
        }
        playerController.OnEnable();
        yield return null;
        
        
    }

    void DisplayAbilityFX(Vector3 destination) {
        switch(currentAbility) {
            case Ability.Ability1:
                Vector3 attackDirection = (playerPos.position + destination).normalized;
                Debug.Log(attackDirection);
                GameObject indicator = Instantiate(attackIndicator, destination, Quaternion.LookRotation(Vector3.forward, attackDirection));
                Destroy(indicator, 0.5f);
            break;
            case Ability.Ability2:
            break;
            case Ability.Ability3:
            break;
            case Ability.Ability4:
            break;
            default:
            break;

        }
    } 

    void SelectAbility() {
        if(selectAction1.IsPressed()) {
            currentAbility = (Ability)1;
        }
        if(selectAction2.IsPressed()) {
            currentAbility = (Ability)2;
        }
        if(selectAction3.IsPressed()) {
            currentAbility = (Ability)3;
        }
        if(selectAction4.IsPressed()) {
            currentAbility = (Ability)4;
        }
    }
}
