using UnityEngine;

public class SmokeTrigger : MonoBehaviour {

    private CustomEffect effects;
    private BlurEffect blurEffect;

    void Start()
    {
        effects = FindObjectOfType<CustomEffect>();
        blurEffect = FindObjectOfType<BlurEffect>();
    }

	void OnTriggerEnter()
    {
        effects.enabled = true;
        blurEffect.enabled = true;
    }

    void OnTriggerExit()
    {
        effects.enabled = false;
        blurEffect.enabled = false;
    }
}
