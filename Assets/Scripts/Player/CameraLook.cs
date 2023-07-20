using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum Pram
{
    Mouse,
    TouchPad
}

public class CameraLook : MonoBehaviour
{
    [SerializeField] Pram Choess;
    [SerializeField] CinemachineFreeLook freeLook;
    [SerializeField] TouchPad touchPad;
    [SerializeField] float TouchX, TouchY;
    [SerializeField] float SpeedMouse = 3f;


    void Update()
    {
        if (Choess == Pram.TouchPad)
        {

            TouchX = touchPad.TouchDis.x;
            TouchY = touchPad.TouchDis.y;

            if (TouchX > 0f || TouchX < 0f)
            {
                print("MoveCamX");
                freeLook.m_XAxis.Value += TouchX * 200 * SpeedMouse * Time.deltaTime;
            }
            if (TouchY > 0f || TouchY < 0f)
            {
                print("MoveCamY");
                freeLook.m_YAxis.Value += TouchY *  SpeedMouse * Time.deltaTime;
            }


        }
        else
        {
            freeLook.m_XAxis.m_InputAxisName = "Mouse X";
            freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        }

    }
}
