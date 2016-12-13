using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Utils;

public class ProfilePanelController : MonoBehaviour {

	public GameObject canvas;

	public Text nameText;
	public Image avatar;
	public int profileNumber;
	Profile profile;
	ProfileMessage msg;

	FileWorker fileWorker;
	// Use this for initialization
	void Start () {
		string name = PlayerPrefs.GetString("Profile" + profileNumber);
		Debug.Log("Profile " + profileNumber + " " + profile);
		fileWorker = new FileWorker();
		if (name.Length > 0) {
			loadProfile(name);
			if (profile.name.Length > 0) {
				nameText.text = profile.name;
				if (profile.avatar.Length > 0) {
					avatar.sprite = Resources.Load<Sprite>(profile.avatar);
				}
			}
		} else {
			profile = new Profile();
		}
		msg = new ProfileMessage(profile, profileNumber);
	}

	void loadProfile(string profileName) {
		string res = fileWorker.readFile(fileWorker.getCurrentDirectory() + CreationMain.PROFILE_PREFIX + profileName + "\\" + "profile.txt");
		Debug.Log(res);
		profile = new Profile();
		profile.readFromJson(res);
	}

	public void showOptions() {
		canvas.SendMessage("showOptions", msg);
	}

	public void deleteProfile() {
		string currentPlayer = PlayerPrefs.GetString("Player");
		if (currentPlayer.Length > 0 && profile.name.Length > 0 && profile.name == currentPlayer) {
			PlayerPrefs.SetString("Player", null);
		}
		profile = null;
		msg = null;
		nameText.text = "Profile name";
		avatar.sprite = Resources.Load<Sprite>(CreationMain.DEFAULT_TEXTURE);
	}
	
}
