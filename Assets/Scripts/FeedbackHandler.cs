using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackHandler : MonoBehaviour
{    
    [Header("Managers")]
    public VideoManager videoManager = null;
    public AnimationManager animationManager = null;

    [Header("Icons")]
    public Sprite pause = null;
    public Sprite play = null;
    public Sprite load = null;  

    private SpriteRenderer spriteRenderer = null;    
    private float distance = 2.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        SetupWithCamera();
        
        videoManager.onPause.AddListener(PressPause);
        videoManager.onPlay.AddListener(PressPlay);
                
    }

    private void OnDestroy()
    {
        videoManager.onPause.RemoveListener(PressPause);             
    }

    private void SetupWithCamera()
    {
        transform.parent = Camera.main.transform;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0, 0, distance);
    }

    private void PressPause(bool isPaused)
    {
        if (isPaused && videoManager.IsVideoReady)                   
            StartCoroutine(AnimationManager.animManager.SpriteToggle(pause, spriteRenderer));                         
    }

    private void PressPlay(bool isPlaying)
    {
        if(isPlaying && videoManager.IsVideoReady)
            StartCoroutine(AnimationManager.animManager.SpriteToggle(play, spriteRenderer));
    }
}
