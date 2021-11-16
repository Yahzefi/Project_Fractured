using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScripts : MonoBehaviour
{

    string[] speakers;
    string[] messages;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public string[] fetchSpeakers (Script scriptRef)
    {
        switch (scriptRef)
        {
            case Script.Intro:
                speakers = new string[2];
                speakers.SetValue("Player", 0);
                speakers.SetValue("???", 1);
                return speakers;
            default:
                return null;
        }
    }

    public string[] fetchMessages (Script scriptRef)
    {
        switch (scriptRef)
        {
            case Script.Intro:
                messages = new string[2];
                messages.SetValue("God, I hate this place.", 0);
                messages.SetValue("Do you now?", 1);
                return messages;
            default:
                return null;
        }
    }

}
