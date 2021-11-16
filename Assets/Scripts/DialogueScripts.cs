using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScripts : MonoBehaviour
{
    DialogueManager manager;

    string[] speakers;
    string[] messages;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator initScript(Script scriptRef)
    {
        switch (scriptRef)
        {
            case Script.Intro:
                string[] speakerList = fetchSpeakers(Script.Intro);
                string[] messageList = fetchMessages(Script.Intro);

                for (int i = 0; i < messageList.Length; i++)
                {
                    StartCoroutine(manager.TypeText(speakerList[i], messageList[i]));
                    yield return new WaitWhile(() => manager.isTalking);

                }

                break;
            default:
                break;
        }
    }

    string[] fetchSpeakers (Script scriptRef)
    {
        switch (scriptRef)
        {
            case Script.Intro:
                speakers = new string[8];

                return speakers;
            default:
                return null;
            

        }
    }

    string[] fetchMessages (Script scriptRef)
    {
        switch (scriptRef)
        {
            case Script.Intro:
                messages = new string[8];

                return messages;
            default:
                return null;
        }
    }

}
