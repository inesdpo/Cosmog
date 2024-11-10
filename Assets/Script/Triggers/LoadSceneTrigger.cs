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

public class LoadSceneTrigger : TriggerBehaviour
    {
        public string sceneToLoad;
        
        public string objectNameToDisableCollider;
        protected void LoadScene(string sceneName, string objectNameToDisableCollider)
        {
            StartCoroutine(LoadSceneAsync(sceneName, objectNameToDisableCollider));
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Call the LoadScene method from the base class
                LoadScene(sceneToLoad, objectNameToDisableCollider);
            }

        }

        private IEnumerator LoadSceneAsync(string sceneName, string objectNameToDisableCollider)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => asyncLoad.isDone);

            // Repeatedly attempt to find the object and remove its collider
            GameObject targetObject = null;
            for (int i = 0; i < 10; i++)
            {
                targetObject = GameObject.Find(objectNameToDisableCollider);
                if (targetObject != null) break;

                yield return new WaitForSeconds(0.1f); // Wait before trying again
            }


            if (targetObject != null)
            {
                    Collider collider = targetObject.GetComponent<Collider>();
                    if (collider != null)
                    {
                        Destroy(collider); // Remove the collider
                        Debug.Log("Collider removed from " + objectNameToDisableCollider);
                    }
                    else
                    {
                        Debug.LogWarning("Collider not found on " + objectNameToDisableCollider);
                    }
            }
            else
            {
                Debug.LogWarning("Object with name " + objectNameToDisableCollider + " not found in the new scene.");
            }
           
        }
    }
}
