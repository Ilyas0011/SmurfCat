using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private string[] danceAnimations = { "Dance", "Dance1", "Dance2", "Dance3", "Dance4", "Dance5" };

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayRunAnimation()
    {
        anim.SetBool("Run", true);
        anim.SetBool("Fall", false);
        anim.SetBool("RightRun", false);
        anim.SetBool("LeftRun", false);
        anim.SetBool("Jump", false);
    }

    public void PlayRightRunAnimation()
    {
        anim.SetBool("Fall", false);
        anim.SetBool("Run", false);
        anim.SetBool("Jump", false);
        anim.SetBool("RightRun", true);
        anim.SetBool("LeftRun", false);
    }

    public void PlayLeftRunAnimation()
    {
        anim.SetBool("Fall", false);
        anim.SetBool("Run", false);
        anim.SetBool("Jump", false);
        anim.SetBool("RightRun", false);
        anim.SetBool("LeftRun", true);
    }

    public void PlayFallAnimation()
    {
        anim.SetBool("Fall", true);
        anim.SetBool("Run", false);
        anim.SetBool("Jump", false);
        anim.SetBool("RightRun", false);
        anim.SetBool("LeftRun", false);
    }

    public void PlayDeathAnimation()
    {
        anim.Play("Death");
    }

    public void PlayJumpAnimation()
    {
        anim.SetBool("Fall", false);
        anim.SetBool("Run", false);
        anim.SetBool("Jump", true);
        anim.SetBool("RightRun", false);
        anim.SetBool("LeftRun", false);
    }


    public void PlayDance()
    {
        int danceKey = PlayerPrefs.GetInt("danceKey");

        if (danceKey >= 0 && danceKey < danceAnimations.Length)
        {
            anim.Play(danceAnimations[danceKey]);
        }
    }

}
