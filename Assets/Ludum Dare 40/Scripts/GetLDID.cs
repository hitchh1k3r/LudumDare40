using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GetLDID : MonoBehaviour
{

  // Referances:
  public TMP_InputField firstName;
  public TMP_InputField lastName;
  public TMP_InputField input;

  void Awake()
  {
    string[] name = NameManager.GetName(Random.Range(0, 1) == 0).Split(' ');
    firstName.text = name[0];
    lastName.text = name[1];
  }

  public void SubmitName()
  {
    LudumDareAPI.SetUser(input.text);
    NameManager.SetMyName(firstName.text, lastName.text);
  }

}
