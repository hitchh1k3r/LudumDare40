using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Signup : MonoBehaviour
{

  // Referances:
  public TextMeshProUGUI errorLine;
  public TMP_InputField firstName;
  public TMP_InputField lastName;
  public TMP_InputField ldName;
  public Button submitButton;
  public TextMeshProUGUI buttonText;

  // State:
  private bool isProcessing;

  void Awake()
  {
    string[] name = NameManager.GetName(Random.Range(0, 1) == 0).Split(' ');
    firstName.text = name[0];
    lastName.text = name[1];
  }

  public void SubmitButton()
  {
    if(!isProcessing)
    {
      errorLine.text = "";
      isProcessing = true;
      submitButton.interactable = false;
      buttonText.text = "working...";

      if(firstName.text.Trim() == "" || lastName.text.Trim() == "")
      {
        errorLine.text = "*First and Last Name are Required";
        isProcessing = false;
        submitButton.interactable = true;
        buttonText.text = "Submit";
        return;
      }
      NameManager.SetMyName(firstName.text, lastName.text);

      if(ldName.text != "")
      {
        LudumDareAPI.SetUser(ldName.text, () => {
                Debug.Log("CALLBACK!");
                isProcessing = false;
                if(LudumDareAPI.GetUserID() > 2)
                {
                  InterfaceManager.ShowFriendBay();
                }
                else
                {
                  submitButton.interactable = true;
                  buttonText.text = "Submit";
                  errorLine.text = "*Ludum Dare Username Could Not Be Found";
                }
              });
      }
      else
      {
        InterfaceManager.ShowFriendBay();
      }
    }
  }

}
