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
    [SerializeField] private GameObject Current;
    public bool Teddy;
    public bool You;
    public bool Soldier;
    [Header("Sound")]
    private AudioSource audioSource;
    [SerializeField] private bool stopAudio;
    [SerializeField] private int FrequencyLevel = 5;
    [Header("SoundCllp")]
    [SerializeField] private AudioSource YouSoundClip;
    [SerializeField] private AudioSource TeddySoundClip;
    // Start is called before the first frame update
    void Start()
    {
        textcomponent.text = string.Empty;
        audioSource = gameObject.AddComponent<AudioSource>();
        Teddy = false;
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
        StartCoroutine(TypeLineYou());
    }
    IEnumerator TypeLineYou()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            PlayDialogueSound(textcomponent.text.Length);
            textcomponent.text += c;
            
            audioSource.PlayOneShot(YouSoundClip.clip);
            yield return new WaitForSeconds(speed);
        }
    }
    IEnumerator TypeLineTeddy()
    {
        
        foreach (char c in lines[index].ToCharArray())
        {
            PlayDialogueSound(textcomponent.text.Length);
            textcomponent.text += c;

            audioSource.PlayOneShot(YouSoundClip.clip);
            yield return new WaitForSeconds(speed);
        }
    }
    IEnumerator TypeLineNone()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            PlayDialogueSound(textcomponent.text.Length);
            textcomponent.text += c;
            yield return new WaitForSeconds(speed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            if (You)
            {
                StartCoroutine(TypeLineYou());
            }
            else if (Teddy)
            {
                StartCoroutine(TypeLineTeddy());
            }
            else
            {
                StartCoroutine(TypeLineNone());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (index == 7)
        {
            OpenDoor.Play();
            Current.SetActive(false);
            namecomponent.text = "Teddy";
            Teddy = true;
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
