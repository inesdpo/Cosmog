using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Script.Triggers
{
    public class PlayTimelineOnDeactivationTrigger : TriggerBehaviour
    {
        [SerializeField] private GameObject objectToCheck; // The object to monitor for deactivation
        [SerializeField] private PlayableDirector timelineDirector; // The Timeline to play

        private bool hasPlayedTimeline = false; // To ensure the timeline plays only once

        protected override void OnEnterAction(Collider other)
        {
            // Start checking if the object is deactivated only when the trigger condition is met
            if (other.CompareTag("Player") && objectToCheck != null && timelineDirector != null)
            {
                StartCoroutine(CheckForDeactivation());
            }
        }

        private IEnumerator CheckForDeactivation()
        {
            // Continuously check if the object is deactivated
            while (!hasPlayedTimeline)
            {
                if (!objectToCheck.activeSelf)
                {
                    PlayTimeline();
                    hasPlayedTimeline = true; // Mark as played to prevent re-triggering
                }
                yield return null; // Wait for the next frame
            }
        }

        private void PlayTimeline()
        {
            if (timelineDirector != null)
            {
                Debug.Log("Object is deactivated! Playing Timeline.");
                timelineDirector.Play();
            }
            else
            {
                Debug.LogWarning("Timeline Director is not assigned.");
            }
        }
    }
}
