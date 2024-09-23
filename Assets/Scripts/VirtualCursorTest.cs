using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;

public class VirtualCursorTest : MonoBehaviour
{
    private VirtualMouseInput virtualMouseInput;
    [SerializeField] private RectTransform canvasRectTransform;

    public Vector2 virtualMousePosition;

    public static VirtualCursorTest instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one VirtualCursorHandler in the scene.");
        }
        instance = this;

        virtualMouseInput = GetComponent<VirtualMouseInput>();
    }

    private void Update()
    {
        transform.localScale = Vector3.one * (1f / canvasRectTransform.localScale.x);
        transform.SetAsLastSibling();
    }

    private void LateUpdate()
    {
        virtualMousePosition = virtualMouseInput.virtualMouse.position.value;
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.x, 0f, Screen.width);
        virtualMousePosition.y = Mathf.Clamp(virtualMousePosition.y, 0f, Screen.height);
        InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePosition);
    }
}
