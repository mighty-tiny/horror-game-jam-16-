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
    public GameObject[] DialogueManager;
    public GameObject BlackScreen;

    [Header("GameObjects")]
    public GameObject TeddyObj;
    public GameObject TeddyObjCave;
    public GameObject printObj;
    public GameObject Cap;

    public GameObject TeddyHeadScary;
    public GameObject TeddyHeadNorm;
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
    public GameObject[] Task;



    bool used;
    bool dialogued;

    [Header("Horror")]
    private bool horror;
    public GameObject bloodParticle;
    public AudioSource screamBoy;

    // Start is called before the first frame update
    void Awake()
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
        DetectTarget.bearDialogue = false;
        
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
        if (DetectTarget.bearDialogue && gathered)
        {
            Phase3();
        }
    }
    void Gathered()
    {
        Task[0].SetActive(false);
        Task[1].SetActive(true);
        gathered = true;
        Phase2();
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
            if (horror)
            {
                bloodParticle.SetActive(true);
                screamBoy.Play();
                
            }
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
        else if (index == 12)
        {
            speed = 0.1f;
        }
        else if (index == 14 && !used)
        {
            Phase1();
        }


        //PHASE3
        if (index == 3 && dialogued)
        {
            namecomponent.text = "Teddy";
            Teddy = true;
            You = false;
            TeddyHeadScary.SetActive(true);
            TeddyHeadNorm.SetActive(false);
        }


    }
    void Phase1()
    {
        Task[0].SetActive(true);

        Invoke("BlackOff", 2);
        DialogueWindow.SetActive(false);
        //StopAllCoroutines();
        textcomponent.text = lines[index];
        namecomponent.text = "You";
        BlackScreen.SetActive(false);
        clickable = false;
        //textcomponent.text = "[Sleeping]";
        Teddy = false;
        You = true;
        var i = Instantiate(Steps, transform.position, Quaternion.identity);
        BlackScreen.SetActive(true);
        printObj.SetActive(true);
        TeddyObj.SetActive(false);
        
        //Invoke("BlackOff", 2);
        Destroy(i, 2);
        used = true;
    }
    void Phase2()
    {
        
        DialogueManager[1].SetActive(true);
        DialogueManager[0].SetActive(false);
        clickable = true;
    }
    public void Phase3()
    {
        dialogued = true;
        DialogueManager[2].SetActive(true);
        DialogueManager[1].SetActive(false);
        clickable = true;
        DetectTarget.bearDialogue = false;

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
        else if (index == 14)
        {
            clickable = false;
            DialogueWindow.SetActive(false);
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

