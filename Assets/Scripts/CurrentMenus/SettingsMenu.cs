using UnityEngine.UI;

public class SettingsMenu : SimpleMenu<SettingsMenu> {
	public void OnControlsPressed() {
		//Debug.Log("Controls button was pressed.");
		//ControlsMenu.Show();
	}
	
	public void OnGraphicsPressed() {
		//Debug.Log("Graphics button was pressed.");
		//GraphicsMenu.Show();
	}
	
	public void OnAudioPressed() {
		//Debug.Log("Audio button was pressed.");
		//AudioMenu.Show();
	}
	
	//public Slider Slider;
}
