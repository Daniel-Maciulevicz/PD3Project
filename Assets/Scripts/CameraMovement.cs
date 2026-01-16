using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform _target;

    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Vector3 _rotation;

    private void FixedUpdate()
    {
        if (_target != null)
        {
            transform.localPosition = _target.position + _offset;
            transform.rotation = Quaternion.Euler(_rotation);
        }
    }
}
