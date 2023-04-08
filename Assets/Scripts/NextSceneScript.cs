using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneScript : MonoBehaviour
{
    public void LoadNextScene()
    {
        var num = SceneManager.GetActiveScene();
        StartCoroutine(LoadLevel(num.buildIndex + 1));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }

    }
}
