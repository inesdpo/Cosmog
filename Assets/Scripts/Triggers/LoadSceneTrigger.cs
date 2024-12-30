using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Triggers
{ /// <summary>
  /// This script will load scene when player collides with a another collider object with script
  /// 
  /// while (!asyncLoad.isDone)
  ///        {
  ///Debug.Log("Scene Loaded");
  ///          yield return null; // Wait until the scene is fully loaded
  /// </summary>

    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;
    using System.Collections.Generic;
    using Script.Triggers;

    public class LoadSceneTrigger : TriggerBehaviour
    {
        [Header("Scene Loading Settings")]
        public string sceneToLoad;
        public string objectNameToDisableCollider;

        [Header("Inventory Check Settings")]
        public string requiredBadgeName;
        public bool requiresBadgeCheck = false;

        [Header("Object Transfer Settings")]
        public List<GameObject> objectsToTransfer;
        private ItemManager itemManager;

        private void Start()
        {
            // Get reference to ItemManager
            itemManager = FindObjectOfType<ItemManager>();
            if (itemManager == null && requiresBadgeCheck)
            {
                Debug.LogError("ItemManager not found but badge check is required!");
            }

            // Mark objects to persist between scenes
            foreach (var obj in objectsToTransfer)
            {
                if (obj != null)
                {
                    DontDestroyOnLoad(obj);
                }
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            if (requiresBadgeCheck)
            {
                bool hasBadge = CheckForBadge();
                if (!hasBadge)
                {
                    Debug.Log($"Player needs the {requiredBadgeName} badge to proceed!");
                    return;
                }
            }

            LoadScene(sceneToLoad, objectNameToDisableCollider);
        }

        private bool CheckForBadge()
        {
            if (itemManager == null) return false;

            // Check inventory items for the required badge
            foreach (var inventoryItem in itemManager.GetInventoryItems())
            {
                if (inventoryItem.GetItemName() == requiredBadgeName && inventoryItem.gameObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }

        protected void LoadScene(string sceneName, string objectNameToDisableCollider)
        {
            StartCoroutine(LoadSceneAsync(sceneName, objectNameToDisableCollider));
        }

        private IEnumerator LoadSceneAsync(string sceneName, string objectNameToDisableCollider)
        {
            // Store positions of objects to transfer before loading new scene
            Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
            foreach (var obj in objectsToTransfer)
            {
                if (obj != null)
                {
                    originalPositions[obj] = obj.transform.position;
                }
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = true;

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Restore transferred objects to their original positions in new scene
            foreach (var kvp in originalPositions)
            {
                if (kvp.Key != null)
                {
                    kvp.Key.transform.position = kvp.Value;
                }
            }

            // Find and disable collider in new scene
            StartCoroutine(FindAndDisableCollider(objectNameToDisableCollider));
        }

        private IEnumerator FindAndDisableCollider(string objectName)
        {
            if (string.IsNullOrEmpty(objectName)) yield break;

            GameObject targetObject = null;
            float timeoutDuration = 2f;
            float elapsedTime = 0f;

            while (targetObject == null && elapsedTime < timeoutDuration)
            {
                targetObject = GameObject.Find(objectName);
                if (targetObject == null)
                {
                    elapsedTime += 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
            }

            if (targetObject != null)
            {
                Collider collider = targetObject.GetComponent<Collider>();
                if (collider != null)
                {
                    Destroy(collider);
                    Debug.Log($"Collider removed from {objectName}");
                }
                else
                {
                    Debug.LogWarning($"Collider not found on {objectName}");
                }
            }
            else
            {
                Debug.LogWarning($"Object {objectName} not found in the new scene after {timeoutDuration} seconds.");
            }
        }
    }
}
