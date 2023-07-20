using UnityEngine;
using UnityEngine.EventSystems;


public class TouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Vector2 TouchDis;
    [SerializeField] bool preesed;
    private Vector2 PointerOld;
    private int PointerId;
    private void Update() {
        if(preesed){
            if(PointerId >= 0 && PointerId <= Input.touches.Length){
                TouchDis=Input.touches[PointerId].position - PointerOld;
                PointerOld=Input.touches[PointerId].position;
                

            }else{
                TouchDis=new Vector2(Input.mousePosition.x,Input.mousePosition.y)-PointerOld;
                PointerOld=Input.mousePosition;

            }
        }else{
            TouchDis=Vector2.zero;
        }
    }

    
    public void OnPointerDown(PointerEventData eventData)
    {
        preesed=true;
        PointerId=eventData.pointerId;
        PointerOld=eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData){
        preesed=false;

    }

}

