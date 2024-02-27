using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkyCharact : MonoBehaviour
{
    [SerializeField] private Material materialSkyBox;

    [SerializeField] private GameObject priceObject;
    [SerializeField] private GameObject useObject;
    [SerializeField] private GameObject usedObject;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI snailCurrency;

    [SerializeField] private int idSky;
    [SerializeField] private int priceSky;

    private bool isAcquired = false; 

    public void SnailUpadate()
    {
        snailCurrency.text = PlayerPrefs.GetInt("SnailCurrency").ToString();
    }

    private void FixedUpdate()
    {
        StatusCheck();
    }

    private void ApplySkyBox()
    {
        RenderSettings.skybox = materialSkyBox;
        DynamicGI.UpdateEnvironment();
    }

    private void UsedSky()
    {
        ApplySkyBox();
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(true);
    }

    private void UnusedSky() 
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(true);
        usedObject.gameObject.SetActive(false);
    }

    private void UnpurchasedSky() 
    {
        priceObject.gameObject.SetActive(true);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(false);
        priceText.text = priceSky.ToString(); //����� �� ����� �������������� � ����
    }

    private void StatusCheck()
    {
        isAcquired = PlayerPrefs.GetInt("acquiredSky" + idSky) == 1 ? true : false; //������ ������� ���� ��� ���

        if (isAcquired == true) //����  �������
        {
            if (idSky == PlayerPrefs.GetInt("skyKey")) 
                UsedSky();
            else if (PlayerPrefs.GetInt("skyKey") != idSky)
                UnusedSky();
        }
        else if (isAcquired == false) //���� �� �������
        {
            UnpurchasedSky();
        }
    }

    public void ClickBuy()
    {
        if (isAcquired == false) //���� �� �������
        {
            if (priceSky <= PlayerPrefs.GetInt("SnailCurrency")) //���� ����� �������, �� ������������ �������
            {
                PlayerPrefs.SetInt("skyKey", idSky);
                PlayerPrefs.SetInt("SnailCurrency", PlayerPrefs.GetInt("SnailCurrency") - priceSky);
                PlayerPrefs.SetInt("acquiredSky" + idSky, 1);
                SnailUpadate();
                UsedSky();
            }
        }
        else if (isAcquired == true) //���� �������
        {

            if (PlayerPrefs.GetInt("skyKey") != idSky)
            {
                PlayerPrefs.SetInt("skyKey", idSky);
                RenderSettings.skybox.SetFloat("_Exposure", 1.0f);
                SnailUpadate();
                UsedSky();
            }
        }
    }
}
