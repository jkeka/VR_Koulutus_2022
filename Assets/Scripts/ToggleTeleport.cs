using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// Manages teleportation toggle input using UnityEvents
// Triggers teleport activation and cancellation based on input action state
public class ToggleTeleport : MonoBehaviour
{
    // Reference to the input action that toggles teleportation
    [SerializeField] private InputActionReference teleportToggleButton;

    // UnityEvent that gets invoked when teleportation is activated
    public UnityEvent OnTeleportActivate;

    // UnityEvent that gets invoked when teleportation is canceled.
    public UnityEvent OnTeleportCancel;

    // Subscribe to input events when the object is enabled
    private void OnEnable()
    {
        teleportToggleButton.action.performed += ActivateTeleport;
        teleportToggleButton.action.canceled += DeactivateTeleport;
    }

    // Unsubscribe to prevent memory leaks when object is disabled
    private void OnDisable()
    {
        teleportToggleButton.action.performed -= ActivateTeleport;
        teleportToggleButton.action.canceled -= DeactivateTeleport;
    }

    // Called when the teleport button is pressed
    private void ActivateTeleport(InputAction.CallbackContext obj)
    {
        OnTeleportActivate.Invoke();
    }

    // Called when the teleport button is released
    // Delay before cancelling teleport
    private void DeactivateTeleport(InputAction.CallbackContext obj)
    {
        Invoke("TurnOffTeleport", .1f);
    }

    private void TurnOffTeleport()
    {
        OnTeleportCancel.Invoke();
    }
}
