using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessCharact : MonoBehaviour
{

    [SerializeField] private GameObject accessorObject; //Объект аксессуара на персонаже

    [SerializeField] private GameObject priceObject;  //Объект цены
    [SerializeField] private GameObject useObject; //Объект куплен
    [SerializeField] private GameObject usedObject; //Объект используется

    [SerializeField] private TextMeshProUGUI priceText; //Текст цены на сцене
    [SerializeField] private TextMeshProUGUI snailText;

    [SerializeField] private int idAccessor; // Номер объекта
    [SerializeField] private int priceNumber; // Цена объекта в улитках

    private bool acquired = false; //Куплен объект или нет

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

    private void UsedAccessory() //Примененный акс
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(true);
        OneAccessor();
    }

    private void UnusedAccessory() //Не использованный акс
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(true);
        usedObject.gameObject.SetActive(false);
        OffAccessor();
    }

    private void UnpurchasedAccessory() //Не купленный акс
    {
        priceObject.gameObject.SetActive(true);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(false);
        priceText.text = priceNumber.ToString();
        OffAccessor();
    }

    private void StatusCheck()
    {
        acquired = PlayerPrefs.GetInt("acquiredAccessor" + idAccessor) == 1 ? true : false; //Узнаем Куплен аксессуар или нет
        if (acquired == true || idAccessor == PlayerPrefs.GetInt("accessorKey")) //Акс куплен
        {
            if (idAccessor == PlayerPrefs.GetInt("accessorKey"))
                UsedAccessory();
            else if (PlayerPrefs.GetInt("accessorKey") != idAccessor)
                UnusedAccessory();
        }
        else if (acquired == false) //Акс не куплен
        {
            UnpurchasedAccessory();
        }
    }


    public void ClickBuy()
    {
        if (acquired == false) //Акс не куплен
        {
            if (priceNumber <= PlayerPrefs.GetInt("SnailCurrency")) //Если денег хватает, то производится покупка
            {
                PlayerPrefs.SetInt("accessorKey", idAccessor);
                PlayerPrefs.SetInt("SnailCurrency", PlayerPrefs.GetInt("SnailCurrency") - priceNumber);
                PlayerPrefs.SetInt("acquiredAccessor" + idAccessor, 1);
                
                SnailUpadate();
                UsedAccessory();
            }
        }
        else if (acquired == true) //акс куплен
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
