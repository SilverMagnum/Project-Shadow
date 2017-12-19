using UnityEngine.UI;

public class OptionalsMenu : SimpleMenu<OptionalsMenu>
{
	public Slider Slider;

	public void OnMagicButtonPressed()
	{
		AwesomeMenu.Show(Slider.value);
	}
}
