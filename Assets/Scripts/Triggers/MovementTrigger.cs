using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Script.Triggers
{
    public class MovementTrigger : TriggerBehaviour
    {

        [SerializeField] public GameObject animator;

        private PlayableDirector animatorTarget;

        // Start is called before the first frame update
        void Start()
        {
            if(animator)
            {
                animatorTarget = animator.GetComponent<PlayableDirector>();
            }
        }

        protected override void OnEnterAction(Collider other)
        {
            PlayTimeline();
        }
        
        
        // Called when the mouse button is released over the object
        void OnMouseDown()
        {
            Debug.Log("MouseDownWorking");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            PlayTimeline();
            

        }

        // Separate method to play the timeline
        private void PlayTimeline()
        {
            if (animatorTarget != null)
            {
                Debug.Log("Playing Timeline sequence!");
                animatorTarget.Play();  // Play the Timeline
            }
        }
    }
}