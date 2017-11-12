using UnityEngine;

public class CollectableTrigger : MonoBehaviour {

    private bool playerInRange = false;

    void Update()
    {
        collect();
    }

    void collect()
    {
        if (Input.GetKeyDown("e") && playerInRange)
        {
            Debug.Log("collected");
            Destroy(gameObject);
        }
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
