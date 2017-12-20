using UnityEngine;
//Let's break down this line word by word.
//Public means this line of whatever is available to anything that has access to it.
//I'm not sure what abstract means.
//class refers to a type of object this is going to be
//Menu is the name of the class.
//<T> is put here because this menu class can be overloaded...?
//<T> is casts to the Menu class.
//Where specifies that T is also supposed to represent other Menu<T>s.
//I think I didn't do that 100% correctly so I'll need to refresh myself.
public abstract class Menu<T>: Menu where T: Menu<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake() {
        Instance = (T)this;
		//Debug.Log("Am I alive?");
    }

    protected virtual void OnDestroy() {
        Instance = null;
	}

	protected static void Open() {
		if (Instance == null) {
			//Debug.Log("Instance == null in Menu's Open function...");
			//I think this is only supposed to be called when menu first
			//	launchs?
			MenuManager.Instance.CreateInstance<T>();
		}
		
		else {
			Instance.gameObject.SetActive(true);
		}
		//	Instance.gameObject.SetActive(true);
			MenuManager.Instance.OpenMenu(Instance);
	}

	protected static void Close() {
		if (Instance == null) {
			Debug.LogErrorFormat(
				"Trying to close menu {0} but Instance is null",
				typeof(T));
			return;
		}

		MenuManager.Instance.CloseMenu(Instance);
	}

	public override void OnBackPressed() {
		Close();
	}
}

public abstract class Menu : MonoBehaviour {
	[Tooltip(
		"Destroy the Game Object when menu is closed (reduces memory usage)")]
	public bool DestroyWhenClosed = true;

	[Tooltip("Disable menus that are under this one in the stack")]
	public bool DisableMenusUnderneath = true;

	public abstract void OnBackPressed();
}
