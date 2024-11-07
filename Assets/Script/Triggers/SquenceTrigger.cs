using UnityEngine;
using UnityEngine.Playables;

namespace Script.Triggers
{
    /// <summary>
    /// This scripts triggers the timeline to play and then deactivate object when timeline completes
    /// </summary>
    public class SquenceTrigger : TriggerBehaviour
    {
        public PlayableDirector playableDirector; // Reference to the PlayableDirector component
        public GameObject objectToDeactivate;
        public bool stopOnExit = false;            // Option to stop the timeline when the player exits the trigger


        private void Start()
        {
            if (playableDirector != null)
            {
                // Ensure we subscribe to the stopped event of the PlayableDirector
                playableDirector.stopped += OnTimelineStopped;
            }
            else
            {
                Debug.LogWarning("PlayableDirector is not assigned in TimelineTrigger.");
            }

            // Ensure the objectToDeactivate is set to this GameObject if not assigned
            if (objectToDeactivate == null)
            {
                objectToDeactivate = gameObject;
            }
        }
        protected override void OnEnterAction(Collider other)
        {
            if (playableDirector != null)
            {
                Debug.Log("Playing Timeline sequence!");
                playableDirector.Play();  // Play the Timeline
            }
        }

        protected override void OnExitAction(Collider other)
        {
            if (stopOnExit && other.CompareTag(triggeringTag))
            {
                if (playableDirector != null)
                {
                    Debug.Log("Stopping Timeline sequence.");
                    playableDirector.Stop();  // Stop the Timeline if stopOnExit is true

                }
            }
        }

        private void OnTimelineStopped(PlayableDirector director)
        {
            // Deactivate the object after the Timeline stops
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
                Debug.Log($"{objectToDeactivate.name} has been deactivated.");
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from the stopped event to avoid memory leaks
            if (playableDirector != null)
            {
                playableDirector.stopped -= OnTimelineStopped;
            }
        }
    }
}