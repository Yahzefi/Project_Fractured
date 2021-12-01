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

//          ( [// > && // <] marks first speaker | [// >> && // <<] marks second speaker )          \\

            case Script.Intro00_01:
                
                speakers = new string[]
                {
                // >
                    "Player",
                    "Player",
                    "Player",
                    "Player",
                // <
                // >>
                    "???",
                // <<

                };

                return speakers;
// <

// > LEVEL ?? INTRO
            case Script.Intro00_02:

                speakers = new string[]
                {
                // >>
                    "???",
                    "???",
                // <<
                // >
                    "Player",
                // <

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
// > LEVEL 01 INTRO #1
            case Script.Intro00_01:

                messages = new string[]
                {
                // >
                    "...",
                    "Ah, yes...",
                    "It seems as though rumors of this place continue to carry across the continents.",
                    "What a shame...  I haven't had a proper challenge in decades.",
                // <
                // >>
                    "Pardon me.",
                // <<

                };

                return messages;
// <

// > LEVEL 01 INTRO #2
            case Script.Intro00_02:

                messages = new string[]
                {
                // >>
                    "I'd hate to interrupt your sulking, but I was curious about something.",
                    "Tell me, have you ever contemplated the weight of desire?",
                // <<
                // >
                    "*Tsk.*  Who the hell are you?",
                // <

                };

                return messages;
// <

            default:
                return null;

        }
    }

}
