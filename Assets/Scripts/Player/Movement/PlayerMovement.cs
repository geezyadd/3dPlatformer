using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerRotationSpeed;
    [SerializeField] private float _playerAccelerationSpeed;
    [SerializeField] private Camera _playerCamera;
    
    [SerializeField] private float _playerAirSpeed;
    [SerializeField] private float _playerMovemenSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private IsPlayerGrounded _isPlayerGrounded;
    [SerializeField] private float _jumpDuration;

    [SerializeField] private float _playerSpeed;
    private InputReader _inputReader;
    private Rigidbody _playerRB;

    private JumpPlayerController _jc;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _playerRB = GetComponent<Rigidbody>();
        _jc = GetComponent<JumpPlayerController>();
    }

    private void FixedUpdate()
    {
        if (_isPlayerGrounded.IsJumping()) { PlayerRotation(); }
        Movement();
        Jump();
    }
    private void SpeedUpdator() 
    {
        if (_isPlayerGrounded.IsJumping()) 
        {
            _playerSpeed = _playerMovemenSpeed;
        }
        else
        {
            _playerSpeed = _playerAirSpeed;
        }
    }


    private void Movement() 
    {
        Vector3 ForwardDirection = _playerCamera.transform.forward.normalized;
        ForwardDirection.y = 0;
        _playerRB.AddForce(_playerAccelerationSpeed * Time.deltaTime * (ForwardDirection * _inputReader.GetVerticalInput() + _playerCamera.transform.right.normalized * _inputReader.GetHorizontalInput()));
        if (_playerRB.velocity.magnitude > _playerSpeed && _isPlayerGrounded.IsJumping())
        {
            _playerRB.velocity = _playerRB.velocity.normalized * _playerSpeed;
        }
    }
    private void PlayerRotation() 
    {
        if (_inputReader.GetVerticalInput() != 0 || _inputReader.GetHorizontalInput() != 0) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(_playerRB.velocity.x, 0, _playerRB.velocity.z)), _playerRotationSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if (_inputReader.GetIsJumping() && _isPlayerGrounded.IsJumping())
        {
            _jc.AnimationsPlaying(transform, _jumpDuration, !(_inputReader.GetVerticalInput() == 0 && _inputReader.GetHorizontalInput() == 0));
            Movement();
        }
        else if (_isPlayerGrounded.IsOnCollisionEnter())
        {
            _jc.StopCoroutine(); 
        }
    }

}
