using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VersionString : MonoBehaviour
{

  public TextAsset versionFile;

  void Awake()
  {
    GetComponent<TextMeshProUGUI>().text = versionFile.text;
  }

}
