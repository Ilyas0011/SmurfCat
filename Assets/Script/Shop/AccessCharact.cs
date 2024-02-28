using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessCharact : MonoBehaviour
{

    [SerializeField] private GameObject accessorObject; //������ ���������� �� ���������

    [SerializeField] private GameObject priceObject;  //������ ����
    [SerializeField] private GameObject useObject; //������ ������
    [SerializeField] private GameObject usedObject; //������ ������������

    [SerializeField] private TextMeshProUGUI priceText; //����� ���� �� �����
    [SerializeField] private TextMeshProUGUI snailText;

    [SerializeField] private int idAccessor; // ����� �������
    [SerializeField] private int priceNumber; // ���� ������� � �������

    private bool acquired = false; //������ ������ ��� ���

    public void SnailUpadate()
    {
        snailText.text = PlayerPrefs.GetInt("SnailCurrency").ToString();
    }

    void FixedUpdate()
    {
        StatusCheck();
    }

    private void OneAccessor()
    {
        accessorObject.SetActive(true);
    }

    void OffAccessor()
    {
        accessorObject.SetActive(false);
    }

    private void UsedAccessory() //����������� ���
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(true);
        OneAccessor();
    }

    private void UnusedAccessory() //�� �������������� ���
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(true);
        usedObject.gameObject.SetActive(false);
        OffAccessor();
    }

    private void UnpurchasedAccessory() //�� ��������� ���
    {
        priceObject.gameObject.SetActive(true);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(false);
        priceText.text = priceNumber.ToString();
        OffAccessor();
    }

    private void StatusCheck()
    {
        acquired = PlayerPrefs.GetInt("acquiredAccessor" + idAccessor) == 1 ? true : false; //������ ������ ��������� ��� ���
        if (acquired == true || idAccessor == PlayerPrefs.GetInt("accessorKey")) //��� ������
        {
            if (idAccessor == PlayerPrefs.GetInt("accessorKey"))
                UsedAccessory();
            else if (PlayerPrefs.GetInt("accessorKey") != idAccessor)
                UnusedAccessory();
        }
        else if (acquired == false) //��� �� ������
        {
            UnpurchasedAccessory();
        }
    }


    public void ClickBuy()
    {
        if (acquired == false) //��� �� ������
        {
            if (priceNumber <= PlayerPrefs.GetInt("SnailCurrency")) //���� ����� �������, �� ������������ �������
            {
                PlayerPrefs.SetInt("accessorKey", idAccessor);
                PlayerPrefs.SetInt("SnailCurrency", PlayerPrefs.GetInt("SnailCurrency") - priceNumber);
                PlayerPrefs.SetInt("acquiredAccessor" + idAccessor, 1);
                
                SnailUpadate();
                UsedAccessory();
            }
        }
        else if (acquired == true) //��� ������
        {

            if (PlayerPrefs.GetInt("accessorKey") != idAccessor)
            {
                PlayerPrefs.SetInt("accessorKey", idAccessor);
                OneAccessor();
                UsedAccessory();

            }

        }

    }
}
