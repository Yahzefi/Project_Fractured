using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScripts : MonoBehaviour
{

    string[] speakers;
    string[] messages;

    public string[] fetchSpeakers (Script scriptRef)
    {
        switch (scriptRef)
        {
// > LEVEL 01 INTRO
            case Script.Intro_01:

                speakers = new string[]
                {
                    "Player",
                    "???",

                };

                return speakers;
// <

// > LEVEL ?? INTRO
            case Script.Intro_02:

                speakers = new string[]
                {
                    "",
                    ""
                };

                return speakers;
// <

            default:
                return null;

        }
    }

    public string[] fetchMessages (Script scriptRef)
    {
        switch (scriptRef)
        {
// > LEVEL 01 INTRO
            case Script.Intro_01:

                messages = new string[]
                {
                    "God, I hate this place.",
                    "Do you now?",

                };

                return messages;
// <

// > LEVEL ?? INTRO
            case Script.Intro_02:

                messages = new string[]
                {
                    "",
                    "",

                };

                return messages;
// <

            default:
                return null;

        }
    }

}
