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
    Text dName;
    Text dText;

    Scene currentScene;

    [HideInInspector] public bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        scripts = GetComponent<DialogueScripts>();

        canvas = GameObject.Find("Dialogue");
        border = GameObject.Find("dBox_Border");
        textBox = GameObject.Find("dBox_Front");

        dName = border.GetComponentInChildren<Text>() ?? null;
        dText = textBox.GetComponentInChildren<Text>() ?? null;

        currentScene = SceneManager.GetActiveScene();

        InitConv();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitConv ()
    {
        switch (currentScene.name)
        {
            case "Level_01":
                StartCoroutine(scripts.initScript(Script.Intro));
                break;

            default:
                break;
        }
    }

    public IEnumerator TypeText (string speakerName, string message)
    {

        isTalking = true;

        dName.text = speakerName;

        char[] textArr = message.ToCharArray();

        foreach (char letter in textArr)
        {
            dText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        if (dText.text == message)
        {
            isTalking = false;
        }


        yield return null;
    }
}
