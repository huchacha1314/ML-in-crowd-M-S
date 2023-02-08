using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraConstroller : MonoBehaviour
{
    private CameraControlActions cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    //Horizontal Translation"
    [SerializeField]
    private float maxSpeed = 5f;
    private float speed;
    //Horizontal Translation
    [SerializeField]
    private float acceleration = 10f;
    //Horizontal Translation
    [SerializeField]
    private float damping = 15f;

    //Vertical Translation")]
    [SerializeField]
    private float stepSize = 2f;
    //Vertical Translation
    [SerializeField]
    private float zoomDampening = 7.5f;
    //Vertical Translation
    [SerializeField]
    private float minHeight = 5f;
    //Vertical Translation
    [SerializeField]
    private float maxHeight = 50f;
    //Vertical Translation
    [SerializeField]
    private float zoomSpeed = 2f;

    //Rotation
    [SerializeField]
    private float maxRotationSpeed = 1f;
    

    //value set in various functions 
    //used to update the position of the camera base object.
    private Vector3 _targetPosition;

    private float _zoomHeight;

    //used to track and maintain velocity w/o a rigidbody
    private Vector3 _horizontalVelocity;
    private Vector3 _lastPosition;

    //tracks where the dragging action started
    Vector3 _startDrag;
    
    /// <summary>
    /// initializes the attributes when starting the application
    /// </summary>
    private void Awake()
    {
        cameraActions = new CameraControlActions();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }
    
    
    private void OnEnable()
    {
        _zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(this.transform);
        _lastPosition = this.transform.position;
        movement = cameraActions.Camera.Movement;
        cameraActions.Camera.RotateCamera.performed += RotateCamera;
        cameraActions.Camera.ZoomCamera.performed += ZoomCamera;
        cameraActions.Camera.Enable();
    }

  
    private void OnDisable()
    {
        cameraActions.Disable();
        cameraActions.Camera.RotateCamera.performed -= RotateCamera;
        cameraActions.Camera.ZoomCamera.performed -= ZoomCamera;
    }

    /// <summary>
    /// gets calle severy frame and updates the camera according to the input
    /// </summary>
    private void Update()
    {
        GetKeyboardMovement();
        
        UpdateVelocity();
        UpdateCameraPosition();
        UpdateBasePosition();
        
    }

    /// <summary>
    /// updates the movement velocity for a smooth camera movement
    /// </summary>
    private void UpdateVelocity()
    {
        _horizontalVelocity = (this.transform.position - _lastPosition) / Time.deltaTime;
        _horizontalVelocity.y = 0f;
        _lastPosition = this.transform.position;
    }

    /// <summary>
    /// gets the input of the keyboard
    /// </summary>
    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
                             + movement.ReadValue<Vector2>().y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
            _targetPosition += inputValue;
    }

    /// <summary>
    /// returns the direction 90 degrees right of the current camera position
    /// </summary>
    /// <returns></returns> 
    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        return right;
    }
    
    /// <summary>
    /// returns the direction of the current look of the camera
    /// </summary>
    /// <returns></returns>
    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        return forward;
    }
    
    /// <summary>
    /// updastes the position according to he velocity
    /// </summary>
    private void UpdateBasePosition()
    {
        if (_targetPosition.sqrMagnitude > 0.1f)
        {
            //create a ramp up or acceleration
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += _targetPosition * speed * Time.deltaTime;
        }
        else
        {
            //create smooth slow down
            _horizontalVelocity = Vector3.Lerp(_horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += _horizontalVelocity * Time.deltaTime;
        }

        //reset for next frame
        _targetPosition = Vector3.zero;
    }
    
    /// <summary>
    /// rotates the camera for a give input
    /// </summary>
    /// <param name="inputValue"></param>
    private void RotateCamera(InputAction.CallbackContext inputValue)
    {
        if (!Mouse.current.rightButton.isPressed)
        {
            return;
        }

        float value = inputValue.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, value * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }

    /// <summary>
    /// handles the zooming with the mouse wheel
    /// </summary>
    /// <param name="inputValue"></param>
    private void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        float value = -inputValue.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(value) > 0.0f)
        {
            _zoomHeight = cameraTransform.localPosition.y + value * stepSize;
            if (_zoomHeight < minHeight)
            {
                _zoomHeight = minHeight;
            }
            else if (_zoomHeight > maxHeight)
            {
                _zoomHeight = maxHeight;
            }
            {
                
            }
        }
    }

    /// <summary>
    /// updates the camera according to all the different ways of movement
    /// </summary>
    private void UpdateCameraPosition()
    {
        var camlocalPosition = cameraTransform.localPosition;
        Vector3 zoomTarget = new Vector3(camlocalPosition.x, _zoomHeight, camlocalPosition.z);
        zoomTarget -= zoomSpeed * (_zoomHeight - camlocalPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(camlocalPosition, zoomTarget, Time.deltaTime * zoomDampening);
        cameraTransform.LookAt(this.transform);
    }
}
