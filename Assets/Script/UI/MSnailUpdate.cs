using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MSnailUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI snailCurrency;

    void Start()
    {
        snailCurrency.text = PlayerPrefs.GetInt("SnailCurrency").ToString();
    }
}
