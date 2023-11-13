using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
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



        // вывести в откдельный класс отвечающий за контроль анимаций персонажа
        [SerializeField] private Animator _playerTestAnimator; 
        private AnimationSwitcher _playerSwitcher;
        // вывести в откдельный класс отвечающий за контроль анимаций персонажа


        private void Start()
        {
            _playerSwitcher = new AnimationSwitcher(_playerTestAnimator); // вывести в откдельный класс отвечающий за контроль анимаций персонажа


            _inputReader = GetComponent<InputReader>();
            _playerRB = GetComponent<Rigidbody>();
            _jc = GetComponent<JumpPlayerController>();
        }

        // вывести в откдельный класс отвечающий за контроль анимаций персонажа
        private void UpdateAnimations() 
        {
            _playerSwitcher.PlayAnimation("Idle", _inputReader.GetVerticalInput() == 0 && _inputReader.GetHorizontalInput() == 0 && _isPlayerGrounded.IsJumping());
            _playerSwitcher.PlayAnimation("Run", _inputReader.GetVerticalInput() != 0 || _inputReader.GetHorizontalInput() != 0);
            _playerSwitcher.PlayAnimation("Jump", Input.GetButton("Jump"));
            _playerSwitcher.PlayAnimation("Falling", !_isPlayerGrounded.IsJumping());
        }
        // вывести в откдельный класс отвечающий за контроль анимаций персонажа
        private void FixedUpdate()
        {
            if (_isPlayerGrounded.IsJumping()) { PlayerRotation(); }
            Movement();
            Jump();
        }
        private void Update()
        {
            UpdateAnimations(); // вывести в откдельный класс отвечающий за контроль анимаций персонажа
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
}
