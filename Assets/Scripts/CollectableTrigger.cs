using UnityEngine;

public class CollectableTrigger : MonoBehaviour {

    private bool playerInRange = false;
    private CollectionManager manager;

    void Start()
    {
        manager = FindObjectOfType<CollectionManager>();
    }

    void Update()
    {
        collect();
    }

    void collect()
    {
        if (Input.GetKeyDown("e") && playerInRange)
        {
            Debug.Log("collected");
            if (gameObject.name == "card1") manager.card1 = true;
            else if (gameObject.name == "card2") manager.card2 = true;
            else if (gameObject.name == "card3") manager.card3 = true;
            GetComponent<AudioSource>().Play();
            Invoke("destroyCard", 0.5f);
        }
    }

    void destroyCard()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter()
    {
        playerInRange = true;
    }

    void OnTriggerExit()
    {
        playerInRange = false;
    }
}
