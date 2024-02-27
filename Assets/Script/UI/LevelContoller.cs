using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelContoller : MonoBehaviour
{

    [SerializeField] private int lvlId;//Ќомер ”ровн€
    [SerializeField] private TextMeshProUGUI recordText; //“екст рекорда
    [SerializeField] private GameObject objectRecord;
    [SerializeField] private Button button;


    private int record; // акое максимальное умножение мы смогли на этом уровне достигнуть
    private int maxProgressLevel;//”ровень на котором остановились

    public int MaxProgressLevel
    {
        get { return maxProgressLevel; }
        set { maxProgressLevel = Math.Max(1, value); }//Ќельз€ присвоить значение меньше 1
    }

    private void Start()
    {
        button = GetComponent<Button>();
        record = PlayerPrefs.GetInt("Record" + lvlId);// акое максимальное умножение мы смогли на этом уровне достигнуть
        LevelInactiveCheck();
        CheckRecord();
    }

    private void CheckRecord()
    {
        if (record <= 0)
        {
            objectRecord.SetActive(false);
        }
        else if (record > 12)
        {
            objectRecord.SetActive(true);
            recordText.text = "12";
        }
        else if (record > 0)
        {
            objectRecord.SetActive(true);
            recordText.text = ($"x{record.ToString()}");
        }
    }

    private void LevelInactiveCheck() //ѕровер€ем дошли ли мы до этого уровн€
    {
        MaxProgressLevel = PlayerPrefs.GetInt("LevelProgress");// +1 добавл€ем чтобы на один уровень было больше открыто

        if (lvlId <= MaxProgressLevel && button != null)
                button.interactable = true;
        else if (button != null)
             button.interactable = false;

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(lvlId);
    }
}
