using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public Text[] Texts;
    public float typingSpeed;
    public string[] Messages;
    public AudioSource AudioClip;
    public GameObject Confetti;

    private string tempMessage;
    private int FontIndex;


    private void OnEnable()
    {
        Debug.Log("I am enabled now");
        SetRandomContent();
    }

    public void SetRandomContent()
    {
        Debug.Log("Starting Random Content");
        foreach (var v in Texts)
        {
            v.gameObject.SetActive(false);
        }

        tempMessage = Messages[Random.Range(0, Messages.Length)];  // Ensure valid range
        Debug.Log("Selected Message: " + tempMessage);

        if (Random.Range(0, 100) <= 50)
        {
            FontIndex = 0;
        }
        else
        {
            FontIndex = 1;
        }

        Debug.Log("Selected Font Index: " + FontIndex);

        Texts[FontIndex].gameObject.SetActive(true); // Activate the selected Text GameObject
        Texts[FontIndex].text = ""; // Clear text
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        yield return new WaitForSeconds(0.2f);
        AudioClip.loop = true;
        AudioClip.Play();
        
        Debug.Log("Starting RevealText Coroutine");
        foreach (char letter in tempMessage.ToCharArray())
        {
            Texts[FontIndex].text += letter;
            Debug.Log("Updated Text: " + Texts[FontIndex].text);
            yield return new WaitForSeconds(typingSpeed);
        }
        Debug.Log("Completed RevealText Coroutine");
        AudioClip.Stop();
    }
}
