using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinCharacter : MonoBehaviour
{
    [SerializeField] private GameObject priceObject;
    [SerializeField] private GameObject useObject;
    [SerializeField] private GameObject usedObject;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI snailCurrency;

    [SerializeField] private Material materialToChange; //�������� �� ������� ����� �������� ��������
    [SerializeField] private Texture applyTexture; //������������� ��������

    [SerializeField] private int idSkin; 
    [SerializeField] private int priceSkin; 

    private bool isAcquired = false;

    public void SnailUpadate()
    {
        snailCurrency.text = PlayerPrefs.GetInt("SnailCurrency").ToString();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("skinKey") == idSkin)
            materialToChange.mainTexture = applyTexture;
    }

    void FixedUpdate()
    {
        StatusCheck();
    }

    private void UsedSkin() //����������� ����
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(true);
    }

    private void UnusedSkin() //�� �������������� ����
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(true);
        usedObject.gameObject.SetActive(false);
    }

    private void UnpurchasedSkin() //�� ��������� ����
    {
        priceObject.gameObject.SetActive(true);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(false);
        priceText.text = priceSkin.ToString();
    }

    private void StatusCheck()
    {
        isAcquired = PlayerPrefs.GetInt("acquiredKey" + idSkin) == 1 ? true : false;
        if (isAcquired == true || PlayerPrefs.GetInt("skinKey") == idSkin) //���� ������
        {
            if (idSkin == PlayerPrefs.GetInt("skinKey"))
                UsedSkin();
            else if (PlayerPrefs.GetInt("skinKey") != idSkin)
                UnusedSkin();
        }
        else if(isAcquired == false) //���� �� ������
        {
            UnpurchasedSkin();
        }
    }

    public void ClickBuy()
    {
        if (isAcquired == false) //���� �� ������
        {
            if (priceSkin <= PlayerPrefs.GetInt("SnailCurrency")) //���� ����� �������, �� ������������ �������
            {
                PlayerPrefs.SetInt("skinKey", idSkin);
                PlayerPrefs.SetInt("SnailCurrency", PlayerPrefs.GetInt("SnailCurrency") - priceSkin);
                PlayerPrefs.SetInt("acquiredKey" + idSkin, 1);
                materialToChange.mainTexture = applyTexture;
                SnailUpadate();
                UsedSkin();
            }
        }
        else if (isAcquired == true) //���� ������
        {
            if (PlayerPrefs.GetInt("skinKey") != idSkin)
            {
                PlayerPrefs.SetInt("skinKey", idSkin);
                materialToChange.mainTexture = applyTexture;
                UsedSkin();
            }
        }
    }
}
