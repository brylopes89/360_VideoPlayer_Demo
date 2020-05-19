using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.iOS;
using UnityEngine.XR;

public class InputManager : MonoBehaviour
{   
    public VideoManager videoManager = null;
    public List<XRController> controllers = null;
    public GameObject videoMenu = null;
    public GameObject player = null;
    public float distance = 10f;

    private Animator animator = null;    

    private void Start()
    {
        animator = videoMenu.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!videoManager.IsVideoReady)
            return;

        VRInput();
        KeyboardInput();
    }
    //private void OculusInput()
    //{
    //    if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.All))
    //    {
    //        videoManager.PauseToggle();
    //    }

    //    if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
    //    {

    //    }

    //    if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
    //    {

    //    }

    //    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
    //    {

    //    }

    //    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
    //    {

    //    }
    //}    

    private void VRInput()
    {
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
            {
                if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool value))
                {                                       
                    videoMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
                    videoMenu.transform.rotation = Quaternion.LookRotation(videoMenu.transform.position - player.transform.position);
                    OpenMenu();                                       
                }                    
            }                
        }
    }
    private void KeyboardInput()
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

    private void OpenMenu()
    {
        bool isOpen = animator.GetBool("OpenMenu");

        animator.SetBool("OpenMenu", !isOpen);
    }
}
