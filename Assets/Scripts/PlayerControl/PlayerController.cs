using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerRotationSpeed;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private IsPlayerGrounded _isPlayerGrounded;
    private InputReader _inputReader;
    private Rigidbody _playerRB;
   
    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerRotation();
        if (_inputReader.GetVerticalInput() != 0 || _inputReader.GetHorizontalInput() != 0) 
        {
            Movement();
        }
        Jump();
        //Debug.Log(_inputReader.GetIsJumping() + " input reader");
        //Debug.Log(_isPlayerGrounded.IsJumping() + " isGrounded");
    }

    private void Movement() 
    {
        //Debug.Log(_playerRB.velocity.magnitude);
        Vector3 ForwardDirection = _playerCamera.transform.forward.normalized;
        ForwardDirection.y = 0;
        //_playerRB.AddForce(ForwardDirection * _movementSpeed * _inputReader.GetVerticalInput() * Time.deltaTime);
        //_playerRB.AddForce(_playerCamera.transform.right.normalized * _movementSpeed * _inputReader.GetHorizontalInput() * Time.deltaTime);
        _playerRB.AddForce(_accelerationSpeed * Time.deltaTime * (ForwardDirection * _inputReader.GetVerticalInput() + _playerCamera.transform.right.normalized * _inputReader.GetHorizontalInput()));
        //transform.Translate(_movementSpeed * Time.deltaTime * (ForwardDirection * _inputReader.GetVerticalInput() + _playerCamera.transform.right.normalized * _inputReader.GetHorizontalInput()));
        if (_playerRB.velocity.magnitude > _playerSpeed)
        {
            _playerRB.velocity = _playerRB.velocity.normalized * _playerSpeed;
        }
        //_playerRB.velocity = _movementSpeed * Time.deltaTime * (ForwardDirection * _inputReader.GetVerticalInput() + _playerCamera.transform.right.normalized * _inputReader.GetHorizontalInput());
    }
    private void PlayerRotation() 
    {
        if (_inputReader.GetVerticalInput() != 0 || _inputReader.GetHorizontalInput() != 0) 
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_playerRB.velocity), _playerRotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(_playerRB.velocity.x, 0, _playerRB.velocity.z)), _playerRotationSpeed * Time.deltaTime);
        }
    }
    private void Jump()
    {
        if (_inputReader.GetIsJumping() && _isPlayerGrounded.IsJumping())
        {
            _playerRB.AddForce(UnityEngine.Vector2.up * _jumpForce, ForceMode.Impulse);
            //_playerRB.AddForce(UnityEngine.Vector2.up * _jumpForce, ForceMode.Impulse);
        }
    }

}
