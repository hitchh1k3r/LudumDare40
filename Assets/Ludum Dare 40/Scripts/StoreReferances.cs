using UnityEngine;
using TMPro;

public class StoreReferances : MonoBehaviour
{

  public GameObject table1;
  public GameObject table2;
  public PresentMover belt;
  public TextMeshProUGUI timer;
  public bool storeTimerOn;
  public float storeTimerLeft = 150;

  public static StoreReferances instance;

  void Awake()
  {
    instance = this;
  }

  void Update()
  {
    if(storeTimerOn)
    {
      timer.gameObject.SetActive(true);
      if(storeTimerLeft > 0)
      {
        timer.text = ((int)storeTimerLeft).ToString();
      }
      else
      {
        timer.text = "0";
      }
      storeTimerLeft -= Time.deltaTime;
    }
    else
    {
      timer.gameObject.SetActive(false);
    }
  }

}
