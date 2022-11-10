using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    Animator fadeAnim;

    public bool isFaded;
    private void Start()
    {
        fadeAnim = this.gameObject.GetComponent<Animator>();
        fadeAnim.enabled = false;
    }

    public void HandleFade()
    {
        if(!isFaded)
        {
            fadeAnim.enabled = false;
            fadeAnim.enabled = true;
            fadeAnim.Play("Fade");
        }

        else
        {
            fadeAnim.enabled = false;
            fadeAnim.enabled = true;
            fadeAnim.Play("FadeOut");
        }
    }
}
