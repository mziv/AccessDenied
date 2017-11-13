using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour {

    public Canvas collection;
    private Text[] texts;
    private const int dataCardOffset = 0;
    private const int acquiredOffset = 3;
    private const int missingOffset = 6;

    //state determines which texts to display when the canvas is displayed
    private const bool inventoryState = true;
    private bool[] dataCardState = new bool[3];
    private bool[] acquiredState = new bool[3];
    private bool[] missingState = new bool[3];

    //state of cards collected
    public bool card1 = false;
    public bool card2 = false;
    public bool card3 = false;

    private bool inventoryOpen = false;

	// Use this for initialization
	void Start () {
        //collect all texts on the inventory canvas
        texts = collection.GetComponentsInChildren<Text>();
        initializeInventoryState();
        disableInventory();
    }
	
	// Update is called once per frame
	void Update () {
        updateCollection();
        if (Input.GetKeyDown("i") && !inventoryOpen)
        {
            displayInventory();
            inventoryOpen = true;
        }
        else if(Input.GetKeyDown("i") && inventoryOpen)
        {
            disableInventory();
            inventoryOpen = false;
        }
	}

    void updateCollection()
    {
        if(card1)
        {
            acquiredState[0] = true;
            missingState[0] = false;
        }
        if (card2)
        {
            acquiredState[1] = true;
            missingState[1] = false;
        }
        if (card3)
        {
            acquiredState[2] = true;
            missingState[2] = false;
        }
        if (inventoryOpen) displayInventory();
    }

    void displayInventory()
    {
        collection.enabled = false;
        inventory().enabled = inventoryState;
        for(int index = 1; index <= 3; index++)
        {
            dataCard(index).enabled = dataCardState[index - 1]; //-1 because state is 0 indexed
            acquired(index).enabled = acquiredState[index - 1];
            missing(index).enabled = missingState[index - 1];
        }
        collection.enabled = true;
    }

    void disableInventory()
    {
        collection.enabled = false;
    }

    void initializeInventoryState()
    {
        for (int index = 0; index < 3; index++)
        {
            dataCardState[index] = true;
            acquiredState[index] = false;
            missingState[index] = true;
        }
    }

    //the following functions take in index to text array and returns the game object
    Text inventory()
    {
        return texts[0];
    }

    Text dataCard(int index)
    {
        return texts[dataCardOffset + index];
    }

    Text acquired(int index)
    {
        return texts[acquiredOffset + index];
    }

    Text missing(int index)
    {
        return texts[missingOffset + index];
    }
}
