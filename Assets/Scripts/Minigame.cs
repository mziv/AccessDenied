using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour {

    public float speed;
    private Image[] images;
    private Image player;
    RectTransform rectTransform;

    // Use this for initialization
    void Start () {
        initializePlayer();
    }

    void initializePlayer()
    {
        images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].CompareTag("virtualPlayer"))
            {
                player = images[i];
            }
        }
        rectTransform = player.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("w"))
        {
            print("key check worked");

            Vector3 currentPos = rectTransform.anchoredPosition;
            player.GetComponent<RectTransform>().anchoredPosition = new Vector3(currentPos.x + speed * Time.deltaTime, currentPos.y, currentPos.z);

        }

    }
}
