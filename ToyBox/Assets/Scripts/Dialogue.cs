using System.Collections;
using System.Collections.Generic;
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
    public bool gathered;
    public AudioSource OpenDoor;
    [Header("Preferences")]
    public float speed;
    private int index;
    [Header("Sound")]
    private AudioSource audioSource;
    [SerializeField] private AudioSource dialogueTypingSoundClip;
    [SerializeField] private bool stopAudio;
    [SerializeField] private int FrequencyLevel = 5;
    // Start is called before the first frame update
    void Start()
    {
        textcomponent.text = string.Empty;
        audioSource = gameObject.AddComponent<AudioSource>();
        StartDialogue();
    }
    private void Update()
    {
        if (!berries[0].activeInHierarchy && !berries[1].activeInHierarchy && !berries[2].activeInHierarchy)
        {
            gathered = true;
        }
        if (gathered)
        {
            namecomponent.text = "Teddy";
            textcomponent.text = "[Sleeping]";
            gathered = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (textcomponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textcomponent.text = lines[index];
            }
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            PlayDialogueSound(textcomponent.text.Length);
            textcomponent.text += c;
            
            audioSource.PlayOneShot(dialogueTypingSoundClip.clip);
            yield return new WaitForSeconds(speed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
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
}
