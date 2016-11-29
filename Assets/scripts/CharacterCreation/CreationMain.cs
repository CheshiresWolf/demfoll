using UnityEngine;
using Generator;
using UnityEngine.UI;
using System.Collections.Generic;
using Utils;
using System.Collections;
using System;

public class CreationMain : MonoBehaviour {

    string str;
    public Text text;

    int imageCounter = 0;
    Sprite[] sprites = new Sprite[3];
    string[] images = new string[3] {"portrait1", "portrait2", "portrait3"};

    static List<State> stateOptions = new List<State>() {
        new State(),
        new State("origin", 2),
        new State("biography", 2),
        new State("mentor", 1),
        new State("perks", 2),
        new State("manners", 2)
    };
    State currentState = stateOptions[0];

    public Button nextBtn;
    public Button previousBtn;
    public Button createBtn;
    public Button deleteBtn;
    public Button continueBtn;
    public Button startBtn;
    public GameObject imageObject;

    GameObject controlPanel;
    GameObject originPanel;
    GameObject biographyPanel;
    GameObject mentorPanel;
    GameObject perksPanel;
    GameObject mannersPanel;
    GameObject generalInfoPanel;
    public GameObject profilesPanel;
    public GameObject singleProfile;
    public Text profileName;
    public Image profileImage;
    public ProfilePanelController[] profilePanels = new ProfilePanelController[3];

    Image playerImage;
    InputField playerName;

    Dictionary<string, ToggleGroup> toggleGroups = new Dictionary<string, ToggleGroup>();
    GameObject togglePrefab;

    HorizontalLayoutGroup layout;

    public const string PROFILE_PREFIX = "\\profiles\\";
    public const string DEFAULT_TEXTURE = "faceless";
    Player player;
    Profile currentProfile;
    int profileIndex = 1;

    FileWorker fileWorker;

    Origin origin = new Origin();
    Biography biography = new Biography();
    Mentor mentor = new Mentor();
    Perks perk = new Perks();
    Manners manner = new Manners();

    List<string> originList = new List<string> { "Далекі Колонії", "Шляхетний дім"};
    List<string> biographyList = new List<string> { "Секретний кур'єр", "Бульварний журналіст", "Колоніальний агент", "Вічний студент", "Джентльмен-Грабіжник", "Салонний окультист", };
    List<string> mentorList = new List<string> { "Мандрівний Лицар", "Єретичний Богослов", "Чаруюча Куртизанка", "Політик Кукловод", "Відставний Шпигун", };
    List<string> perksList = new List<string> { "Емпат", "Дипломат", "Геніальний", "Імунітет", "Доктор"};
    List<string> mannersList = new List<string> { "Грубість", "Дипломатія", "Таємничість", "Гумор", "Відкритість", "Маніпуляція" };

    void Start() {
        //toggleGroups.Clear();
        player = new Player();
        togglePrefab = GameObject.Find("TogglePrefab");
        RandomUtils utils = new RandomUtils();
        
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i] = Resources.Load<Sprite>(images[i]);
        }

        for(int i = 0; i < profilePanels.Length; i++) {
            profilePanels[i] = (ProfilePanelController)GameObject.Find("ProfilePanel" + (i+1)).GetComponent("ProfilePanelController");
        }

        playerImage = imageObject.GetComponent<Image>();
        playerImage.sprite = sprites[imageCounter];

        playerName = GameObject.Find("NameInput").GetComponent<InputField>();
        //Debug.Log("Name: " + playerName.text);

        text = GameObject.Find("Text").GetComponent<Text>();
        text.gameObject.SetActive(false);

        controlPanel = GameObject.Find("ControlPanel");
        originPanel = GameObject.Find("OriginPanel");
        biographyPanel = GameObject.Find("BiographyPanel");
        mentorPanel = GameObject.Find("MentorPanel");
        perksPanel = GameObject.Find("PerksPanel");
        mannersPanel = GameObject.Find("MannersPanel");
        generalInfoPanel = GameObject.Find("GeneralInfoPanel");

        layout = controlPanel.GetComponent<HorizontalLayoutGroup>();

        layout.spacing = 100;
        controlPanel.gameObject.SetActive(false);

        //profilesPanel.SetActive(false);

        nextBtn.gameObject.SetActive(false);
        previousBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(false);

        singleProfile.SetActive(false);

        generalInfoPanel.SetActive(false);
        toggleGroups.Add("origin", AddChildren(originList, originPanel, false));
        toggleGroups.Add("biography", AddChildren(biographyList, biographyPanel, false));
        toggleGroups.Add("mentor", AddChildren(mentorList, mentorPanel, false));
        toggleGroups.Add("perks", AddChildren(perksList, perksPanel, false));
        toggleGroups.Add("manners", AddChildren(mannersList, mannersPanel, false));

        togglePrefab.SetActive(false);
        //StartCoroutine(AnimateText("Pretty cool text"));
        fileWorker = new FileWorker();

        //startCreation();

    }
    // Update is called once per frame
    void Update() {

    }

    void showOptions(ProfileMessage msg) {
        currentProfile = msg.profile;
        profileIndex = msg.index;
        Debug.Log("received message " + msg.profile.name + " " + msg.index);

        profilesPanel.SetActive(false);
        singleProfile.SetActive(true);
        controlPanel.SetActive(true);
        bool optionsMode = false;
        string name = "Profile name";
        Sprite sprite = Resources.Load<Sprite>(DEFAULT_TEXTURE);
        if(currentProfile.name.Length > 0) {
            optionsMode = true;
            name = currentProfile.name;
            sprite = Resources.Load<Sprite>(currentProfile.avatar);
        }
        profileName.text = name;
        profileImage.sprite = sprite;
        toggleControls(optionsMode);
        previousBtn.gameObject.SetActive(true);
    }

    void toggleControls(bool enable) {
        createBtn.gameObject.SetActive(!enable);
        deleteBtn.gameObject.SetActive(enable);
        continueBtn.gameObject.SetActive(enable);
    }

    public void startCreation() {
        generalInfoPanel.SetActive(true);
        nextBtn.gameObject.SetActive(true);
        controlPanel.SetActive(false);
        singleProfile.SetActive(false);
    }

    public void deleteProfile() {
        PlayerPrefs.SetString("Profile" + profileIndex, null);
        string fullPath = fileWorker.getCurrentDirectory() + PROFILE_PREFIX + currentProfile.name;
        Debug.Log(fullPath);
        fileWorker.deleteDirectory(fullPath);
        currentProfile = null;
        profilePanels[profileIndex-1].deleteProfile();
        returnToProfiles();
    }

    public void nextImage() {
        imageCounter++;
        if (imageCounter > images.Length-1) {
            imageCounter = 0;
        }
        playerImage.sprite = sprites[imageCounter];
    }

    public void previousImage() {
        imageCounter--;
        if (imageCounter < 0) {
            imageCounter = images.Length-1;
        }
        playerImage.sprite = sprites[imageCounter];
    }

    public void nextStep() {
        int currentIndex = 0;
        ToggleGroup group;
        if (currentState.name.Length > 0) {
            group = toggleGroups[currentState.name];
            group.hide();
            currentIndex = stateOptions.IndexOf(currentState);
        }

        if (currentIndex == 0) {
            generalInfoPanel.SetActive(false);
            previousBtn.gameObject.SetActive(true);
        }
        nextBtn.gameObject.SetActive(false);
        currentIndex++;
        currentState = stateOptions[currentIndex];
        group = toggleGroups[currentState.name];
        group.show();
    }

    public void previousStep() {
        int currentIndex = stateOptions.IndexOf(currentState) - 1;
        if (singleProfile.activeSelf || currentIndex < 0) {
            returnToProfiles();
            return;
        }
        ToggleGroup group = toggleGroups[currentState.name];
        group.toggleOff();
        group.hide();
        nextBtn.gameObject.SetActive(true);
        currentState = stateOptions[currentIndex];

        if (currentIndex == 0) {
            generalInfoPanel.SetActive(true);
            return;
        }
        group = toggleGroups[currentState.name];
        group.show();
    }

    void returnToProfiles() {
        singleProfile.SetActive(false);
        controlPanel.SetActive(false);
        generalInfoPanel.SetActive(false);
        profilesPanel.SetActive(true);
        previousBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);
    }

    public void toggleCheck(bool isChecked) {
        if (currentState.name.Length == 0) return;
        ToggleGroup group = toggleGroups[currentState.name];
        int checkCount = group.countToggled();
        if (checkCount >= currentState.activeToggles) {
            group.disableToggles();

            int currentIndex = stateOptions.IndexOf(currentState);
            if (currentIndex < stateOptions.Count-1) {
                nextBtn.gameObject.SetActive(true);
            }
            if (currentState.name == "manners") {
                startBtn.gameObject.SetActive(true);
            }
        } else {
            group.enableToggles();

            nextBtn.gameObject.SetActive(false);
            startBtn.gameObject.SetActive(false);
        }
    }


    void savePlayer() {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        string fullPath = fileWorker.getCurrentDirectory() + PROFILE_PREFIX + currentProfile.name;

        fileWorker.createDirectory(fullPath);

        fileWorker.writeFile(fullPath + "\\" + "player.txt", player.convertToJson());
        fileWorker.writeFile(fullPath + "\\" + "profile.txt", currentProfile.convertToJson());

        Debug.Log("Player save time : " + sw.ElapsedMilliseconds + " ms");
        sw.Stop();
    }

    ToggleGroup AddChildren(List<string> collection, GameObject panel, bool visible) {
        int i = 0, count = collection.Count;
        List<GameObject> buffer = new List<GameObject>();
        while (i < count) {
            GameObject child = Instantiate(togglePrefab) as GameObject;
            child.transform.SetParent(panel.transform, false);
            Text toggleText = child.transform.FindChild("Label").GetComponent<Text>();
            toggleText.text = collection[i];
            buffer.Add(child);
            i++;
            //yield return null;//new WaitForSeconds(0.01f);
        }
        panel.gameObject.SetActive(visible);
        return new ToggleGroup(panel, buffer.ToArray());
    }

    public void createProfile() {
        string[] words = playerName.text.Split(' ');
        player.name = words[0];
        //player.surname = (words.Length > 0) ? words[1] : "";

        string profileName = playerName.text.Replace(' ', '_');
        currentProfile = new Profile(profileName, sprites[imageCounter].texture.name);

        string[] origins = getChoice("origin", originList);
        string[] biographies = getChoice("biography", biographyList);
        string[] mentors = getChoice("mentor", mentorList);
        string[] perks = getChoice("perks", perksList);
        string[] manners = getChoice("manners", mannersList);

        foreach(string val in origins){
            origin.apply(player, val);
        }
        foreach(string val in biographies){
            biography.apply(player, val);
        }
        foreach(string val in mentors) {
            mentor.apply(player, val);
        }
        foreach(string val in perks) {
            perk.apply(player, val);
        }
        foreach(string val in manners) {
            manner.apply(player, val);
        }

        PlayerPrefs.SetString("Profile" + profileIndex, currentProfile.name);
        PlayerPrefs.SetString("Player", currentProfile.name);

        savePlayer();
    }

    string[] getChoice(string key, List<string> list) {
        ToggleGroup group = toggleGroups[key];
        int[] toggled = group.getToggled();
        List<string> res = new List<string>();
        foreach(int index in toggled) {
            res.Add(list[index]);
        }
        return res.ToArray();
    }

    public void startGame() {
        PlayerPrefs.SetString("Player", currentProfile.name);
    }

    /*
    void applyChoice(string[] list, AbstractBase applierClass) {
        foreach(string val in list) {
            applierClass.apply(player, val);
        }
    }*/
    /*
    GameObject[] AddChildren(List<string> collection, GameObject panel, bool visible) {
        int i = 0, count = collection.Count;
        GameObject[] children = new GameObject[count];
        while (i < count) {
            GameObject child = Instantiate(togglePrefab) as GameObject;
            child.transform.SetParent(panel.transform, false);
            Text toggleText = child.transform.FindChild("Label").GetComponent<Text>();
            toggleText.text = collection[i];
            children[i] = child;
            i++;
            //yield return null;//new WaitForSeconds(0.01f);
        }
        panel.gameObject.SetActive(visible);
        return children.Clone();
    }*/

    IEnumerator AnimateText(string strComplete) {
        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            text.text = str;

            yield return new WaitForSeconds(0.05F);
        }
    }

}
