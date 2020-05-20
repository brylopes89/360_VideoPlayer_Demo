using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{      
    public VideoManager videoManager = null;        
    public GameObject videoMenu = null;
    public GameObject startMenu = null;
    public GameObject player = null;

    public float distance = 10f;

    private void Start()
    {        
        if (startMenu != null)
        {
            startMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
            startMenu.transform.rotation = Quaternion.LookRotation(videoMenu.transform.position - player.transform.position);
        }
    }

    private void Update()
    {
        if (!videoManager.IsVideoReady)
            return;

        OculusInput();        
        KeyboardInput();
    }

    private void OculusInput() //For Oculus(Go) setup
    {
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.All) && videoManager.IsVideoReady)
        {
            videoMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
            videoMenu.transform.rotation = Quaternion.LookRotation(videoMenu.transform.position - player.transform.position);
            AnimationManager.animManager.OpenVideoMenu();
        }        
    }    

    private void KeyboardInput() //For standalone testing
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            videoManager.Pause();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            videoManager.PreviousVideo();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            videoManager.NextVideo();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            videoManager.SeekBack();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            videoManager.SeekForward();
        }
    }
}
