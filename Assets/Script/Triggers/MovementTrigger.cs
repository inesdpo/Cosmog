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
            animatorTarget = animator.GetComponent<PlayableDirector>();
        }

        protected override void OnEnterAction(Collider other)
        {
            if (animatorTarget != null)
            {
                Debug.Log("Playing Timeline sequence!");
                animatorTarget.Play();  // Play the Timeline
            }
        }
    }
}