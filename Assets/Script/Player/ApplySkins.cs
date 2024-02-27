using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySkins : MonoBehaviour
{
    [SerializeField] Material materialToChange; //ћатериал на который нужно наложить текстуру
    [SerializeField] private Texture[] smurfTexture = new Texture[6];


    private void Start()
    {
        SetSmurfTexture(PlayerPrefs.GetInt("skinKey"));

    }

    private void SetSmurfTexture(int skinKey)
    {
        if(skinKey >= 0 && skinKey < smurfTexture.Length)
        {
            materialToChange.mainTexture = smurfTexture[skinKey];
        }
    }
}
