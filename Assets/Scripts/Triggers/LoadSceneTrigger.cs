using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public class LoadSceneTrigger : TriggerBehaviour
    {
        [System.Serializable]
        public class TransferredObjectData
        {
            public GameObject objectToTransfer;
            public Vector3 positionInNewScene;
            public Quaternion rotationInNewScene;
            public bool maintainCurrentTransform = false;
            
        }

        [Header("Scene Loading Settings")]
        public string sceneToLoad;
        public string objectNameToDisableCollider;

        [Header("Inventory Check Settings")]
        public string requiredBadgeName;
        public bool requiresBadgeCheck = false;

        [Header("Object Transfer Settings")]
        public List<TransferredObjectData> objectsToTransfer = new List<TransferredObjectData>();
        public bool isPersistentTransfer = true; // If true, objects will be available for next scene transfer

        private static Dictionary<string, List<GameObject>> persistentObjects = new Dictionary<string, List<GameObject>>();
        private ItemManager itemManager;

        [SerializeField] private InputActionProperty triggerAction;

        private void Start()
        {
            itemManager = FindObjectOfType<ItemManager>();
            if (itemManager == null && requiresBadgeCheck)
            {
                Debug.LogError("ItemManager not found but badge check is required!");
            }

            // If this scene has received any persistent objects, position them correctly
            string currentScene = SceneManager.GetActiveScene().name;
            if (persistentObjects.ContainsKey(currentScene))
            {
                PositionPersistentObjectsInScene();
            }
            triggerAction.action.Enable();
        }

        private void PositionPersistentObjectsInScene()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (!persistentObjects.ContainsKey(currentScene)) return;

            foreach (GameObject obj in persistentObjects[currentScene])
            {
                // Find matching transfer data for this object
                TransferredObjectData transferData = objectsToTransfer.Find(data => data.objectToTransfer.name == obj.name);
                if (transferData != null && !transferData.maintainCurrentTransform)
                {
                    obj.transform.position = transferData.positionInNewScene;
                    obj.transform.rotation = transferData.rotationInNewScene;
                }
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            if (requiresBadgeCheck && !CheckForBadge() && triggerAction.action.WasPressedThisFrame())
            {
                Debug.Log($"Player needs the {requiredBadgeName} badge to proceed!");
                return;
            }

            LoadScene(sceneToLoad, objectNameToDisableCollider);
        }

        private bool CheckForBadge()
        {
            if (itemManager == null) return false;

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
            // Prepare objects for transfer
            PrepareObjectsForTransfer(sceneName);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Handle collider disabling in new scene
            StartCoroutine(FindAndDisableCollider(objectNameToDisableCollider));
        }

        private void PrepareObjectsForTransfer(string targetScene)
        {
            List<GameObject> objectsForScene = new List<GameObject>();

            foreach (var transferData in objectsToTransfer)
            {
                if (transferData.objectToTransfer != null)
                {
                    DontDestroyOnLoad(transferData.objectToTransfer);
                    objectsForScene.Add(transferData.objectToTransfer);
                }
            }

            if (isPersistentTransfer)
            {
                // Store objects for potential future transfers
                persistentObjects[targetScene] = objectsForScene;
            }
        }

        private IEnumerator FindAndDisableCollider(string objectName)
        {
            if (string.IsNullOrEmpty(objectName)) yield break;

            float timeoutDuration = 2f;
            float elapsedTime = 0f;
            GameObject targetObject = null;

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
            }
            else
            {
                Debug.LogWarning($"Object {objectName} not found in new scene after {timeoutDuration} seconds.");
            }
        }

        // Method to cleanup persistent objects if needed
        public static void ClearPersistentObjects()
        {
            persistentObjects.Clear();
        }
    }
}
