using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NameText : MonoBehaviour
{


  // Cache:
  private TextMeshProUGUI text;
  private int lastUserID = -1;

  // Messages:

  void Awake()
  {
    text = GetComponent<TextMeshProUGUI>();
    text.enabled = false;
  }

  void Update()
  {
    if(lastUserID != LudumDareAPI.GetUserID())
    {
      string name = LudumDareAPI.GetUsername();
      if(!string.IsNullOrEmpty(name))
      {
        lastUserID = LudumDareAPI.GetUserID();
        text.text = name;
        text.enabled = true;
      }
    }
  }

}
