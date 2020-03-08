using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSheet
{

    private static string[] names = new string[80]
    {
        "Carl A",
        "Ruth A",
        "Mike B",
        "Dawn B",
        "Zack C",
        "Rose C",
        "John P",
        "Jojo J",
        "Paul B",
        "Suzy Q",
        "Rick S",
        "Ally D",
        "Sean G",
        "Aren E",
        "Bert H",
        "Judy F",
        "Jack I",
        "Kate G",
        "Clay J",
        "Jana H",
        "Cole K",
        "Lisa I",
        "Matt L",
        "Elle L",
        "Nate M",
        "Sara M",
        "Ryan G",
        "Kacy N",
        "Noah O",
        "Lucy O",
        "Wade P",
        "Abby P",
        "Seth Q",
        "Beth S",
        "Gage R",
        "Asia R",
        "Phil S",
        "Cara T",
        "Reed T",
        "Lily U",
        "Kurk U",
        "Maya A",
        "Andy V",
        "Hana V",
        "Tony W",
        "Jade W",
        "Mark Z",
        "Gwyn S",
        "Adam I",
        "Gigi G",
        "Cody J",
        "Kira L",
        "Earl S",
        "Nora F",
        "Eric K",
        "Macy P",
        "Jake B",
        "Demi L",
        "Kurt K",
        "Aria G",
        "Trey M",
        "Rita C",
        "Zane E",
        "Skye B",
        "Lane G",
        "Emma M",
        "Will C",
        "Lexi J",
        "Chad A",
        "Zoey B",
        "Luke B",
        "Anna K",
        "Josh D",
        "Cass E",
        "Alex F",
        "Tori S",
        "Nick E",
        "Mary U",
        "Drew H",
        "Leah B"
    };

    private static int[] pairs = new int[80]
    {
        1,
        0,
        -1,
        -1,
        -1,
        6,
        5,
        8,
        7,
        -1,
        11,
        10,
        -1,
        -1,
        -1,
        -1,
        17,
        16,
        -1,
        -1,
        21,
        20,
        23,
        22,
        -1,
        -1,
        -1,
        -1,
        29,
        28,
        -1,
        -1,
        33,
        32,
        -1,
        -1,
        -1,
        -1,
        -1,
        40,
        39,
        42,
        41,
        44,
        43,
        -1,
        -1,
        -1,
        49,
        48,
        -1,
        -1,
        -1,
        54,
        53,
        -1,
        -1,
        -1,
        59,
        58,
        61,
        60,
        -1,
        -1,
        -1,
        -1,
        67,
        66,
        -1,
        -1,
        -1,
        72,
        71,
        74,
        73,
        -1,
        -1,
        -1,
        79,
        78
    };

    private static int[,] difficultyIndex = new int[2, 8]
    {
        {
            6,
            7,
            10,
            11,
            12,
            13,
            14,
            15
        },
        {
            0,
            1,
            2,
            3,
            4,
            5,
            8,
            9
        },
    };

    private static string[,] normieDialogue = new string[4, 8]
    {
        {
            "Hi!",
            "Hey, how are you? :)",
            "Omg what's up???",
            "What are you up to?",
            "Have you heard the new Drake album? He's definitely in my top 5 list.",
            "I can't wait to see the next Marvel movie!",
            "Good to see you!",
            "Have you seen the new iPhone? I'm totally getting one.",
            
        },
        {
            "Do you even lift, bro?",
            "I went running before class, but now I have a shin splint...",
            "Pain is weakness leaving the body.",
            "Haha, I bet I could bench press you.",
            "We're gonna win the next football game, I can feel it.",
            "*Sips protein shake*",
            "Please don't eat bread around me, I'm cutting right now.",
            "Did you see me at the last game? Yeah, I was the one scoring all the points.",
            
        },
        {
            "You have a very bold face.",
            "Have you noticed the school's interior design? It's... lacking to say the least.",
            "I've been learning Fur Elise, do you wanna hear me practice?",
            "I want to write a book, but every time I start writing I get distracted. :(",
            "I should've gotten the lead role in the play. The director is just biased.",
            "*Continues jotting in notebook*",
            "You're wearing  t h a t ?  Ok...",
            "The marching band is going to get tons of awards this year!",
            
        },
        {
            "Do you need help with the homework?",
            "Have you played Warren's new game? It's a pretty neat Social Simulator!",
            "Make this quick, I have to study.",
            "What's your favorite anime?",
            "Excuse me, I'm reading.",
            "I love physics!",
            "I've been learning C# so I can make my own game one day!",
            "Fyodor Dostoyevsky was quite the thinker...",
            
        },  
    };

    private static string[,] friendDialogue = new string[16 , 3]
    {
        {
            "Bro you look so great today! :)",
            "Do you wanna hang out this evening?",
            "I hope you're having a good day! :)",
            
        }, 
        {
            "Hey!!!",
            "How's your day going? :)",
            "It's been too long since we've hung out!!!",
            
        }, 
        {
            "I'm thinking about getting a job. I want to invest.",
            "You look uncomfortable, is everything okay?",
            "Do you wanna buy some chocolate? It's for a fundraiser.",
            
        }, 
        {
            "I can't wait for our debate competition.",
            "Do you think I'd make a good Senator?",
            "Warren Wheeler is the greatest game developer of all time. Change my mind.",
            
        }, 
        {
            "Hey bud, tell me what's up!",
            "Come here, I wanna talk to you!",
            "WHAT THE HECK IS UP BRO?!?",
            
        }, 
        {
            "What's up? The ceiling hahaha!",
            "Have you met Joe? *stifles laughter*",
            "Knock knock!",
            
        }, 
        {
            "I think I'll run for student government next year.",
            "I get the feeling you're worried about something.",
            "Our school is so clique-y. I might transfer...",
            
        }, 
        {
            "The city needs a new mall. It seems like everyone wears the same clothes every day...",
            "This school has bad flow. I mean, you have to go through the cafeteria to get to the gym..",
            "I told the coach about a new play I thought of. We might run it this Friday!",
            
        }, 
        {
            "Let's skip class today, I wanna show you something ;)",
            "I can't wait for dance practice this weekend.",
            "I can't wait for the next choir concert. I've been practicing my butt off!",
            
        }, 
        {
            "Need some advice?",
            "I hope you're feeling ok! :)",
            "How has your family been doing?",
            
        }, 
        {
            "I think I just decided to go on a day trip this weekend.",
            "Let's go to a concert soon!",
            "You promised we'd go hiking soon. You better not lie to me!",
            
        }, 
        {
            "If you're having trouble with the lesson we can study together later.",
            "If I say another jock bullying someone, I'm going to lose it.",
            "Our school needs to start recycling. But I guess we should get trash cans first...",
            
        }, 
        {
            "Everyone makes grades such a big deal. As long as you get B's you'll be fine.",
            "I'll never understand spending money on clothes. You could do so much more with the money.",
            "Do you wanna split up the homework problems? We can finish in half the time.",
            
        }, 
        {
            "You're so smart... I wish I was that smart.",
            "I hope I do well at the track meet. I'm not the best, but I've been practicing!",
            "Let's go shopping together sometime! Something about your outfit is so stylish and different.",
            
        }, 
        {
            "Sorry I can't hang out this weekend. I'm going to be writing poetry in isolation.",
            "It's so annoying. The bband director likes to pick on me because I'm first chair",
            "Yeah, I did well on the last test. I don't really have to study.",
            
        }, 
        {
            "If you think the new Drake album is good, I'm sorry but you're wrong.",
            "Come play me in Smash Bros.",
            "I seriosly think I'm the best chess player in the school.",
            
        }, 
        
    };

    private static int[] order = new int[80];
    private static int[] oldOrder = new int[80];

    private static string[] whoRevealed = new string[80];
    
    // 0: infected | 1: exposed
    private static bool[,] parameters = new bool[80, 2];

    private static int[] spreadToday = new int[80];

    // 0: spread history index | 1: spread to id 1 | 2: spread from id 1 | 3: spread to id 2 | 4: spread from id 2
    // 5: trend to primary | 6: trend from primary | 7: trend to secondary | 8: trend from secondary |

    private static int[,] spreadStats = new int[16, 9];

    // spreadings
    private static int[,] spreadByRoom = new int[5, 4];

    public static string GetName(int index)
    {
        return names[order[index]];
    }

    public static int GetSex(int index)
    {
        return (order[index] % 2) + 1;
    }

    public static int GetTypeOne(int index)
    {
        if(order[index] < 64)
            return (Mathf.FloorToInt(order[index]/16) + 1);

        else
            return (Mathf.FloorToInt((order[index]-64)/4) + 1);
            
    }

    public static int GetTypeTwo(int index)
    {
        if(order[index] < 64)
            return (((Mathf.FloorToInt(order[index]/4)) % 4) + 1);

        else
            return ((order[index] % 4) + 1);
        
    }

    public static int GetOldTypeOne(int index)
    {
        if(oldOrder[index] < 64)
            return (Mathf.FloorToInt(oldOrder[index]/16) + 1);

        else
            return (Mathf.FloorToInt((oldOrder[index]-64)/4) + 1);
            
    }

    public static int GetOldTypeTwo(int index)
    {
        if(oldOrder[index] < 64)
            return (((Mathf.FloorToInt(oldOrder[index]/4)) % 4) + 1);

        else
            return ((oldOrder[index] % 4) + 1);
        
    }

    public static void Initialize()
    {
        for(int i = 0; i < 80; i++) order[i] = i;
        parameters = new bool[80, 2];
        spreadToday = new int[80];
        spreadByRoom = new int[5, 4];
        spreadStats = new int[16, 9];
        return;
    }

    public static void Shuffle()
    {
        int[] populations = new int[5];
        int[] copies = new int[80];

        for(int i = 0; i < 80; i++)
        {
            oldOrder[i] = order[i];
            copies[i] = order[i];
        }

        for(int i = 0; i < 80; i++)
        {
            int r = Random.Range(0, 79-i);
            int rr = Random.Range(1, 100);

            int room = 3;
            int room1 = 0, room2 = 0;
            int type1 = GetOldTypeOne(r) - 1;

            switch(type1)
            {
                case 0:
                    room1 = 0;
                    break;

                case 1:
                    room1 = 4;
                    break;

                case 2:
                    room1 = 1;
                    break;

                case 3:
                    room1 = 2;
                    break;
            }

            int type2 = GetOldTypeTwo(r) - 1;

            switch(type2)
            {
                case 0:
                    room2 = 0;
                    break;

                case 1:
                    room2 = 4;
                    break;

                case 2:
                    room2 = 1;
                    break;

                case 3:
                    room2 = 2;
                    break;
            }

            // place friends
            if(copies[i] >= 64)
            {
                int f = copies[i];
                int randomChance = (f==0 || f==1 || f==2 || f==3 || f==6 || f==8 || f==10 || f==14) ? 50 : 100;

                // standard
                if(rr < randomChance)
                {
                    if (populations[room1] < 16) // and not full
                        room = room1;
                    
                    else if(populations[room2] < 16) // also not full
                        room = room2;
                }

                // random
                else
                {
                    while(true)
                    {
                        int randRoom = Random.Range(0, 4);
                        if(populations[randRoom] < 16 && randRoom != type1 && randRoom != type2)
                        {
                            room = randRoom;
                            break;
                        }
                    }
                }
            }

            // place normies
            else
            {
                if (rr >= 75 && populations[room1] < 16) // and not full
                    room = room1;
                
                else if(populations[room2] < 16) // also not full
                    room = room2;

                else if (populations[room1] < 16) // 1 was not full
                    room = room1;
            }

            order[(16*room) + populations[room]] = oldOrder[r];
            populations[room]++;

            for(int j = r; j < 79; j++)
            {
                oldOrder[j] = oldOrder[j + 1];
            }
        }
    }

    public static int GetIndex(int index)
    {
        return order[index];
    }

    public static bool GetParameter(int index, int p)
    {
        return parameters[order[index], p];
    }

    public static int GetSpreadToday(int index)
    {
        return spreadToday[order[index]];
    }

    public static int GetSpreadStat(int index, int stat)
    {
        return spreadStats[order[index] - 64, stat];
    }

    public static void SetParameter(int index, int p, bool val)
    {
        parameters[order[index], p] = val;
    }

    public static void SetSpreadToday(int index, int val)
    {
        spreadToday[order[index]] = val;
    }

    public static void SetSpreadStat(int index, int stat, int val)
    {
        spreadStats[order[index] - 64, stat] = val;
    }

    public static int CheckPair(int firstIndex)
    {
        return pairs[firstIndex];
    }

    public static string GetNormieDialogue(int type)
    {
        int r = Random.Range(0, 7);
        return normieDialogue[type, r];
    }

    public static string GetFriendDialogue(int ID)
    {
        int r = Random.Range(0, 2);
        Debug.Log(ID);
        return friendDialogue[ID-64, r];
    }

    public static int GetDistributor(int difficulty)
    {
        int r = Random.Range(0, 7);
        return difficultyIndex[difficulty, r] + 64;
    }

    public static void SetRevealer(int ID, int revealerID)
    {
        whoRevealed[order[ID]] = GetName(revealerID);
    }

    public static string GetRevealer(int ID)
    {
        return whoRevealed[order[ID]];
    }
}

