using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    DialogueScripts scripts;

    GameObject canvas;
    GameObject border;
    GameObject textBox;
    GameObject indicator;
    Text dName;
    Text dText;

    Scene currentScene;

    [HideInInspector] public bool isTalking = false;
    bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Dialogue");
        border = GameObject.Find("dBox_Border");
        textBox = GameObject.Find("dBox_Front");
        indicator = GameObject.Find("indicator");

        scripts = canvas.GetComponent<DialogueScripts>();

        indicator.SetActive(indicator != null ? false : indicator);

        dName = border.GetComponentInChildren<Text>() ?? null;
        dText = textBox.GetComponentInChildren<Text>() ?? null;
        dName.text = dName != null ? "" : dName.text;
        dText.text = dText != null ? "" : dText.text;

        canvas.SetActive(false);

        currentScene = SceneManager.GetActiveScene();

         PlayIntroScene();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isFlashing)
        {
            dName.text = "";
            dText.text = "";
            isFlashing = false;
        }
    }

    void PlayIntroScene ()
    {
        switch (currentScene.name)
        {
            case "Level_01":
                StartCoroutine(initScript(Script.Intro));
                break;

            default:
                break;
        }
    }

    void StartInteraction(Script scriptRef)
    {
        switch (scriptRef)
        {
            case Script.InteractionWithNPC01:
                break;
            default:
                break;
        }
    }

    public IEnumerator initScript(Script scriptRef)
    {
        switch (scriptRef)
        {
            case Script.Intro:
                yield return new WaitForSeconds(1.0f);
                canvas.SetActive(true);
                string[] speakerList = scripts.fetchSpeakers(Script.Intro);
                string[] messageList = scripts.fetchMessages(Script.Intro);
                Debug.Log(isTalking);
                for (int i = 0; i < messageList.Length; i++)
                {
                    StartCoroutine(TypeText(speakerList[i], messageList[i]));
                    yield return new WaitWhile(() => isTalking);

                }

                canvas.SetActive(false);

                break;

            default:
                break;
        }
    }

    public IEnumerator TypeText (string speakerName, string message)
    {

        isTalking = true;

        dName.text = speakerName;
        dName.color = speakerName == "Player" ? Color.blue : speakerName == "???" ? Color.red : dName.color;

        char[] textArr = message.ToCharArray();

        foreach (char letter in textArr)
        {
            dText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        if (dText.text == message)
        {
            isFlashing = true;
        }

        while (isFlashing)
        {
            indicator.SetActive(true);
            yield return new WaitForSeconds(isFlashing ? 0.5f : 0);
            indicator.SetActive(false);
            yield return new WaitForSeconds(isFlashing ? 0.5f : 0);
        }

        yield return new WaitWhile(() => isFlashing);

        isTalking = false;

        yield return null;
    }
}
