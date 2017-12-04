using UnityEngine;

public class Present : MonoBehaviour
{

  // Referances:
  public HitchLib.ColorEnum color;
  public SpriteRenderer box;
  public SpriteRenderer ribbon;

  // Messages:

  void OnEnable()
  {
    Color pColor = HitchLib.Colors.FromEnum(color);
    float H, S, V;
    Color.RGBToHSV(pColor, out H, out S, out V);
    box.color = pColor;
    ribbon.color = Color.HSVToRGB((H + 0.5f) % 1.0f, S * 0.5f, (V * 0.25f) + 0.75f);
  }

}
