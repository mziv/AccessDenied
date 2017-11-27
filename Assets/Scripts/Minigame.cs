using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{

    public float speed;
    private Image[] images;
    private bool[] activated;
    private Image player;
    
    public Vector3 startPosBody1;
    public Vector3 startPosBody2;
    public Vector3 startPos;
    //private Image body1;
    private Image[] bodies;
    private Image background;

    private float curTime = 0;
    public float waitTime;
    public float deathTime;
    public int AITurn;

    //numBodies MUST be accurate for this to work!!
    public int numBodies = 2;
    private bool fading = false;

    //private bool danger = false;
    //private float timeToDie = 0;
    private bool endGame = false;

    public bool playingSound = false;

    public void Start()
    {
        images = GetComponentsInChildren<Image>();
        InitializeBackground();
        InitializePlayer();
        InitializeFinishes();
        InitializeImages();
        InitializeActivated();
        RenderBoxes();
        print("start");
    }

    void InitializeBackground()
    {
        background = images[0];
        Color black = new Color(0, 0, 0);
        SetBackgroundColor(black);
    }

    void SetBackgroundColor(Color c)
    {
        background.GetComponent<CanvasRenderer>().SetColor(c);
    }

    void InitializePlayer()
    {

        player = images[images.Length - 1];
        //startPos = startPosBody1;
        //player = GameObject.FindGameObjectWithTag("virtualPlayer");

    }

    void InitializeFinishes()
    {
        bodies = new Image[numBodies];
        for(int i = 0; i < numBodies; i++)
        {
            bodies[i] = images[images.Length - 1 - numBodies + i];
        }
        //body1 = images[images.Length - 2];
    }

    void InitializeImages()
    {
        Image[] temp = new Image[images.Length - (numBodies + 2)];
        for (int i = 1; i < images.Length - (numBodies + 1); i++)
        {
            temp[i - 1] = images[i];
        }
        images = temp;
    }

    void InitializeActivated()
    {
        activated = new bool[images.Length];
        SetActivated(false);
    }

    void SetActivated(bool val)
    {
        for (int i = 0; i < activated.Length; i++)
        {
            activated[i] = val;
        }
    }

    void Update()
    {
        if (!endGame)
        {
            MovePlayer();
            CheckAI();
            CheckFinish();
            UpdateAI();
        } 
    }

    //called by other scripts when opening terminal.
    public void ResetTerminal()
    {
        print("reset terminal");

        player.GetComponent<RectTransform>().anchoredPosition = startPos;
    }

    void MovePlayer()
    {
        Vector3 newPos = player.GetComponent<RectTransform>().anchoredPosition;

        if (Input.GetKey("w"))
        {
            if (CheckInBounds("up")) newPos.y = newPos.y + speed * Time.deltaTime;
        }
        else if (Input.GetKey("s"))
        {
            if (CheckInBounds("down")) newPos.y = newPos.y - speed * Time.deltaTime;
        }
        else if (Input.GetKey("a"))
        {
            if (CheckInBounds("left")) newPos.x = newPos.x - speed * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            if (CheckInBounds("right")) newPos.x = newPos.x + speed * Time.deltaTime;
        }

        player.GetComponent<RectTransform>().anchoredPosition = newPos;
    }

    bool CheckInBounds(string direction)
    {
        float width = player.GetComponent<RectTransform>().rect.width;
        float height = player.GetComponent<RectTransform>().rect.height;
        Vector3 pos = player.GetComponent<RectTransform>().anchoredPosition;
        RectTransform canvas = this.GetComponent<RectTransform>();
        if (direction == "left")
        {
            if (pos.x - width / 2 <= -1 * canvas.rect.width / 2) return false;
        }
        else if (direction == "right")
        {
            if (pos.x + width / 2 >= canvas.rect.width / 2) return false;
        }
        else if (direction == "up")
        {
            if (pos.y + height / 2 >= canvas.rect.height / 2) return false;
        }
        else if (direction == "down")
        {
            if (pos.y - height / 2 <= -1 * canvas.rect.height / 2) return false;
        }

        return true;
    }

    bool CheckAI()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (activated[i])
            {
                if (InsideBox(images[i]))
                {
                    StartCoroutine(FadeBoxRed(i));
                    GetOut(i);
                }
            }
        }
        return false;
    }

    public IEnumerator FadeBoxRed(int index)
    {
        if (!fading)
        {
            fading = true;
            float time = Time.realtimeSinceStartup;
            Color original = images[index].GetComponent<CanvasRenderer>().GetColor();
            Color moreRed = original;
            while (time < time + deathTime && InsideBox(images[index]))
            {
                //Color temp = new Color(moreRed.r, moreRed.g - (time / (time + 1))*0.5f, moreRed.b - (time / (time + 1))*0.5f);
                Color temp = new Color(255, 0, 0);
                images[index].GetComponent<CanvasRenderer>().SetColor(temp);
                yield return new WaitForSeconds(0.05f);
                time += 0.05f;
            }
            images[index].GetComponent<CanvasRenderer>().SetColor(original);
            fading = false;
        }
        
    }

    void CheckFinish()
    {
        if (InsideBox(bodies[1]))
        {
            //if (FindObjectOfType<CollectionManager>().card1)
            //{
            //print(FindObjectOfType<CollectionManager>().card1);
            startPos = startPosBody2;
            print("set start pos to body2");
            WinGame(1);
            
            

            //} else
            //{
                //PlaySound("access denied");
                //print("access denied");
                //FindObjectOfType<AIAudioController>().PlayAccessDenied();
                
            //}
            
        }
        else if (InsideBox(bodies[0]))
        {
            startPos = startPosBody1;
            WinGame(0);
            
        }
    }

    void WinGame(int body)
    {
        SetActivated(false);
        RenderBoxes();
        FindObjectOfType<NewTerminalScript>().bodyToSwitchTo = body;
        FindObjectOfType<NewTerminalScript>().gameWon = true;
    }

    bool InsideBox(Image box)
    {
        float boxWidth = box.GetComponent<RectTransform>().rect.width;
        float boxHeight = box.GetComponent<RectTransform>().rect.height;

        Vector3 playerPos = player.GetComponent<RectTransform>().anchoredPosition;
        Vector3 boxPos = box.GetComponent<RectTransform>().anchoredPosition;

        float leftBound = boxPos.x - boxWidth / 2;
        float rightBound = boxPos.x + boxWidth / 2;
        float upperBound = boxPos.y + boxHeight / 2;
        float lowerBound = boxPos.y - boxHeight / 2;

        if (playerPos.x > leftBound && playerPos.x < rightBound &&
            playerPos.y < upperBound && playerPos.y > lowerBound)
        {
            return true;
        }

        return false;
    }

    void UpdateAI()
    {
        if (CheckTimeValid())
        {
            for(int i = 0; i < images.Length; i++)
            {
                if (!InsideBox(images[i])) activated[i] = false;
            }
            //SetActivated(false);
            PickBoxes();
            RenderBoxes();
        }
    }

    void PickBoxes()
    {
        System.Random rng = new System.Random();
        for (int i = 0; i < AITurn; i++)
        {
            activated[rng.Next(0, images.Length)] = true;
        }
    }

    bool CheckTimeValid()
    {
        float time = Mathf.Ceil(Time.realtimeSinceStartup);

        if (Mathf.Ceil(Time.realtimeSinceStartup) % waitTime == 0 &&
            Mathf.Ceil(Time.realtimeSinceStartup) != curTime)
        {
            curTime = time;
            return true;
        }

        return false;
    }

    void RenderBoxes()
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (activated[i])
            {
                images[i].GetComponent<CanvasRenderer>().SetAlpha(1f);

            }
            else images[i].GetComponent<CanvasRenderer>().SetAlpha(0f);
        }
    }

    void GetOut(int index)
    {
        StartCoroutine(WaitCheckSafe(index));
        FindObjectOfType<AIAudioController>().PlayDetectionSound();
        
    }

    public IEnumerator WaitCheckSafe(int index)
    {
        float time = 0;
        while (InsideBox(images[index])) 
        {
            yield return new WaitForSeconds(0.05f);
            time += 0.05f;
            if (time >= deathTime) LoseGame();
        }
    }

    void LoseGame()
    {
        endGame = true;
        SetActivated(true);
        RenderBoxes();
        FindObjectOfType<NewTerminalScript>().gameLost = true;
    }
}
