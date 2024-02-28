using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAccess : MonoBehaviour
{
    [SerializeField] private GameObject[] _accessories;

    void Start()
    {
        InitializeAccessories();
        ActivateAccessory(PlayerPrefs.GetInt("accessorKey"));
    }

    private void InitializeAccessories()
    {
        foreach (GameObject accessory in _accessories)
        {
            accessory.SetActive(false);
        }
    }

    private void ActivateAccessory(int accessorKey)
    {
        if (accessorKey > 0 && accessorKey < _accessories.Length)
        {
            _accessories[accessorKey - 1].SetActive(true);
        }
    }
}
