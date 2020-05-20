using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoEvent : UnityEvent<bool> { }
public class VideoManager : MonoBehaviour
{
    public List<VideoClip> videos = null;        

    public VideoEvent onPause = new VideoEvent();
    public VideoEvent onPlay = new VideoEvent();    

    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } private set { isPaused = value; onPause.Invoke(isPaused); } }

    private bool isPlaying = false;
    public bool IsPlaying { get { return isPlaying; } private set { isPlaying = value; onPlay.Invoke(isPlaying); } }

    private bool isVideoReady = false;
    public bool IsVideoReady { get { return isVideoReady; } private set { isVideoReady = value; } }   

    private int index = 0;
    private VideoPlayer videoPlayer = null;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.seekCompleted += OnComplete;
        videoPlayer.prepareCompleted += OnComplete;
        videoPlayer.loopPointReached += OnLoop;

        OVRManager.HMDMounted += HandleHDMounted;
        OVRManager.HMDUnmounted += HandleHDUnmounted;
    }

    public void StartPrepare()
    {
        StartPrepare(index);
        StartCoroutine(AnimationManager.animManager.CloseStartMenu());           
    }

    public void Quit()
    {
        Application.Quit();        
    }

    public void Pause()
    {
        IsPaused = true;
        IsPlaying = false;
        videoPlayer.Pause();       
    }

    public void Play()
    {
        IsPlaying = true;
        IsPaused = false;    
        videoPlayer.Play();
    }

    private void OnDestroy()
    {
        videoPlayer.seekCompleted -= OnComplete;
        videoPlayer.prepareCompleted -= OnComplete;
        videoPlayer.loopPointReached -= OnLoop;

        OVRManager.HMDMounted -= HandleHDMounted;
        OVRManager.HMDUnmounted -= HandleHDUnmounted;
    }

    private void HandleHDMounted()
    {
        Play();
    }

    private void HandleHDUnmounted()
    {
        Pause();
    }

    public void SeekForward()
    {
        StartSeek(5.0f);
    }

    public void SeekBack()
    {
        StartSeek(-5.0f);
    }

    private void StartSeek(float seekAmount)
    {
        IsVideoReady = false;    
        videoPlayer.time += seekAmount;
    }

    public void NextVideo()
    {
        index++;
        if (index == videos.Count)
            index = 0;

        StartPrepare(index);
    }

    public void PreviousVideo()
    {
        index--;
        if (index == -1)
            index = videos.Count - 1;

        StartPrepare(index);
    }

    private void StartPrepare(int clipIndex)
    {
        IsVideoReady = false;     
        videoPlayer.clip = videos[clipIndex];        
        videoPlayer.Prepare();
    }

    private void OnComplete(VideoPlayer videoPlayer)
    {
        IsVideoReady = true;       
        videoPlayer.Play();
    }

    private void OnLoop(VideoPlayer videoPlayer)
    {
        NextVideo();
    }   
}
