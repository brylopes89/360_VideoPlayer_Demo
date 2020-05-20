using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager animManager;

    public Animator videoMenuAnimator;
    public Animator startMenuAnimator;
    public Animator spriteAnimator;

    private void Awake()
    {
        if (animManager == null)
            animManager = this;
    }

    public void OpenVideoMenu()
    {
        bool isOpen = videoMenuAnimator.GetBool("OpenMenu");

        videoMenuAnimator.SetBool("OpenMenu", !isOpen);
    }

    public IEnumerator CloseStartMenu()
    {
        startMenuAnimator.SetBool("CloseMenu", true);
        yield return new WaitForSeconds(1f);        
        startMenuAnimator.gameObject.SetActive(false);        
    }

    public IEnumerator SpriteToggle(Sprite sprite, SpriteRenderer renderer)
    {
        if (spriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            spriteAnimator.SetBool("FadeOut", true);
            spriteAnimator.SetBool("FadeIn", false);

            yield return new WaitForSeconds(1f);
        }
        renderer.sprite = sprite;

        spriteAnimator.SetBool("FadeOut", false);
        spriteAnimator.SetBool("FadeIn", true);

        yield return new WaitForSeconds(2f);

        spriteAnimator.SetBool("FadeOut", true);
        spriteAnimator.SetBool("FadeIn", false);
    }    
}
