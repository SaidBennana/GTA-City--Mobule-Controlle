using UnityEngine;
using EasyJoystick;
using DG.Tweening;

public enum AirPram
{
    Mobile,
    Pc
}
public class AirControll : MonoBehaviour
{

    [SerializeField] AirPram control;
    [SerializeField] float FlySpeed = 13.5f;
    [SerializeField] bool isEngane = false;
    [SerializeField] float Vinput;
    [SerializeField] float Hinput;
    [SerializeField] float SmoothRot;
    [SerializeField] Joystick joystick;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody rd;
    [SerializeField] bool isGround;
    [SerializeField] LayerMask layerMaskChake;
    [SerializeField] float rayCastDistance = 0.9f;

    // مكان خروج العيب
    [SerializeField] Transform posOut;

    // yaw
    private float YawAmount = 120;
    private float Yaw;
    private float Patch;
    private float Roll;



    void Update()
    {
        if (GameManager.instance.playerMode == GameManager.PlayerMode.AirPlane)
        {
            if (GameManager.instance.playerMode != GameManager.PlayerMode.Player)
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (isEngane == false)
                    {
                        isEngane = true;
                    }
                    else
                    {
                        isEngane = false;
                    }
                    anim.enabled = isEngane;

                }
                if (isEngane)
                {
                    transform.position += transform.forward * FlySpeed * Time.deltaTime;
                    rd.position = transform.position;
                }


                if (control == AirPram.Mobile)
                {
                    Hinput = joystick.Horizontal();
                    Vinput = joystick.Vertical();

                }
                else
                {
                    Hinput = Input.GetAxis("Horizontal");
                    Vinput = Input.GetAxis("Vertical");
                }
                Yaw += Hinput * YawAmount * Time.deltaTime;
                Patch = Mathf.Lerp(0, 30, Mathf.Abs(Vinput)) * Mathf.Sign(Vinput);
                Roll = Mathf.Lerp(0, 60, Mathf.Abs(Hinput)) * -Mathf.Sign(Hinput);
                transform.localRotation = Quaternion.Lerp(Quaternion.Euler(Vector3.up * Yaw + Vector3.right * Patch + Vector3.forward * Roll),
                transform.rotation
                , SmoothRot);
            }

        }






    }

    private void FixedUpdate()
    {
        if(isEngane){
            FlySpeed = Mathf.Clamp(FlySpeed + Time.fixedDeltaTime,5,20);
        }
        isGround = Physics.CheckSphere(transform.position, rayCastDistance, layerMaskChake);
        if (GameManager.instance.playerMode == GameManager.PlayerMode.Player)
        {
            if (!isGround )
            {
                transform.position -= transform.up * FlySpeed * Time.deltaTime;
                isEngane=false;
                anim.enabled=isEngane;

            }

        }
        else if(isEngane==false &&  !isGround){
            transform.position -= transform.up * FlySpeed * Time.deltaTime;
        }
        // RaycastHit Hit;
        // if (Physics.Raycast(transform.position, Vector3.down, out Hit, rayCastDistance, layerMaskChake))
        // {
        //     isGround = true;
        // }
        // else
        // {
        //     isGround = false;
        // }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rayCastDistance);
    }
    // زر الخروج من الطائرة
    public void OutButton()
    {

        GameManager.instance.AllItamsCars[1].transform.position = posOut.position;
        GameManager.instance.AllItamsCars[1].SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
        GameManager.instance.playerMode = GameManager.PlayerMode.Player;
        if (isEngane)
        {
            isEngane = false;
        }

    }

    // private void OnCollisionStay(Collision other)
    // {

    //     if (other.collider.gameObject.layer == layerMaskChake)
    //     {
    //         isGround = true;
    //         transform.localRotation =Quaternion.Lerp(Quaternion.Euler( 38.517f,transform.rotation.y,transform.rotation.z ),transform.rotation,SmoothRot) ;
    //     }
    // }
    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.collider.gameObject.layer == layerMaskChake)
    //     {
    //         isGround = false;
    //     }

    // }
}
