using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;

public class PlayerControlle : MonoBehaviour
{
    enum Controlle { Mobile, Pc }
    [SerializeField] Controlle IsMobile;

    [SerializeField] float SpeedMove;
    [SerializeField] CharacterController character;
    [SerializeField] Vector3 Distance;
    [SerializeField] float Vinput, Hinput;
    private Camera Cam;
    [SerializeField] Joystick joystick;
    [Header("Smooth Settings")]
    [SerializeField] float SmoothValue;
    private float RefValue = 0;
    [Header("Jump Settings")]
    [SerializeField] float speedJump = 2;
    [SerializeField] float gravity = -9f;
    Vector3 vilosity;
    [SerializeField] bool isGraund = true;
    [SerializeField] Transform PointChackGround;
    [SerializeField] LayerMask LayerMaskChackGround;

    [Header("Animation")]
    [SerializeField] Animator anim;

    void Start()
    {
        Cam = Camera.main;

    }

    void Update()
    {
        isGraund = Physics.CheckSphere(PointChackGround.position, 0.4f, LayerMaskChackGround);
        if (vilosity.y < 0 && isGraund)
        {
            vilosity.y = -2f;
            anim.SetBool("isJump", false);
            anim.SetFloat("JumpValue",1f);
        }
        else
        {
            anim.SetBool("isJump", true);
            anim.SetFloat("JumpValue",0.5f);
        }

        // Jump fun
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetFloat("JumpValue",0f);
            jump();
        }
        if (IsMobile == Controlle.Mobile)
        {
            Vinput = joystick.Vertical();
            Hinput = joystick.Horizontal();

        }
        else
        {
            Vinput = Input.GetAxisRaw("Vertical");
            Hinput = Input.GetAxisRaw("Horizontal");
        }
        Distance = new Vector3(Hinput, 0, Vinput);

        if (Distance.magnitude >= 0.1f)
        {

            float angel = Mathf.Atan2(Distance.x, Distance.z) * Mathf.Rad2Deg + Cam.transform.eulerAngles.y;
            float TargetAngel = Mathf.SmoothDampAngle(transform.eulerAngles.y, angel, ref RefValue, SmoothValue);
            transform.rotation = Quaternion.Euler(0, TargetAngel, 0);
            Vector3 MoveTo = Quaternion.Euler(0, angel, 0) * Vector3.forward;
            character.Move(MoveTo * SpeedMove * Time.deltaTime);

        }



        anim.SetFloat("ForwardSpeed", Distance.magnitude);
        vilosity.y += gravity * Time.deltaTime;
        character.Move(vilosity * Time.deltaTime);

    }
    void jump()
    {
        if (isGraund)
        {

            vilosity.y = Mathf.Sqrt(speedJump * -2f * gravity);

        }
    }
}
