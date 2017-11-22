#pragma strict

private var anim : Animator;
private var character : GameObject;
var checkPlayer : boolean = false;
var distanceToOpen:float = 5;
private var characterNearbyHash : int = Animator.StringToHash("character_nearby");

function Start () 
{
    anim = GetComponent("Animator");
	character = GameObject.FindWithTag("Player");
}


function Update () 
{
	//if (checkPlayer){
	//print("check player worked");
		//character = GameObject.FindWithTag("Player");
		//checkPlayer = false;
	//}
	character = GameObject.FindWithTag("Player");
	var distance = Vector3.Distance(transform.position,character.transform.position);
	GetComponent("DoorBoolInfo");
	if (distanceToOpen>=distance){
    	anim.SetBool(characterNearbyHash, true);
    }else{
    	anim.SetBool(characterNearbyHash, false);
    }
}