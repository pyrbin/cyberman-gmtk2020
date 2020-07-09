using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Keyboard Input => Keyboard.current;

    public float2 MousePosition => ((float3)Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition)).xy;
    public bool SpacePressed => Input.spaceKey.wasPressedThisFrame;
    public float2 LookDirection { get; private set; } = new float2(0, 0);
    public int2 MovementInput { get; private set; } = new int2(0, 0);

    // Update is called once per frame
    void Update()
    {
        ClearInput();
        ProcessInput();
    }

    void ProcessInput()
    {
        int x = 0, y = 0;

        // WASD/Arrows Movement
        if (Input.leftArrowKey.isPressed || Input.aKey.isPressed) x = -1;
        else if (Input.rightArrowKey.isPressed || Input.dKey.isPressed) x = 1;
        if (Input.upArrowKey.isPressed || Input.wKey.isPressed) y = 1;
        else if (Input.downArrowKey.isPressed || Input.sKey.isPressed) y = -1;

        MovementInput = new int2(x, y);

        // Look direction
        LookDirection = math.normalize(MousePosition - (float2)(Vector2)transform.position);
    }

    void ClearInput()
    {
        // Reset inputs
        MovementInput = int2.zero;
        LookDirection = float2.zero;
    }
}
