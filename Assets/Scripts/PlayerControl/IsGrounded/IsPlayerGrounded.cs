
using System.Numerics;

using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class IsPlayerGrounded : MonoBehaviour
{
    public bool IsJumping() { return _isGrounded; }
    private bool _isGrounded;
    private Vector3 _normal;
    [SerializeField] private float _correctJumpAngle;

    private void OnCollisionStay(Collision collision)
    {
        _normal = collision.contacts[0].normal;
        if (IsContactGrounded(collision.contacts[0]) || IsContactGrounded(collision.contacts[1]))
        {
            _isGrounded = true;
        }
    }

    private bool IsContactGrounded(ContactPoint contact)
    {
        // ������ ������� ���� ����� �������� �������� � �������� Vector3.up
        float cosAngle = Vector3.Dot(contact.normal.normalized, Vector3.up.normalized);
        // ������ ���� � ��������
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        // ���� ���� ������ 45 �������� ��� ����� 90 ��������, ������� ������� "������"
        return angle < _correctJumpAngle;
    }
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
    }
}
