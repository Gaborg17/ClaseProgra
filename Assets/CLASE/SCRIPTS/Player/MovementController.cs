using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using Fusion.Addons.SimpleKCC;

[RequireComponent(typeof(Rigidbody), typeof(GroundCheck))]
public class MovementController : NetworkBehaviour
{
    private InputManager inputManager;
    private Rigidbody rbPlayer;

    [SerializeField] private Animator _animator;

    private InputInfo input;

    private SimpleKCC simpleKCC;

    public override void Spawned()
    {
        inputManager = InputManager.Instance;
        simpleKCC = GetComponent<SimpleKCC>();
        
    }


    public override void FixedUpdateNetwork()
    {

        if (HasInputAuthority && GetInput(out input))
        {
            Movement();
            Animaciones();
        }

        

    }

    private void Animaciones()
    {
        _animator.SetBool("IsWalking", input.isMoving);
        _animator.SetBool("IsRunning", input.isRunInputPressed);
        _animator.SetFloat("WalkingZ", input.playerPosition.y);
        _animator.SetFloat("WalkingX", input.playerPosition.x);
    }

    #region Movimiento

    [SerializeField] private float walkSpeed = 5.5f;
    [SerializeField] private float runSpeed = 7.7f;
    [SerializeField] private float crouchSpeed = 3.9f;

    private void Movement()
    {
        Vector3 moveDirection = transform.forward * input.playerPosition.y + transform.right * input.playerPosition.x;
        float currentSpeed = Speed();
        Vector3 deltaMovement = moveDirection.normalized * (currentSpeed * Runner.DeltaTime);
        simpleKCC.Move(deltaMovement);
    }

    private float Speed()
    {
        return input.isMovingBackwards || input.isMovingOnXAxis ? walkSpeed :
            input.isRunInputPressed ? runSpeed : walkSpeed;
    }


    #endregion

  
}