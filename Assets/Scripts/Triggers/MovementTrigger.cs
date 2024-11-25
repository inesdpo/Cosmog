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

        private bool isPlayerInTrigger = false;

        private bool CanPlayAnimator = false;

        protected override void OnEnterAction(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInTrigger = true;
            }
        }

        protected override void OnExitAction(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInTrigger = false;
            }
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            { 
                CanPlayAnimator = true;
            }

            if (isPlayerInTrigger && CanPlayAnimator)
            {
                Debug.Log("It's Working");
                PlayTimeline();
            }
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

        public void OnActivated()
        {
            CanPlayAnimator = true;
        }
    }
}