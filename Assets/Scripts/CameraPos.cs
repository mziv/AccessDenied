using UnityEngine;

public class CameraPos : MonoBehaviour {

    public Vector3 cameraPos;
    private GameObject player;
    public float upDownOffset = 3.0f;
    public bool checkPlayer = false;
    private bool cameraTouchingWall = false;
    private bool slerpCamera = false;
    //after camera stick on wall, the time it takes for camera to resume position
    public float slerpTimer = 3f;
    private float currentTimer = 0f;
    private static float initplayerToCamDistance = 2.152759f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //initplayerToCamDistance = getPlayerCamDistance();
	}
	
	// Update is called once per frame
	void Update () {
        LocatePlayer();
        //Debug.Log(playerToCamDistance);
        //Debug.Log(initplayerToCamDistance);
        Vector3 newCameraPos = translatePosToV3();

        Vector3 cameraDestination = player.transform.position + newCameraPos;
        Vector3 cameraCurrent = this.transform.position;

        //if (slerpCamera)
        //{
            //currentTimer += Time.deltaTime;
            //if (currentTimer >= slerpTimer) slerpCamera = false;
            //this.transform.position = Vector3.Slerp(cameraCurrent, cameraDestination, Time.deltaTime * 6);
        //}
        //else if (!cameraTouchingWall) this.transform.position = player.transform.position + newCameraPos;
        if (!cameraTouchingWall) this.transform.position = Vector3.Slerp(cameraCurrent, cameraDestination, Time.deltaTime * 6);
        isPlayerFarFromWall();

        //this.transform.position = player.transform.position + newCameraPos;
        this.transform.LookAt(player.transform.position + upDownOffset * transform.up);
    }

    //after player switch body update player in camera so camera know which player to follow
    void LocatePlayer()
    {
        if (checkPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            FindObjectOfType<Worker>().locateTarget = true;
            checkPlayer = false;
        }
    }

    //makes sure camera always stays relative to player
    Vector3 translatePosToV3()
    {
        float faceDirectionMagnitude = Mathf.Sqrt(Mathf.Pow(player.transform.forward.x, 2) + Mathf.Pow(player.transform.forward.z, 2));
        float scaleX = cameraPos.x / faceDirectionMagnitude;
        float scaleZ = cameraPos.z / faceDirectionMagnitude;

        float x = player.transform.forward.x * scaleX * -1 + player.transform.forward.z * scaleZ;
        float z = player.transform.forward.z * scaleX * -1 + player.transform.forward.x * scaleZ * -1;

        return new Vector3(x, cameraPos.y , z);
    }

    //old camera wall fix
    void isPlayerFarFromWall()
    {
        if (initplayerToCamDistance <= getPlayerCamDistance())
        {
            cameraTouchingWall = false;
            //slerpCamera = true;
            //currentTimer = 0;
        }
    }

    //old camera wall fix
    float getPlayerCamDistance()
    {
        return Mathf.Sqrt(Mathf.Pow((player.transform.position - this.transform.position).x, 2) + Mathf.Pow((player.transform.position - this.transform.position).z, 2));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            cameraTouchingWall = true;
            //Debug.Log("touch");
        }
    }
}
