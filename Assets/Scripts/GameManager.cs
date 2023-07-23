using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum PlayerMode{ Player,AirPlane, Car,Scoter}
    public static GameManager instance;
    public UISettings uI;
    public PlayerMode playerMode;
    [SerializeField] public List<GameObject> AllItamsCars = new List<GameObject>();//جميع المركبات واللغيب


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }






    // AllItamsCarsمن اجل اختيار ال
    public void SelcetPlayersCars(GameObject value = null)
    {

        foreach (GameObject obj in AllItamsCars)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in AllItamsCars)
        {
            if (value == obj && value != null){

                obj.SetActive(true);
            }
        }
    }
}
