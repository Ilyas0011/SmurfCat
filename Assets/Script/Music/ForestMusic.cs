using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ForestMusic : MonoBehaviour
{
    private AudioSource ForestMusics => GetComponent<AudioSource>();
    int sceneIndex;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0 || sceneIndex == 12)
            ForestMusics.Stop();
        else
            ForestMusics.Play();
    }

}
