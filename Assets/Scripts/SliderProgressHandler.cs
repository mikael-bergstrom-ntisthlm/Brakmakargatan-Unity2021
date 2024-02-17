using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderProgressHandler : ProgressHandler
{
  public override void UpdateValues(float current, float max)
  {
    GetComponent<Slider>().value = current / max;
  }
}
