using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NameText : MonoBehaviour
{


  // Cache:
  private TextMeshProUGUI text;

  // Messages:

  void Awake()
  {
    text = GetComponent<TextMeshProUGUI>();
    text.enabled = false;
  }

  void Update()
  {
    string name = NameManager.GetMyName();
    if(!string.IsNullOrEmpty(name))
    {
      text.text = name;
      text.enabled = true;
      enabled = false;
    }
  }

}
