using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textcomponent;
    public TextMeshProUGUI namecomponent;
    public string[] lines;
    [Header("Story")]
    public GameObject[] berries;
    private bool gathered;
    [Header("GameObjectsUI")]
    public GameObject DialogueWindow;
    public GameObject BlackScreen;

    [Header("GameObjects")]
    public GameObject TeddyObj;
    public GameObject TeddyObjCave;
    public GameObject printObj;
    public GameObject Cap;

    [Header("Preferences")]
    public float speed;
    private int index;
    [SerializeField] private GameObject Current;
    public bool Teddy;
    public bool You;
    public bool Soldier;
    public bool Skipable;
    private bool clickable;
    [Header("Sound")]
    private AudioSource audioSource;
    private DialogueAudioPlayer youDialogueAudioPlayer;
    private DialogueAudioPlayer teddyDialogueAudioPlayer;
    [SerializeField] private bool stopAudio;
    [SerializeField] private int FrequencyLevel = 5;
    [Header("SoundCllp")]
    [SerializeField] private AudioSource YouSoundClip;
    [SerializeField] private AudioSource TeddySoundClip;
    [Header("SoundStory")]
    public AudioSource OpenDoor;
    public AudioSource Knock;
    public AudioSource Steps;

    [Header("Tasks")]
    public GameObject Task1;
    // Start is called before the first frame update
    void Start()
    {
        textcomponent.text = string.Empty;
        audioSource = gameObject.AddComponent<AudioSource>();

        youDialogueAudioPlayer = new DialogueAudioPlayer(YouSoundClip.gameObject);
        teddyDialogueAudioPlayer = new DialogueAudioPlayer(TeddySoundClip.gameObject);

        You = true;
        speed = 0.1f;
        Teddy = false;
        StartDialogue();
        Skipable = true;
        
    }
    private void Update()
    {
        
        if (!berries[0].activeInHierarchy && !berries[1].activeInHierarchy && !berries[2].activeInHierarchy && !gathered)
        {
            Gathered();
        }
        
        if (textcomponent.text == lines[index] && index == 11)
        {
            
            NextLine();
        }

        else if (Input.GetMouseButtonDown(0) && clickable && index != 11)
        {
            if (textcomponent.text == lines[index] )
            {
                NextLine();
                
            }
            else
            {
                StopAllCoroutines();
                textcomponent.text = lines[index];
            }
        }
        else if (Input.GetMouseButtonDown(0) && index == 11)
        {
            speed /= 1.1f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Skipable)
        {
            Skip();
        }
    }
    void Gathered()
    {
        clickable = true;
        Task1.SetActive(false);
        textcomponent.text = "[Sleeping]";
        DialogueWindow.SetActive(true);
        index = 15;
        gathered = true;
    }
    void StartDialogue()
    {
        clickable = true;
        DialogueWindow.SetActive(true);
        index = 0;
        StartCoroutine(TypeLineYou());
    }
    IEnumerator TypeLineYou()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            PlayDialogueSound(textcomponent.text.Length);
            textcomponent.text += c;
            if (You)
            {
                youDialogueAudioPlayer.Play();
            }
            else if (Teddy)
            {
                teddyDialogueAudioPlayer.Play();
            }
            yield return new WaitForSeconds(speed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine(TypeLineYou());
        }
        else
        {
            DialogueWindow.SetActive(false);
        }


        //DIALOGUES

        //PHASE 1

        if (index == 5)
        {
            namecomponent.text = "You";
            Teddy = false;
            You = true;

        }
        if (index == 7)
        {
            namecomponent.text = "";
            Instantiate(OpenDoor, transform.position, Quaternion.identity);
            Current.SetActive(false);
            Teddy = false;
            You = false;

        }
        else if (index == 8 || index == 12)
        {
            namecomponent.text = "Teddy";
            Teddy = true;
            You = false;
        }
        else if (index == 4)
        {
            Instantiate(Knock, transform.position, Quaternion.identity);
            namecomponent.text = "";
            Teddy = false;
            You = false;
        }
        else if (index == 10)
        {
            namecomponent.text = "You";
            Teddy = false;
            You = true;
        }
        else if (index == 14)
        {
            Phase1();
        }


    }
    void Phase1()
    {
        Task1.SetActive(true);

        Invoke("BlackOff", 2);
        DialogueWindow.SetActive(false);
        StopAllCoroutines();
        textcomponent.text = lines[index];
        namecomponent.text = "Teddy";
        BlackScreen.SetActive(false);
        clickable = false;
        //textcomponent.text = "[Sleeping]";
        Teddy = true;
        You = false;
        var i = Instantiate(Steps, transform.position, Quaternion.identity);
        BlackScreen.SetActive(true);
        printObj.SetActive(true);
        TeddyObj.SetActive(false);
        Invoke("BlackOff", 2);
        Destroy(i, 2);
    }
    void Phase2()
    {

    }
    private void PlayDialogueSound(int currentDisplayedCharacterCount)
    {
        if (currentDisplayedCharacterCount % FrequencyLevel == 0)
        {
            if (stopAudio)
            {
                audioSource.Stop();
            }
        }
    }
    public void Skip()
    {
        if (index < 14)
        {
            Phase1();
            Skipable = false;

        }
    }
    public void BlackOff()
    {
        BlackScreen.SetActive(false);
        PlayerMovement.CantControl = false;
    }

    //IEnumerator FadeOut()
    //{
    //    for (float f = 1f; f >= 0f; f -= 0.5f)
    //    {
    //        Color c = Current.material.color;
    //        c.a = f;
    //        Current.material.color = c;
    //        yield return new WaitForSeconds(0.05f);

    //    }
    //}
    //void FadeIn()
    //{
    //    Color c = Current.material.color;
    //    c.a = 0f;
    //    Current.material.color = c;
    //}
}

