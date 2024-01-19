using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] private Image Current;
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
        if (index == 7)
        {
            OpenDoor.Play();
            FadeIn();
            namecomponent.text = "Teddy";
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
    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0f; f -= 0.5f)
        {
            Color c = Current.material.color;
            c.a = f;
            Current.material.color = c;
            yield return new WaitForSeconds(0.05f);

        }
    }
    void FadeIn()
    {
        Color c = Current.material.color;
        c.a = 0f;
        Current.material.color = c;
    }
}
