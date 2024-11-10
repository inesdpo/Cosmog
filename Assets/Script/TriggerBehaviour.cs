using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehaviour : MonoBehaviour
{
    public string triggeringTag = "Player";  // Only trigger if the object has this tag

    // Called when another collider enters the trigger zone
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (string.IsNullOrEmpty(triggeringTag) || other.CompareTag(triggeringTag))
        {
            Debug.Log($"{other.name} has entered the trigger.");
            OnEnterAction(other);
        }
    }

    // Called when another collider stays within the trigger zone
    protected virtual void OnTriggerStay(Collider other)
    {
        if (string.IsNullOrEmpty(triggeringTag) || other.CompareTag(triggeringTag))
        {
            OnStayAction(other);
        }
    }

    // Called when another collider exits the trigger zone
    protected virtual void OnTriggerExit(Collider other)
    {
        if (string.IsNullOrEmpty(triggeringTag) || other.CompareTag(triggeringTag))
        {
            Debug.Log($"{other.name} has exited the trigger.");
            OnExitAction(other);
        }
    }

    // Virtual methods for custom actions on enter, stay, and exit
    protected virtual void OnEnterAction(Collider other) { }
    protected virtual void OnStayAction(Collider other) { }
    protected virtual void OnExitAction(Collider other) { }
}
