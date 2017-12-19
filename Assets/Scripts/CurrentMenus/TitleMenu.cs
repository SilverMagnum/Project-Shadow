using UnityEngine;

public class TitleMenu : SimpleMenu<TitleMenu> {
	public void OnLoadGamePressed() {
		//Debug.Log("Load Game button was pressed.");
		//Load Menu Load
	}
	
    public void OnNewGamePressed() {
		//Debug.Log("New Game button was pressed.");
		//Scene 1 Load
	}

	public void OnLevelEditorPressed() {
		//Debug.Log("Level Editor button was pressed.");
	}
	
	public void OnSettingsPressed() {
		SettingsMenu.Show();
		//Debug.Log("Settings button was pressed.");
	}
	
	public void OnQuitPressed() {
		//Debug.Log("Quit button was pressed.");
		Application.Quit();
	}
	
	public override void OnBackPressed() {
		Application.Quit();
	}
}
