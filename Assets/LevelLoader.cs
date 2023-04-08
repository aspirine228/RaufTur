using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.RemoteConfig;
using System;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public GameObject loadingScreen;
    public Text progressValue;

    void Start()
    {
        StartCoroutine(LoadAsync());
    }


    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressValue.text = progress * 100f + "%";

            yield return null;
        }
    }
}
