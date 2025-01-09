using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace Script.Triggers
{
        public class MovementTrigger : TriggerBehaviour
        {
            [Header("Animation Settings")]
            [SerializeField] private GameObject animator;
            [SerializeField] private bool playOnlyOnce = false; // Option to play animation only once
            [SerializeField] private float cooldownTime = 0.5f; // Prevent spam-clicking

            private PlayableDirector animatorTarget;
            private bool isPlayerInTrigger = false;
            private bool canPlayAnimator = false;
            private bool hasPlayedOnce = false;
            private float lastPlayTime = 0f;
            [SerializeField] private InputActionProperty triggerAction;


            private void Start()
            {
                if (animator)
                {
                    animatorTarget = animator.GetComponent<PlayableDirector>();
                }
                triggerAction.action.Enable();
            }

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
                    canPlayAnimator = false; // Reset when player exits
                }
            }

            private void Update()
            {
                if (!isPlayerInTrigger) return; // Only check input if player is in trigger

                if (triggerAction.action.WasPressedThisFrame())
                {
                    // Check if we can play the animation
                    if (CanPlayAnimation())
                    {
                        PlayTimeline();
                        lastPlayTime = Time.time;
                        hasPlayedOnce = true;
                    }
                }
            }

            private bool CanPlayAnimation()
            {
                // Check cooldown
                if (Time.time - lastPlayTime < cooldownTime)
                {
                    return false;
                }

                // Check if animation should only play once
                if (playOnlyOnce && hasPlayedOnce)
                {
                    return false;
                }

                // Check if animation is currently playing
                if (animatorTarget != null && animatorTarget.state == PlayState.Playing)
                {
                    return false;
                }

                return true;
            }

            private void PlayTimeline()
            {
                if (animatorTarget != null)
                {
                    Debug.Log("Playing Timeline sequence!");

                    // If the animation is already playing, stop it first
                    if (animatorTarget.state == PlayState.Playing)
                    {
                        animatorTarget.Stop();
                    }

                    // Play from start
                    animatorTarget.time = 0;
                    animatorTarget.Play();
                }
                else
                {
                    Debug.LogWarning("No PlayableDirector component found on animator object!");
                }
            }

            public void OnActivated()
            {
                canPlayAnimator = true;
            }

            // Method to reset the animation state if needed
            public void ResetAnimation()
            {
                hasPlayedOnce = false;
                lastPlayTime = 0f;
            }

            // Optional: Method to check if animation can be played (for external scripts)
            public bool IsReadyToPlay()
            {
                return isPlayerInTrigger && CanPlayAnimation();
            }
        }
    
}