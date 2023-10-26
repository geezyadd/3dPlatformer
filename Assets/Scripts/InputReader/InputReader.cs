using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isJumping;

    public float GetHorizontalInput() { return _horizontalInput; }
    public float GetVerticalInput() { return _verticalInput; }
    public bool GetIsJumping() { return _isJumping; }

    private void Update()
    {
        InputSeter();
        IsJumping();
    }
    private void InputSeter() 
    {
        _horizontalInput = NormalizedInput("Horizontal");
        _verticalInput = NormalizedInput("Vertical");
    }
    private void IsJumping()
    {
        if (UnityEngine.Input.GetButton("Jump"))
        {
            _isJumping = true;
        }
        else
        {
            _isJumping = false;
        }
    }
    

    private int NormalizedInput(string Direction)
    {
        if (Input.GetAxis(Direction) > 0) { return 1; }
        else if (Input.GetAxis(Direction) < 0) { return -1; }
        else { return 0; }
    }
 
}
