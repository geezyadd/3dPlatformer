using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsPlayerGrounded : MonoBehaviour
{
    private bool _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }



    public bool IsJumping() { return _isGrounded; }
}
