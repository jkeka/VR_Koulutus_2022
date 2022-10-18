using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ToggleTeleport : MonoBehaviour
{
    [SerializeField] private InputActionReference teleportToggleButton;

    public UnityEvent OnTeleportActivate;
    public UnityEvent OnTeleportCancel;

    private void OnEnable()
    {
        teleportToggleButton.action.performed += ActivateTeleport;
        teleportToggleButton.action.canceled += DeactivateTeleport;
    }

    private void OnDisable()
    {
        teleportToggleButton.action.performed -= ActivateTeleport;
        teleportToggleButton.action.canceled -= DeactivateTeleport;
    }

    private void ActivateTeleport(InputAction.CallbackContext obj)
    {
        OnTeleportActivate.Invoke();
    }
    private void DeactivateTeleport(InputAction.CallbackContext obj)
    {
        Invoke("TurnOffTeleport", .1f);
    }

    private void TurnOffTeleport()
    {
        OnTeleportCancel.Invoke();
    }
}
