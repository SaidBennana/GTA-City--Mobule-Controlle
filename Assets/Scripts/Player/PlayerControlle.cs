using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlle : MonoBehaviour
{

    [SerializeField] float SpeedMove;
    [SerializeField] CharacterController character;
    [SerializeField] Vector3 Distance;
    [SerializeField] float Vinput, Hinput;
    private Camera Cam;
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
        }

        // Jump fun
        if(Input.GetButtonDown("Jump")){
            jump();
        }
        Vinput = Input.GetAxisRaw("Vertical");
        Hinput = Input.GetAxisRaw("Horizontal");
        Distance = new Vector3(Hinput, 0, Vinput);

        if (Distance.magnitude >= 0.1f)
        {
            float angel = Mathf.Atan2(Distance.x, Distance.z) * Mathf.Rad2Deg + Cam.transform.eulerAngles.y;
            float TargetAngel = Mathf.SmoothDampAngle(transform.eulerAngles.y, angel, ref RefValue, SmoothValue);
            transform.rotation = Quaternion.Euler(0, TargetAngel, 0);
            Vector3 MoveTo = Quaternion.Euler(0, angel, 0) * Vector3.forward;
            character.Move(MoveTo * SpeedMove * Time.deltaTime);

        }
        vilosity.y += gravity * Time.deltaTime;
        character.Move(vilosity * Time.deltaTime);

    }
    void jump()
    {
        if (isGraund)
            vilosity.y = Mathf.Sqrt(speedJump * -2f * gravity);
    }
}
