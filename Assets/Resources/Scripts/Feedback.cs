using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    public float distance = 2.5f;
    public VideoManager videoManager = null;

    [Header("Icons")]
    public Sprite pause = null;
    public Sprite play = null;
    public Sprite load = null;

    private SpriteRenderer spriteRenderer = null;
    private Animator animator = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        SetupWithCamera();

        videoManager.onPause.AddListener(TogglePause);
        videoManager.onLoad.AddListener(ToggleLoad);
    }

    private void OnDestroy()
    {
        videoManager.onPause.RemoveListener(TogglePause);
        videoManager.onLoad.RemoveListener(ToggleLoad);
    }

    private void SetupWithCamera()
    {
        transform.parent = Camera.main.transform;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0, 0, distance);
    }

    private void TogglePause(bool isPaused)
    {
        if (isPaused)
        {            
            StartCoroutine(SpriteToggle(pause));
        }
        else
        {
            StartCoroutine(SpriteToggle(play));
        }           
    }

    private void ToggleLoad(bool isLoaded)
    {
        //spriteRenderer.sprite = load;
        //spriteRenderer.enabled = !isLoaded;
    }

    private IEnumerator SpriteToggle(Sprite sprite)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            animator.SetBool("FadeOut", true);
            animator.SetBool("FadeIn", false);

            yield return new WaitForSeconds(1f);            
        }
        spriteRenderer.sprite = sprite;

        animator.SetBool("FadeOut", false);
        animator.SetBool("FadeIn", true);

        yield return new WaitForSeconds(2f);

        animator.SetBool("FadeOut", true);
        animator.SetBool("FadeIn", false);
    }
}
