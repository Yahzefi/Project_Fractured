using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScripts : MonoBehaviour
{

    string[] speakers;
    string[] messages;

// !fs
    public string[] fetchSpeakers(int levelNum, int sceneNum, int sectionNum)
    {
// !L1_s
        if (levelNum == 1)
        {
            if (sceneNum == 1) // Cutscene.S01_01
            {
                switch (sectionNum)
                {
                    case 1:

                        speakers = new string[]
                        {
                        "Player",
                        "Player",
                        "Player",
                        "Player",
                        "Player",
                        "???"
                        };

                        return speakers;

                    case 2:

                        speakers = new string[]
                        {
                        "???"
                        };

                        return speakers;

                    case 3:

                        speakers = new string[]
                        {
                        "???",
                        "Player",
                        "???",
                        "Player",
                        "???",
                        "Player"
                        };

                        return speakers;

                    case 4:

                        speakers = new string[]
                        {
                        "Player",
                        "Player",
                        "Player",
                        "Player",
                        "???",
                        "???",
                        "???",
                        "Player",
                        "???",
                        "???",
                        "Player"
                        };

                        return speakers;

                    case 5:

                        speakers = new string[]
                        {
                        "Player",
                        "Player",
                        "Player",
                        "Player",
                        "Player",
                        "Player"
                        };

                        return speakers;

                    default:
                        return null;

                }
            }
            else if (sceneNum == 2)
            {
                switch (sectionNum)
                {
                    case 1:

                        speakers = new string[]
                        {
                            "...",
                            "...",
                            "..."
                        };

                        return speakers;

                    default:
                        return null;

                }
            }
            else if (sceneNum == 3)
            {
                switch (sectionNum)
                {
                    case 1:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    case 2:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    case 3:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    case 4:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    case 5:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    case 6:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    case 7:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    case 8:

                        speakers = new string[]
                        {

                        };

                        return speakers;

                    default:
                        return null;

                }
            }
            else
            {
                return null;
            }
            
        }
// !L2_s
        else if (levelNum == 2)
        {
            if (sceneNum == 1)
            {
                switch (sectionNum)
                {
                    case 1:

                        speakers = new string[]
                        {
                            ""
                        };

                        return speakers;

                    default:
                        return null;

                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }

    }

// !fm
    public string[] fetchMessages(int levelNum, int sceneNum, int sectionNum)
    {

        /* 
            [// > && // <] = first speaker 
            [// >> && // <<] = second speaker
        */

// !L1_m
        if (levelNum == 1)
        {
    // !S1_m
            if (sceneNum == 1)
            {
                switch (sectionNum)
                {
                    case 1:

                        messages = new string[]
                        {
                    // > Player
                        "...",
                        "Ah, yes...",
                        "It seems as though rumors of this place continue to carry across the continents.",
                        "There's only been a small handful stupid enough to come since I arrived.",
                        "In the end they were nothing but peons high off arbitrary reputation.",
                    // <
                    // >> ???
                        "Pardon me.",
                    // <<
                        };

                        return messages;

                    case 2:

                        messages = new string[]
                        {
                    // >> ???
                        "I'd hate to interrupt your sulking, but I was curious about something.",
                    // <<
                        };

                        return messages;

                    case 3:

                        messages = new string[]
                        {
                    // >> ???
                        "Tell me, have you ever contemplated the weight of desire and the consequences that follow?",
                    // <<
                    // > Player
                        "Oh great...  My first visitor in years is philosophical.",
                    // <
                    // >> ???
                        "Now, now.  This'll only take a moment.  Tell me, have you ever heard the tale of Achilles?",
                    // <<
                    // > Player
                        "What of that mortal fool?  Are you here to fight or to continue running your mouth?",
                    // <
                    // >> ???
                        "Hm...  I see.  It really is quite unfortunate.  How one could be so aware of one's own weakness and yet still fall on the battlefield.",
                    // <<
                    // > Player
                        "What are you-"
                    // <
                        };

                        return messages;

                    case 4:

                        messages = new string[]
                        {
                    // > Player
                        "Y-You bastard...  What'd you just...  Wait a second, no you didn't...",
                        "I don't know how, but you took it didn't you?!  Heh...",
                        "Haha...  HAHAHAHAHAHAHA!  ",
                        "Are you suicidal or something? Have you no idea who you're dealing with right now?!",
                    // <
                    // >> ???
                        "If you know what I now hold then surely you already know the answer.",
                        "What a shame...  Your preposterous pride has bloated your ego much larger than I'd predicted.",
                        "It truly pains me to witness such an abhorrent sight.",
                    // <<
                    // > Player
                        "Predicted?  You don't know SHIT about me!",
                    // <
                    // >> ???
                        "Indeed...  I regret that I must agree with such a conclusion; however, that is of little importance now.  I've recovered the artifact.",
                        "Farewell.",
                    // <<
                    // > Player
                        "You think I'll just let you-"
                    // <
                        };

                        return messages;

                    case 5:

                        messages = new string[]
                        {
                    // > Player
                        "DAMMIT!!!",
                        "How did this happen..?  It's hard enough to believe he knew about the artifact, but for him to just... take it. ",
                        "He acted like I was nothing, as if i was a mere child.",
                        "He doesn't know...  Nobody could possibly understand what I went through to find it.",
                        "Heh...  Well whaddaya know!  Looks like I finally got an excuse to leave this shithole.",
                        "Let's milk some fun outta this, shall we?"
                    // <
                        };

                        return messages;

                    default:
                        return null;

                }
            }
            else if (sceneNum == 2)
            {
                switch (sectionNum)
                {
                    case 1:

                        messages = new string[]
{
                        // > Player (Thoughts)
                            "I didn't want to admit it, but I'm not oblivious.  I've grown weaker...",
                            "After all this time spent as a prideful top-class warrior has been unrightfully ripped away from me.",
                            "Something about this isn't right, though.  I wasn't this weak before I found that damn artifact, so what gives?"
    // <
};

                        return messages;

                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
            
        }
// !L2_m
        else if (levelNum == 2)
        {
            if (sceneNum == 1)
            {
                switch (sectionNum)
                {
                    case 1:

                        messages = new string[]
                        {
                            ""
                        };

                        return messages;

                    default:
                        return null;

                }
            }
            else
            {
                return null;
            }
        }
        // !L3_m
        else if (levelNum == 3)
        {
            if (sceneNum == 1)
            {
                switch (sectionNum)
                {
                    case 1:

                        messages = new string[]
                        {
                            ""
                        };

                        return messages;

                    default:
                        return null;

                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }


}
