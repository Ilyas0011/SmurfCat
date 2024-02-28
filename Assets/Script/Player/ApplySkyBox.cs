using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkyBox : MonoBehaviour
{

    [SerializeField] private Material[] skyMaterials;

    void Start()
    {
        SetSkyboxMaterial(PlayerPrefs.GetInt("skyKey"));
    }

    private void SetSkyboxMaterial(int skyKey)
    {
        if (skyKey >= 0 && skyKey < skyMaterials.Length)
        {
            RenderSettings.skybox = skyMaterials[skyKey];
            DynamicGI.UpdateEnvironment();
        }
    }
}
