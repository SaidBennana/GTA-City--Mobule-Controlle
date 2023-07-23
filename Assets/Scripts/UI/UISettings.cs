using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public Button openCar;




    // زر ركوب السيارة وغيرها
    public void ShowButtonOpenCar(GameObject modPlayer, bool CanShow, string PlayerMode)
    {
        openCar.gameObject.SetActive(CanShow);
        openCar.onClick.AddListener(() =>
        {
            GameManager.instance.SelcetPlayersCars(modPlayer);
            switch (PlayerMode)
            {
                case "AirPlans":
                    {
                        GameManager.instance.playerMode = GameManager.PlayerMode.AirPlane;
                        modPlayer.transform.Find("Controlle").gameObject.SetActive(true);
                        FindObjectOfType<AirControll>().enabled=true;
                        break;

                    }
                case "Player":
                    {
                        GameManager.instance.playerMode = GameManager.PlayerMode.Player;
                        modPlayer.transform.Find("Controlle").gameObject.SetActive(false);
                        GameManager.instance.AllItamsCars[1].SetActive(true);
                        FindObjectOfType<AirControll>().enabled=false;
                        break;

                    }
            }

        });

    }


}
