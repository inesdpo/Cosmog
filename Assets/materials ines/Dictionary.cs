using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary : MonoBehaviour
{
    Dictionary<string, bool> GameEvents = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.V))
            DoAction("IsItemPickedUp"); //need to do this but first check if the last item has been picked up and 
    }

    private void DoAction(string IDToToggle)
    {
        NotifyActionHappened(IDToToggle);
    }
    public void Item1(string IDToToggle)
    {
        GameEvents["Item1PickedUp"] = true;
    }

    public void Item2(string IDToToggle)
    {
        GameEvents["Item2PickedUp"] = true;
    }

    public void NotifyActionHappened(string IDToToggle)
    {
        GameEvents["IsItemPickedUp"] = true;
    }

    public bool GetStateForGameEVent(string IDToGet)
    {
        return GameEvents[IDToGet];
    }

}
