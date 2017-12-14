using UnityEngine;

public class SmokeTrigger : MonoBehaviour {

    private CustomEffect effects;
    private BlurEffect blurEffect;

    void Start()
    {
        effects = FindObjectOfType<CustomEffect>();
        blurEffect = FindObjectOfType<BlurEffect>();
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            effects.enabled = true;
            blurEffect.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            effects.enabled = false;
            blurEffect.enabled = false;
        }
    }
}
