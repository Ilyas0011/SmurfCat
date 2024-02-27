using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestMusic : MonoBehaviour
{
    public GameObject[] musicObjs;

    private void Start()
    {
        musicObjs = GameObject.FindGameObjectsWithTag("Music");


        if (musicObjs.Length > 1)
        {
            Destroy(musicObjs[1].gameObject);
        }


    }

}
