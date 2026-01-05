using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool autoMove = true;

    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0.0f;
        if (autoMove)
        {
            move = speed * Time.deltaTime;
        }
        else
        {
            move = moveInput.x * speed * Time.deltaTime;
        }

        transform.position += new Vector3(move, 0.0f, 0.0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    internal void SetAutoMove(bool value)
    {
        autoMove = value;
    }

    internal void SetSpeed(float newSpeed)
    {
        throw new NotImplementedException();
    }
}
