using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    DialogueScripts scripts;

    GameObject canvas;
    GameObject border;
    GameObject speakerBox_Left;
    GameObject speakerBox_Right;
    GameObject nameLine_Left;
    GameObject nameLine_Right;
    GameObject textBox;
    GameObject indicator;
    Text dName_Left;
    Text dName_Right;
    Text dText;

    Animator c_Animator;
    Animator p_Animator;

    [HideInInspector] public bool isTalking = false;
    bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Dialogue");
        border = GameObject.Find("dBox_Border");
        speakerBox_Left = GameObject.Find("SpeakerName_Left");
        speakerBox_Right = GameObject.Find("SpeakerName_Right");
        textBox = GameObject.Find("dBox_Front");
        indicator = GameObject.Find("indicator");

        scripts = canvas.GetComponent<DialogueScripts>();

        indicator.SetActive(indicator != null ? false : indicator);

        dName_Left = speakerBox_Left.GetComponent<Text>();
        dName_Right = speakerBox_Right.GetComponent<Text>();
        dName_Left.text = dName_Left != null ? "" : dName_Left.text;
        dName_Right.text = dName_Right != null ? "" : dName_Right.text;

        nameLine_Left = GameObject.Find("NameUnderline_Left");
        nameLine_Right = GameObject.Find("NameUnderline_Right");

        speakerBox_Left.SetActive(false);
        speakerBox_Right.SetActive(false);
        nameLine_Left.SetActive(false);
        nameLine_Right.SetActive(false);

        dText = textBox.GetComponentInChildren<Text>() ?? null;
        dText.text = dText != null ? "" : dText.text;

        canvas.SetActive(false);

        c_Animator = Camera.main.gameObject.GetComponent<Animator>();
        p_Animator = GameObject.Find("Player").GetComponent<Animator>();
        c_Animator.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isFlashing)
        {
            dName_Left.text = speakerBox_Left.activeInHierarchy ? "" : dName_Left.text;
            dName_Right.text = speakerBox_Right.activeInHierarchy ? "" : dName_Right.text;
            dText.text = "";
            isFlashing = false;
        }
    }

/*    void PlayIntroScene ()
    {
        switch (currentScene.name)
        {
            case "Level_01":
                StartCoroutine(initScript(Script.Intro_01));

                break;

            case "Level_02":
                //

                break;

            default:
                break;
        }
    }*/

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
            case Script.Intro_01:
                yield return new WaitForSeconds(1.0f);
                string[] speakerList = scripts.fetchSpeakers(Script.Intro_01);
                string[] messageList = scripts.fetchMessages(Script.Intro_01);

// > camera animations
                //

                canvas.SetActive(true);

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

        speakerBox_Left.SetActive(speakerName == "Player");
        nameLine_Left.SetActive(speakerName == "Player");

        speakerBox_Right.SetActive(speakerName != "Player");
        nameLine_Right.SetActive(speakerName != "Player");

        if (speakerBox_Left.activeInHierarchy)
        {
            dName_Left.text = speakerName;
            dName_Left.color = Color.blue;
            dText.color = new Color(225, 225, 225);
        }
        else if (speakerBox_Right.activeInHierarchy)
        {
            dName_Right.text = speakerName;
            dName_Right.color = Color.grey;
            dText.color = Color.red;
        }

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
