using UnityEngine;

public class CameraPos : MonoBehaviour {

    public Vector3 cameraPos;
    private GameObject player;
    public float upDownOffset = 3.0f;
    public bool checkPlayer = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        LocatePlayer();
        this.transform.position = player.transform.position + cameraPos;
        this.transform.LookAt(player.transform.position + upDownOffset * transform.up);
    }

    void LocatePlayer()
    {
        if (checkPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            FindObjectOfType<Worker>().locateTarget = true;
            checkPlayer = false;
        }
    }
}
