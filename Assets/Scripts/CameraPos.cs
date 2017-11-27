using UnityEngine;

public class CameraPos : MonoBehaviour {

    public Vector3 cameraPos;
    private GameObject player;
    public float upDownOffset = 3.0f;
    public bool checkPlayer = false;
    private bool cameraTouchingWall = false;
    private float playerToCamDistance;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerToCamDistance = getPlayerCamDistance();
	}
	
	// Update is called once per frame
	void Update () {
        LocatePlayer();
        //Debug.Log(playerToCamDistance);
        //Debug.Log(getPlayerCamDistance());
        Vector3 newCameraPos = translatePosToV3();
        //isPlayerFarFromWall();
        //if(!cameraTouchingWall) this.transform.position = player.transform.position + newCameraPos;
        this.transform.position = player.transform.position + newCameraPos;
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

    Vector3 translatePosToV3()
    {
        float faceDirectionMagnitude = Mathf.Sqrt(Mathf.Pow(player.transform.forward.x, 2) + Mathf.Pow(player.transform.forward.z, 2));
        float scaleX = cameraPos.x / faceDirectionMagnitude;
        float scaleZ = cameraPos.z / faceDirectionMagnitude;

        float x = player.transform.forward.x * scaleX * -1 + player.transform.forward.z * scaleZ;
        float z = player.transform.forward.z * scaleX * -1 + player.transform.forward.x * scaleZ * -1;

        return new Vector3(x, cameraPos.y , z);
    }

    void isPlayerFarFromWall()
    {
        if (playerToCamDistance <= getPlayerCamDistance()) cameraTouchingWall = false;
    }

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
