using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Triggers
{ /// <summary>
/// This script moves canvas with the first person controller
/// </summary>
    public class BillboardTrigger1 : TriggerBehaviour
    {
        [Header("References")]
        [SerializeField] private Canvas billboardCanvas;
        [SerializeField] private Transform playerCamera;

        [Header("Settings")]
        [SerializeField] private bool rotateVertically = false;
        [SerializeField] private float showDistance = 5f;
        [SerializeField] private bool startHidden = true;

        private bool isPlayerInTrigger = false;
        private Transform canvasTransform;

        private void Start()
        {
            // Ensure we have references
            if (billboardCanvas == null)
            {
                Debug.LogError("Billboard Canvas not assigned to BillboardTrigger script!");
                enabled = false;
                return;
            }

            // Get the main camera if player camera not assigned
            if (playerCamera == null)
            {
                playerCamera = Camera.main.transform;
                Debug.Log("Using Main Camera as player camera reference");
            }

            canvasTransform = billboardCanvas.transform;

            // Set initial visibility
            billboardCanvas.enabled = !startHidden;
        }

        private void Update()
        {
            if (!isPlayerInTrigger) return;

            // Check if player is within show distance
            float distanceToPlayer = Vector3.Distance(transform.position, playerCamera.position);
            billboardCanvas.enabled = distanceToPlayer <= showDistance;

            if (billboardCanvas.enabled)
            {
                // Calculate direction to player
                Vector3 directionToPlayer = playerCamera.position - canvasTransform.position;

                if (!rotateVertically)
                {
                    // Zero out the y component to prevent vertical rotation
                    directionToPlayer.y = 0;
                }

                // Make the canvas face the player
                canvasTransform.rotation = Quaternion.LookRotation(-directionToPlayer);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            // Check if it's the player entering the trigger
            if (other.CompareTag("Player"))
            {
                isPlayerInTrigger = true;
               
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            // Check if it's the player exiting the trigger
            if (other.CompareTag("Player"))
            {
                isPlayerInTrigger = false;
                
            }
        }

        // Optional: Visualize the trigger area in the editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, showDistance);
        }

    }
}

