using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DanceCharac : MonoBehaviour
{
    [SerializeField] private GameObject priceObject;  
    [SerializeField] private GameObject useObject;
    [SerializeField] private GameObject usedObject;

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI snailCurrency;

    [SerializeField] private Animator anim;

    [SerializeField] private int idDance; 
    [SerializeField] private int priceDance;

    private bool isAcquired = false; //Куплен Танец или нет

    public void SnailUpadate()
    {
        snailCurrency.text = PlayerPrefs.GetInt("SnailCurrency").ToString();
    }

    private void OnEnable()
    {
        AnimaPlayer();
    }

    private void FixedUpdate()
    {
        StatusCheck();
    }

    private void UsedDance() //Примененный танец
    {
        AnimaPlayer();
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(true);
    }

    private void UnusedDance() //Не использованный танец
    {
        priceObject.gameObject.SetActive(false);
        useObject.gameObject.SetActive(true);
        usedObject.gameObject.SetActive(false);
    }

    private void UnpurchasedDance() //Не купленный танец
    {
        priceObject.gameObject.SetActive(true);
        useObject.gameObject.SetActive(false);
        usedObject.gameObject.SetActive(false);
        priceText.text = priceDance.ToString(); //Текст на сцене приравнивается к цене
    }

    private void StatusCheck()
    {
        isAcquired = PlayerPrefs.GetInt("acquiredDance" + idDance) == 1 ? true : false;
        if (isAcquired == true) //Танец куплен
        {
            if (idDance == PlayerPrefs.GetInt("danceKey"))
            {
                UsedDance();
            }
            else if (PlayerPrefs.GetInt("danceKey") != idDance)
                UnusedDance();

        }
        else if(isAcquired == false) //Танец не куплен
        {
            UnpurchasedDance();

        }
    }


    private void AnimaPlayer()
    {
         anim.Play("Dance" + idDance);
    }

    public void ClickBuy()
    {
        if (isAcquired == false) //Танец не куплен
        {
            if (priceDance <= PlayerPrefs.GetInt("SnailCurrency")) //Если улиток хватает, то производится покупка
            {
                PlayerPrefs.SetInt("acquiredDance" + idDance, 1);
                PlayerPrefs.SetInt("danceKey", idDance);
                UsedDance();
                PlayerPrefs.SetInt("SnailCurrency", PlayerPrefs.GetInt("SnailCurrency") - priceDance);
                SnailUpadate();

            }
        }
        else if (isAcquired == true) //Танец куплен
        {

            if (PlayerPrefs.GetInt("danceKey") != idDance)
            {
                PlayerPrefs.SetInt("danceKey", idDance);
                UsedDance();
            }

        }

    }

}
