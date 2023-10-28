
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
        // Ќайдем косинус угла между нормалью контакта и вектором Vector3.up
        float cosAngle = Vector3.Dot(contact.normal.normalized, Vector3.up.normalized);
        // Ќайдем угол в градусах
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        // ≈сли угол меньше 45 градусов или равен 90 градусов, считаем контакт "землей"
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
