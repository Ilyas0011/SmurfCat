using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using TMPro;

public class PlayerController : MonoBehaviour
{
    static int counterKill;
    static int counterWins;

    [SerializeField] private AudioController audioScript;
    [SerializeField] private AnimationController animScript;
    [SerializeField] private Animator animUI;
    [SerializeField] private Joystick joystick;

    [SerializeField] private TMP_Text snailExpressionText; //����� ��������� � ����� ������
    [SerializeField] private TMP_Text menuSnailText; //����� ������ ���������� ������ � ��������� ����
    [SerializeField] private TMP_Text levelSnailText; //����� ������ ��������� �� ����� ������
    [SerializeField] private TMP_Text mushroomText;

    private Rigidbody rb;

    private float moveInput;
    private float jumpForce = 32f;
    private float speedPlayer = 15f;

    private int levelNumberX; //�� ������� ����� �������� ��������� � ����� ������, ����� ��������� ������, ��� 20 ��� x20, � 0 ������� ��� ������ �� �������
    private int nextLvlId;    
    private int lvlId;

    private bool loseMushroom; //���������� ������
    private bool isLevelComplete = false; //������� ��������
    private bool movePlayer = false;
    private bool isGrounded = true;
    private bool leftWallCollision = false;
    private bool rightWallCollision = false;

    private int _levelMushroom; //����� �� �������
    public int LevelMushroom
    {
        get { return _levelMushroom; }
        private set
        {
            // ����������, ��� �������� �� ����� ���� �������������
            _levelMushroom = Mathf.Max(0, value);
        }
    }

    private int _levelSnail; //������ �� ������� �������
    public int LevelSnail
    {
        get { return _levelSnail; }
        private set
        {
            _levelSnail = Mathf.Max(0, value);
        }
    }

    private int _snailCurrency; //����� ���������� ������
    public int SnailCurrency
    {
        get { return _snailCurrency; }
        private set
        {
            _snailCurrency = Mathf.Max(0, value);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        SnailCurrency = PlayerPrefs.GetInt("SnailCurrency");
        menuSnailText.text = SnailCurrency.ToString();

        lvlId = SceneManager.GetActiveScene().buildIndex;

        nextLvlId = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLvlId > 11)
        {
            nextLvlId = 11;

        }
    }

    private void Final()
    {
        if (levelNumberX != 0)
            Wins();
        else
            Death();
    }



    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);    
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FixedUpdate()
    {

        if (movePlayer == true && isLevelComplete == false)
        {
            if (isGrounded == true)
            {

                if (joystick.Horizontal > 0.5 && rightWallCollision == false) //������������ �������� ������ � 0.5, ����� ��� �������� ������� �� �� �������� � �������
                {
                    RightRun();
                }
                else if (joystick.Horizontal < -0.5 && leftWallCollision == false)
                {
                    LeftRun();
                }else if(joystick.Vertical > 0.8 )
                {
                    Jump();
                }
                else
                {
                    Run();
                }
                 
            }
            else if (isGrounded == false)
            {
                Fall();
            }

            rb.velocity = new Vector3(moveInput * 8, rb.velocity.y, speedPlayer);
        }
    }

    private void Run()
    {
            speedPlayer = 15f;
      

        moveInput = 0;
        audioScript.PlayRunAudio();
        animScript.PlayRunAnimation();
    }
    private void RightRun()
    {
        speedPlayer = 9;
        animScript.PlayRightRunAnimation();
        moveInput = joystick.Horizontal;
    }


    private void LeftRun()
    {
        speedPlayer = 9;
        animScript.PlayLeftRunAnimation();
        moveInput = joystick.Horizontal;
    }

    private void Fall()
    {
        speedPlayer = 11f;
        audioScript.StopRunAudio();
        animScript.PlayFallAnimation();
        if (joystick.Horizontal > 0.5 && rightWallCollision == false)
            moveInput = joystick.Horizontal;
        else if (joystick.Horizontal < -0.5 && leftWallCollision == false)
            moveInput = joystick.Horizontal;
        else
            moveInput = 0;
    }


    public void  StartPlayer()
    {
        movePlayer = true;
    }


    // ����� ��� ����� ������
    private void AddSnail(int amount)
    {
        animUI.Play("AddSnail");
        LevelSnail += amount;
        levelSnailText.text = LevelSnail.ToString();

    }

    private void Wins()
    {
        speedPlayer = 0;
        movePlayer = false;
        isLevelComplete = true;

        animUI.Play("Pop-up window");
        animScript.PlayDance();
        audioScript.StopRunAudio();

        SnailCurrency += LevelSnail * levelNumberX;
        snailExpressionText.text = $"{SnailCurrency}+({LevelSnail * levelNumberX})";
        menuSnailText.text = SnailCurrency.ToString();

        PlayerPrefs.SetInt("SnailCurrency", SnailCurrency);

        if (levelNumberX > PlayerPrefs.GetInt("Record" + lvlId))
            PlayerPrefs.SetInt("Record" + lvlId, levelNumberX);
       
        counterWins++;
        if (counterWins >= 3)
        {
            counterWins = 0;
            AppodealShowInterstitial();
        }

        LevelProgress();
    }

    private void AppodealShowInterstitial()
    {
        // ���� ������� ���������, ���������� �
        if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
        {
            Appodeal.Show(AppodealAdType.Interstitial);
        }
        else
        {
            // ���������� �������
            Appodeal.Cache(AppodealAdType.Interstitial);
        }
    }

    private void LevelProgress() //��������� ����� ���������� ������� ����� � ���� ��������
    {
        if (nextLvlId > PlayerPrefs.GetInt("LevelProgress"))
            PlayerPrefs.SetInt("LevelProgress", nextLvlId);
    }

    private void Death()
    {
        if (isLevelComplete == false)
        {
            speedPlayer = 0;
            movePlayer = false;
            isLevelComplete = true;


            audioScript.PlayDeathSound();
            audioScript.StopRunAudio();
            animScript.PlayDeathAnimation();

            
            counterKill++;
            if (counterKill == 5)
                Appodeal.Cache(AppodealAdType.Interstitial); //�����������

            if (counterKill >= 6) //���� ���� 6 ����������
            {
                counterKill = 0;
                AppodealShowInterstitial();
            }

            StartCoroutine(Restart());
        }
    }

    private void Jump()
    {
        if (isLevelComplete == false && Mathf.Abs(rb.velocity.y) < 0.01f) // ���������, ��� �������� �� ������������ ��� ������ � ���� (����� �� � �������� ������ ��� ��� � �������)
        {
            audioScript.PlayJumpAudio();
            animScript.PlayJumpAnimation();

            // ��������� ���� ����� � Rigidbody ��� ������
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isLevelComplete)
        {
            HandleColliderEnter(other.gameObject.tag);
        }
    }

    private void HandleColliderEnter(string tag)
    {
        switch (tag)
        {
            case "Trap":
                Death();
                break;
            case "Ground":
                isGrounded = true;
                break;
            case "BandaRightGround":
                rightWallCollision = true;
                break;
            case "BandaLeftGround":
                leftWallCollision = true;
                break;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (!isLevelComplete)
        {
            HandleCollisionExit(other.gameObject.tag);
        }
    }

    private void HandleCollisionExit(string tag)
    {
        switch (tag)
        {
            case "Ground":
                isGrounded = false;
                break;
            case "BandaRightGround":
                rightWallCollision = false;
                break;
            case "BandaLeftGround":
                leftWallCollision = false;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isLevelComplete)
        {
            HandleTriggerEnter(other.gameObject.tag, other.gameObject);
        }
    }

    

    private void HandleTriggerEnter(string tag, GameObject other)
    {
        switch (tag)
        {
            case "Mushroom":
                audioScript.PlayPickingAudio();
                AddMushroom(1);
                Destroy(other.gameObject);
                break;
            case "Snail":
                audioScript.PlayPickingAudio();
                AddSnail(1);
                Destroy(other.gameObject);
                break;
            case "Trap2":
                loseMushroom = true;
                StartCoroutine(LoseMushroom(4));
                levelNumberX = 0;
                break;
            case "Finish":
                loseMushroom = true;
                StartCoroutine(LoseMushroom(1));
                break;
            case "MultiplyBonus":
                audioScript.PlayPickingAudio();
                LevelMushroom *= other.GetComponent<MultiplyMushroom>().numberMultiple; //�������� ���� ������ �� ����� ������
                MushroomUpdate();
                break;
            case "DivisionBonus":
                audioScript.PlayPickingAudio();
                LevelMushroom /= other.GetComponent<DivisionMushroom>().numberDivision;
                MushroomUpdate();
                break;
            case "StartReport":
                StartCoroutine(LoseMushroom(1));
                break;
            case "EndLevel":
                levelNumberX = other.GetComponent<EndLevel>().endLevelNumberX; //������ �� ����� ������ ��������� ����� �� ������ ������ ��������
                break;
            default:
                break;
        }
    }

    // ����� ��� ����� ������
    private void AddMushroom(int amount)
    {
        animUI.Play("AddMushroom");
        LevelMushroom += amount;
        MushroomUpdate();
    }

    private void MushroomUpdate()
    {
        mushroomText.text = LevelMushroom.ToString();
    }

    IEnumerator LoseMushroom(int numMushroomToSubtract)
    {
        loseMushroom = true;
        Appodeal.Cache(AppodealAdType.RewardedVideo);
        Appodeal.Cache(AppodealAdType.Interstitial);

        if (numMushroomToSubtract == 1)
        {
            audioScript.StopForestAudio();
            audioScript.PlayMusic();
        }

        while (loseMushroom)
        {
            LevelMushroom -= numMushroomToSubtract;
            if (LevelMushroom <= 0)
            {
                LevelMushroom = 0;
                loseMushroom = false;
                Final();
            }
            MushroomUpdate();
            yield return new WaitForSeconds(0.067f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trap2"))
            loseMushroom = false;
    }
}
