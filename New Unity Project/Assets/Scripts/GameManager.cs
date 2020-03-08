using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum ParameterType
    {
        Infected,
        Exposed
    }

    public enum StatType
    {
        HistInd,
        S2ID1,
        SFID1,
        S2ID2,
        SFID2,
        T2P,
        TFP,
        T2S,
        TFS,
    }

    public Image[] roomImages = new Image[5];

    public Text cutsceneText;
    public Text splashText;
    public Text spreadText;
    public Text minutesText;
    public Text daysText;
    public Text finalScore;
    public Text statsText;
    
    GameObject[] points = new GameObject[16];
    GameObject[] NPCs = new GameObject[80];

    public GameObject[] Prefabs = new GameObject[8];
    
    public Color activeColor;
    public Color friendColor;
    public Color infectedColor;
    public Color infectedFriend;

    public Color[] cliqueColors = new Color[4];
    public Color[] skinTones = new Color[5];

    public GameObject NPCHolder;
    public GameObject cutsceneObject;
    public GameObject cutsceneNoise;

    public GameObject splashScreen, mainMenu, instructionsScreen, HUD, miniMap;
    public GameObject winScreen, loseScreen;
    public GameObject statsWindow;
    public GameObject buttons, whoToldYouButton;

    public GameObject playerPrefab;
    GameObject player;

    int currentRoom = 0;

    int distributorID;
    int difficulty;
    int tmpID;

    public int spread, minutesLeft = 16, days = 0;

    bool dialogueRunning = false;
    bool won;

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        points = GameObject.FindGameObjectsWithTag("Respawn");
        StartCoroutine(StartHandler());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeRoom(int room)
    {
        roomImages[currentRoom].color = Color.white;
        roomImages[room].color = activeColor;
        currentRoom = room;
    }

    public void SpawnChars()
    {
        int i = 0;
        GameObject npc;

        foreach(GameObject point in points)
        {
            // If Male
            if(PlayerSheet.GetSex(i) == 1)
                npc = Prefabs[(int) point.GetComponent<Spawner>().thisOrientation];

            // If Female
            else
                npc = Prefabs[(int) point.GetComponent<Spawner>().thisOrientation + 4];

            GameObject clone = Instantiate(npc, point.transform.position, Quaternion.identity, NPCHolder.transform);
            NPCs[i] = clone;

            clone.GetComponentInChildren<TextMesh>().text = PlayerSheet.GetName(i).ToUpper();
            clone.GetComponentInChildren<MeshRenderer>().sortingLayerName = "Overlay";
            clone.GetComponentInChildren<MeshRenderer>().sortingOrder = 1;

            clone.GetComponent<NPC>().top.color = cliqueColors[PlayerSheet.GetTypeOne(i)-1];
            clone.GetComponent<NPC>().bottom.color = cliqueColors[(PlayerSheet.GetTypeTwo(i)-1)];
            clone.GetComponent<NPC>().skin.color = skinTones[PlayerSheet.GetIndex(i) % 5];
            clone.GetComponent<NPC>().SetID(i);

            if(PlayerSheet.GetIndex(i) >= 64 && PlayerSheet.GetParameter(i, (int) ParameterType.Exposed)) clone.GetComponentInChildren<TextMesh>().color = infectedFriend;
            else if (PlayerSheet.GetIndex(i) >= 64) clone.GetComponentInChildren<TextMesh>().color = friendColor;
            else if(PlayerSheet.GetParameter(i, (int) ParameterType.Exposed)) clone.GetComponentInChildren<TextMesh>().color = infectedColor;
            else clone.GetComponentInChildren<TextMesh>().color = Color.white;
            i++;
        }
    }

    public void DestroyChars()
    {
        GameObject[] chars = GameObject.FindGameObjectsWithTag("NPC");
        foreach(GameObject g in chars) g.GetComponent<NPC>().Die();
    }

    public IEnumerator Dialogue(int ID)
    {
        if(minutesLeft != 0)
        {
            minutesLeft -= 2;
            minutesText.text = "Minutes Left: " + minutesLeft.ToString();

            player.GetComponent<PlayerController>().AssertMovement(false);

            string sentence = PlayerSheet.GetName(ID) + ": ";
            cutsceneText.text = "";

            string statsString = "Stats: ";

            switch(PlayerSheet.GetIndex(ID)-64){
                case 0:
                    statsString += "INFP\nIntrovert\nIntuitive\nFeeling\nProspecting";
                    break;

                case 1:
                    statsString += "ENFP\nExtrovert\nIntuitive\nFeeling\nProspecting";
                    break;

                case 2:
                    statsString += "ESTP\nExtrovert\nObservant\nThinking\nProspecting";
                    break;

                case 3:
                    statsString += "ENTP\nExtrovert\nIntuitive\nThinking\nProspecting";
                    break;

                case 4:
                    statsString += "ENTJ\nExtrovert\nIntuitive\nThinking\nJudging";
                    break;

                case 5:
                    statsString += "ENFJ\nExtrovert\nIntuitive\nFeeling\nJudging";
                    break;

                case 6:
                    statsString += "INTP\nIntrovert\nIntuitive\nThinking\nProspecting";
                    break;

                case 7:
                    statsString += "INTJ\nIntrovert\nIntuitive\nThinking\nJudging";
                    break;

                case 8:
                    statsString += "ESFP\nExtrovert\nObservant\nFeeling\nProspecting";
                    break;

                case 9:
                    statsString += "ESFJ\nExtrovert\nObservant\nFeeling\nJudging";
                    break;

                case 10:
                    statsString += "ISFP\nIntrovert\nObservant\nFeeling\nProspecting";
                    break;

                case 11:
                    statsString += "ISFJ\nIntrovert\nObservant\nFeeling\nJudging";
                    break;

                case 12:
                    statsString += "ISTJ\nIntrovert\nObservant\nThinking\nJudging";
                    break;

                case 13:
                    statsString += "INFJ\nIntrovert\nIntuitive\nFeeling\nJudging";
                    break;

                case 14:
                    statsString += "ISTP\nIntrovert\nObservant\nThinking\nProspecting";
                    break;

                case 15:
                    statsString += "ESTJ\nExtrovert\nObservant\nThinking\nJudging";
                    break;
            }

            statsText.text = statsString;
            cutsceneObject.SetActive(true);

            // Normie
            if(PlayerSheet.GetIndex(ID) < 64)
            {
                cutsceneText.color = Color.white;

                if(PlayerSheet.GetParameter(ID, (int) ParameterType.Infected))
                {
                    PlayerSheet.SetParameter(ID, (int) ParameterType.Exposed, true);
                    NPCs[ID].GetComponentInChildren<TextMesh>().color = infectedColor;
                    sentence += "Haha, I heard you suck eggs!";
                }
                else
                {
                    int r = Random.Range(1, 100);
                    if(r >= 75) sentence += PlayerSheet.GetNormieDialogue(PlayerSheet.GetTypeOne(ID) - 1);
                    else sentence += PlayerSheet.GetNormieDialogue(PlayerSheet.GetTypeTwo(ID) - 1);
                }
            }
            else 
            {
                cutsceneText.color = Color.yellow;
                statsWindow.SetActive(true);

                // if infected and not the spreader
                if(PlayerSheet.GetParameter(ID, (int) ParameterType.Infected) && PlayerSheet.GetIndex(ID) != distributorID)
                {
                    NPCs[ID].GetComponentInChildren<TextMesh>().color = infectedFriend;
                    PlayerSheet.SetParameter(ID, (int) ParameterType.Exposed, true);
                    sentence += "Since we're friends, I want to let you know that someone told me about the eggs... But I promise I never told anyone!";
                }

                // if uninfected
                else
                    sentence += PlayerSheet.GetFriendDialogue(PlayerSheet.GetIndex(ID));
            }

            int sound = 1;

            foreach (char c in sentence)
            {
                cutsceneText.text += c;
                yield return new WaitForSeconds(.025f);

                //play sound every other character
                if(sound % 6 == 0)
                {
                    cutsceneNoise.SetActive(false);
                    cutsceneNoise.SetActive(true);
                }
                sound++;

            }

            yield return new WaitForSeconds(1.5f);
            cutsceneText.text = "";

            // friend options
            if(PlayerSheet.GetIndex(ID) >= 64 && minutesLeft != 0)
            {
                buttons.SetActive(true);
                if(PlayerSheet.GetParameter(ID, (int) ParameterType.Exposed)) whoToldYouButton.SetActive(true);
                tmpID = ID;
            }

            else {
                player.GetComponent<PlayerController>().AssertMovement(true);
                cutsceneObject.SetActive(false);
            }
        }
        dialogueRunning = false;
        yield return null;
    }

    public void StartDialogue(int ID){
        if(!dialogueRunning)
        {
            dialogueRunning = true;
            StartCoroutine(Dialogue(ID));
        }
    }

    IEnumerator StartGame(){

        miniMap.SetActive(true);
        HUD.SetActive(true);

        float start = Time.time;
        while(true)
        {
            float percent = (Time.time - start)*2;
            mainMenu.GetComponent<CanvasGroup>().alpha = 1-percent;
            if(percent >= 1) break;
            yield return new WaitForEndOfFrame();
        }
        mainMenu.SetActive(false);

        PlayerSheet.Initialize();
        distributorID = PlayerSheet.GetDistributor(difficulty);
        Debug.Log(PlayerSheet.GetName(distributorID) + " is the distributor.");
        PlayerSheet.SetParameter(distributorID, (int) ParameterType.Infected, true);
        player.transform.position = Vector2.zero;

        minutesLeft = 16;
        days = 0;
        spreadText.text = "Spread: " + spread.ToString();
        minutesText.text = "Minutes Left: " + minutesLeft.ToString();
        daysText.text = "Days: " + days.ToString();

        StartCoroutine(GameHandler());

        yield return null;
    }

    IEnumerator GameHandler()
    {
        int i = 0;
        while(true)
        {
            // Respawn NPC s
            PlayerSheet.Shuffle();
            SpawnChars();

            // Reset spread and minutes
            spread = 0;
            for(int j = 0; j < 80; j++)
            {
                if(PlayerSheet.GetParameter(j, (int) ParameterType.Infected) == true) spread++;
            }
            spreadText.text = "Spread: " + spread.ToString() + "/64";
            minutesLeft = 16;
            minutesText.text = "Minutes Left: " + minutesLeft.ToString();

            // wait for end of turn
            while(!Input.GetKeyDown(KeyCode.N))
            {
                if(spread == 1)
                {
                    i = 0;
                    days = 0;
                    break;
                }
                yield return null;
            }

            //Reset for next turn
            DestroyChars();

            // stop if the whole school knows
            if(spread >= 64 || won) break;

            // Infector Spreads rumor

            // spread rumor
            for(int j = 0; j < 80; j++)
            {
                // if in position to spread
                int pair = PlayerSheet.CheckPair(j);
                if(pair >= 0 && (PlayerSheet.GetParameter(j, (int) ParameterType.Infected)) == true)
                {
                    int num_spread = PlayerSheet.GetSpreadToday(i);
                    int r = Random.Range(1, 100);

                    // if rumor can be spread
                    bool isThinking = ((PlayerSheet.GetIndex(j) == 66 || PlayerSheet.GetIndex(j) == 67 || PlayerSheet.GetIndex(j) == 68 || PlayerSheet.GetIndex(j) == 70 || PlayerSheet.GetIndex(j) == 71 || 
                    PlayerSheet.GetIndex(j) == 76 || PlayerSheet.GetIndex(j) == 78 || PlayerSheet.GetIndex(j) == 79 || PlayerSheet.GetIndex(j) == distributorID) || PlayerSheet.GetIndex(j) < 64);

                    if(((difficulty == 0 && num_spread == 0 && r <= 40) || (difficulty == 1) && r <= 80) && PlayerSheet.GetParameter(pair, (int) ParameterType.Infected) == false && isThinking)
                    {
                        // spread rumor
                        PlayerSheet.SetSpreadToday(j, num_spread + 1);
                        PlayerSheet.SetParameter(pair, (int) ParameterType.Infected, true);

                        if(PlayerSheet.GetIndex(pair) >= 64) PlayerSheet.SetRevealer(pair, j);

                        // find room
                        int room = (int) Mathf.Floor(j/16);
                        Debug.Log(PlayerSheet.GetName(j) + " spread to " + PlayerSheet.GetName(pair) + " in room " + room.ToString());

                        for(int k = 16 * room; k < 16 * (room + 1); k++)
                        {
                            bool sharespreader1_1 = (PlayerSheet.GetTypeOne(k) == PlayerSheet.GetTypeOne(j));
                            bool sharespreader1_2 = (PlayerSheet.GetTypeOne(k) == PlayerSheet.GetTypeTwo(j));
                            bool sharespreader2_1 = (PlayerSheet.GetTypeTwo(k) == PlayerSheet.GetTypeOne(j));
                            bool sharespreader2_2 = (PlayerSheet.GetTypeTwo(k) == PlayerSheet.GetTypeTwo(j));
                            bool sharesinfected1_1 = (PlayerSheet.GetTypeOne(k) == PlayerSheet.GetTypeOne(pair));
                            bool sharesinfected1_2 = (PlayerSheet.GetTypeOne(k) == PlayerSheet.GetTypeTwo(pair));
                            bool sharesinfected2_1 = (PlayerSheet.GetTypeTwo(k) == PlayerSheet.GetTypeOne(pair));
                            bool sharesinfected2_2 = (PlayerSheet.GetTypeTwo(k) == PlayerSheet.GetTypeTwo(pair));

                            // if it is a friend
                            if(PlayerSheet.GetIndex(k) > 64 && k != j && k != pair)
                            {

                                // if they share clique with spreader
                                if(sharespreader1_1 || sharespreader1_2 || sharespreader2_1 || sharespreader2_2)
                                {
                                    int observeChance = 20;
                                    int trendChance = 20;
                                    int friend = (PlayerSheet.GetIndex(k) - 64);

                                    // if intuitive
                                    if (friend == 0 || friend == 1 || friend == 3 || friend == 4 || friend == 5 || friend == 6 || friend == 7 || friend == 13)
                                        trendChance = 40;

                                    // if observant
                                    else
                                        observeChance = 40;

                                    int rObserve = Random.Range(1, 100);
                                    int rTrend = Random.Range(1, 100);
                                    
                                    // if observed, enter into stats
                                    if(rObserve < observeChance)
                                    {
                                        int mod = (PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.HistInd) % 2) == 0) ? 0 : 2;
                                        PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.S2ID1) + mod, pair);
                                        PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.SFID1) + mod, j);

                                        Debug.Log(PlayerSheet.GetName(k) + " observed " + PlayerSheet.GetName(j) + " to: " + PlayerSheet.GetName(pair));
                                        Debug.Log(rObserve);
                                    }

                                    // if trend noticed
                                    if(rTrend < trendChance)
                                    {
                                        if(sharespreader1_1) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.T2P), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.T2P)) + 1);
                                        if(sharespreader1_2) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.T2P), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.T2P)) + 1);
                                        if(sharespreader2_1) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.T2S), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.T2S)) + 1);
                                        if(sharespreader2_2) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.T2S), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.T2S)) + 1);
                                        if(sharesinfected1_1) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.TFP), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.TFP)) + 1);
                                        if(sharesinfected1_2) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.TFP), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.TFP)) + 1);
                                        if(sharesinfected2_1) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.TFS), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.TFS)) + 1);
                                        if(sharesinfected2_2) PlayerSheet.SetSpreadStat(k, Stat2Int(StatType.TFS), PlayerSheet.GetSpreadStat(k, Stat2Int(StatType.TFS)) + 1);

                                        Debug.Log(PlayerSheet.GetName(k) + " noticed a Trend!");
                                        Debug.Log(rTrend);
                                    }
                                }
                            }

                            // add statistics
                        //PlayerSheet.SetSpreadToday(j, (int) SpreadType.ID1, pair);
                        }
                    }
                }
            }
            // new day
            if((i % 3) == 0)
            {
                for(int j = 0; j < 80; j++) PlayerSheet.SetSpreadToday(j, 0);
                days++;
                daysText.text = "Days: " + days.ToString();
            } 
            i++;

            yield return new WaitForEndOfFrame();
        }

        // Deconstruct game
        DestroyChars();
        miniMap.SetActive(false);
        HUD.SetActive(false);

        // start result manager
        StartCoroutine(EndGame());
    }

    IEnumerator StartHandler()
    {
        float start = Time.time;
        while(true)
        {
            float percent = Time.time - start;
            splashText.GetComponent<CanvasGroup>().alpha = percent;
            if(percent >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        mainMenu.SetActive(true);
        yield return new WaitForSeconds(1);

        start = Time.time;
        while(true)
        {
            float percent = Time.time - start;
            splashScreen.GetComponent<CanvasGroup>().alpha = 1-percent;
            if(percent >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        splashScreen.SetActive(false);
        yield return null;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartEasy()
    {
        difficulty = 0;
        StartCoroutine(StartGame());
    }

    public void StartHard()
    {
        difficulty = 1;
        StartCoroutine(StartGame());
    }

    int Stat2Int(StatType s){
        return (int) s;
    }

    IEnumerator FriendSay(string message, int another)
    {
        int sound = 1;
        buttons.SetActive(false);
        whoToldYouButton.SetActive(false);

        foreach (char c in message)
        {
            cutsceneText.text += c;
            yield return new WaitForSeconds(.025f);

            //play sound every other character
            if(sound % 6 == 0)
            {
                cutsceneNoise.SetActive(false);
                cutsceneNoise.SetActive(true);
            }
            sound++;

        }
        yield return new WaitForSeconds(2.5f);

        if(another == 1) StartFriendSay(4);
        if(another == 2) 
        {
            cutsceneText.text = "";
            cutsceneObject.SetActive(false);
            statsWindow.SetActive(false);
            player.GetComponent<PlayerController>().AssertMovement(true);
            StartCoroutine(EndGame());
        }
        else
        {
            cutsceneText.text = "";
            cutsceneObject.SetActive(false);
            statsWindow.SetActive(false);
            player.GetComponent<PlayerController>().AssertMovement(true);
        }

    }

    public void StartFriendSay(int choice)
    {
        minutesLeft -= 2;
        minutesText.text = "Minutes Left: " + minutesLeft.ToString();

        string message = PlayerSheet.GetName(tmpID) + ": ";
        int sendID;
        int recID;

        switch(choice)
        {
            case 0:
                int toPrimary = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.T2P));
                int fromPrimary = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.TFP));
                int toSecondary = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.T2S));
                int fromSecondary = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.TFS));

                bool noticeToPrimary = (((toPrimary - toSecondary > 0)) && (toPrimary - fromSecondary > 0));
                bool noticeFromPrimary = (((fromPrimary - toSecondary > 0)) && (fromPrimary - fromSecondary > 0));
                bool noticeToSecondary = ((toSecondary - toPrimary > 0) && ((toSecondary - fromPrimary > 0)));
                bool noticeFromSecondary = ((fromSecondary - toPrimary > 0) && ((fromSecondary - fromPrimary > 0)));
                
                string[] typename = {"Preps", "Jocks", "Artists", "Nerds"};

                if(noticeToPrimary || noticeFromPrimary) message += "I've noticed a lot of " + typename[PlayerSheet.GetTypeOne(tmpID)-1] + " whispering amongst themselves.";
                else if(noticeToPrimary || noticeFromPrimary) message += "I've noticed a lot of " + typename[PlayerSheet.GetTypeTwo(tmpID)-1] + " whispering amongst themselves.";
                else if(PlayerSheet.GetTypeOne(tmpID) == PlayerSheet.GetTypeTwo(tmpID) && toPrimary > 3) message += "I've noticed a lot of " + typename[PlayerSheet.GetTypeOne(tmpID)-2] + " whispering amongst themselves.";
                else message += "I haven't noticed anything in particular.";

                StartCoroutine(FriendSay(message, 0));
                break;

            case 1:
                sendID = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.SFID1));
                recID = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.S2ID1));

                if(sendID != 0 && recID != 0) message += "I noticed " + PlayerSheet.GetName(sendID) + " whispering to " + PlayerSheet.GetName(recID) + " very suspiciously.";
                else message += "Nah, I haven't seen anything strange. Why?";
                
                int another = (PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.SFID2)) != 0) && (PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.S2ID2)) != 0) ? 1 : 0;
                StartCoroutine(FriendSay(message, another));
                break;

            case 2:
                message += PlayerSheet.GetRevealer(tmpID) + " told me.";
                StartCoroutine(FriendSay(message, 0));
                break;

            case 3:
                if (PlayerSheet.GetIndex(tmpID) == distributorID)
                {
                    won = true;
                    message += "Aww you caught me. I'm so sorry, I just think it's so hilarious that you suck eggs! :(";
                }
                else message += "How dare you accuse me! You're not a very good friend.";
                StartCoroutine(FriendSay(message, 2));
                break;

            case 4:
                sendID = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.SFID2));
                recID = PlayerSheet.GetSpreadStat(tmpID, Stat2Int(StatType.S2ID2));
                message += "I also noticed " + PlayerSheet.GetName(sendID) + " whispering to " + PlayerSheet.GetName(recID) + " very suspiciously.";
                break;
        }
    }

    IEnumerator EndGame()
    {
        GameObject s = (won) ? winScreen : loseScreen;
        if(won) finalScore.text = "Spread to " + spread.ToString() + " people.";
        won = false;
        s.GetComponent<CanvasGroup>().alpha = 0;
        s.SetActive(true);

        float start = Time.time;
        while(true)
        {
            float percent = (Time.time - start)*2;
            s.GetComponent<CanvasGroup>().alpha = percent;
            if(percent >= 1) break;
            yield return new WaitForEndOfFrame();
        }

        
        mainMenu.GetComponent<CanvasGroup>().alpha = 1;
        mainMenu.SetActive(true);
        DestroyChars();
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(0);
    }

    public void StartInstructions()
    {
        instructionsScreen.SetActive(true);
        StartCoroutine(instructionsScreen.GetComponent<InstructionsController>().RunPage());
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
