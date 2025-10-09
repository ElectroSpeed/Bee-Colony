using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private bool _isRightMouseHeld = false;
    private bool _isLeftMouseHeld = false;

    [Header("Camera Information")]
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _pivotCamera;

    [Header("Zoom Settings")]
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    private float _currentZoom;

    [Header("Rotation Settings")]
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _minRotationY;
    [SerializeField] private float _maxRotationY;
    private float _rotationX;
    private float _rotationY;

    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed;
    private Vector3 _targetPosition;
    private Vector3 _velocity = Vector3.zero;

    [Header("Map Center Restriction")]
    [SerializeField] private Transform _mapCenter;
    [SerializeField] private float _maxDistance;

    private void Start()
    {
        _currentZoom = Vector3.Distance(_playerCamera.position, _pivotCamera.position);
        _targetPosition = _pivotCamera.position;
        UpdateCameraPosition();
    }

    #region Click Detection

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.started)
            _isRightMouseHeld = true;
        else if (context.canceled)
            _isRightMouseHeld = false;
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
            _isLeftMouseHeld = true;
        else if (context.canceled)
            _isLeftMouseHeld = false;
    }

    #endregion

    #region Rotate

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!_isRightMouseHeld) return;

        Vector2 lookInput = context.ReadValue<Vector2>();

        _rotationX += lookInput.x * _sensitivity;
        _rotationY += lookInput.y * -1 * _sensitivity;

        _rotationY = Mathf.Clamp(_rotationY, _minRotationY, _maxRotationY);

        _pivotCamera.localEulerAngles = new Vector3(_rotationY, _rotationX, 0f);
    }

    #endregion

    #region Move

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_isLeftMouseHeld) return;

        Vector2 moveInput = context.ReadValue<Vector2>();

        Vector3 forward = _pivotCamera.forward;
        Vector3 right = _pivotCamera.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (-moveInput.y * forward) + (-moveInput.x * right);

        _targetPosition += move * _moveSpeed * Time.deltaTime;

        if (_mapCenter != null)
        {
            Vector3 offset = _targetPosition - _mapCenter.position;
            if (offset.magnitude > _maxDistance)
            {
                offset = offset.normalized * _maxDistance;
                _targetPosition = _mapCenter.position + offset;
            }
        }
    }

    private void LateUpdate()
    {
        _pivotCamera.position = Vector3.SmoothDamp(_pivotCamera.position, _targetPosition, ref _velocity, 0.1f);
    }

    #endregion
    
    #region Zoom

    public void OnZoom(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            Vector2 scroll = context.ReadValue<Vector2>();
            float input = scroll.y;

            _currentZoom -= input * _zoomSpeed * Time.deltaTime;
            _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

            UpdateCameraPosition();
        }
    }

    private void UpdateCameraPosition()
    {
        _playerCamera.localPosition = new Vector3(0, 0, -_currentZoom);
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        if (_mapCenter != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(_mapCenter.position, _maxDistance);
        }
    }

    #endregion
}