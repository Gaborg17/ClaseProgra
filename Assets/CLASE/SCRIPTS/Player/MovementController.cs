using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

[RequireComponent(typeof(Rigidbody), typeof(GroundCheck))]
public class MovementController : NetworkBehaviour
{
    private InputManager inputManager;
    private Rigidbody rbPlayer;

    [SerializeField] private Animator _animator;

    private InputInfo input;


    public override void Spawned()
    {
        inputManager = InputManager.Instance;
        rbPlayer = GetComponent<Rigidbody>();
        
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
        rbPlayer.linearVelocity = transform.localRotation * 
            new Vector3(input.playerPosition.x, 0, input.playerPosition.y) *
            Speed();
    }

    private float Speed()
    {
        return input.isMovingBackwards || input.isMovingOnXAxis ? walkSpeed :
            input.isRunInputPressed ? runSpeed : walkSpeed;
    }


    #endregion

  
}