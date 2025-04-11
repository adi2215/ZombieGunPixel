using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private WeaponParent weaponParent;
    private AgentAnimations agentAnimations;
    private ZombieMovement zombieMover;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Update()
    {
        zombieMover.MovementInput = MovementInput;
        float dis = Vector2.Distance(transform.position, PointerInput);
        if (dis < 4)
        {
           zombieMover.maxSpeed = 2f;
        } 
        else if (dis < 7 && dis >= 4)
        {
            zombieMover.maxSpeed = 2.3f;
        }
        else if (dis < 100 && dis >= 7)
        {
            zombieMover.maxSpeed = 2.7f;
        }
        //weaponParent.PointerPosition = pointerInput;
        AnimateCharacter();
    }

    public void PerformAttack()
    {
        weaponParent.Attack();
    }

    private void Awake()
    {
        agentAnimations = GetComponent<AgentAnimations>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        zombieMover = GetComponent<ZombieMovement>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(MovementInput);
    }
}
