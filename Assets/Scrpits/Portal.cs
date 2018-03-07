using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private Portal otherPortal;
    private PlayerControls plc;
    private Portal[] portals;
    private List<PlayerControls> playersToIgnore = new List<PlayerControls>();



	// Use this for initialization
	void Start () {
        portals = FindObjectsOfType<Portal>();

        if(portals[0].gameObject == gameObject)
        {
            otherPortal = portals[1];
        }
        else
        {
            otherPortal = portals[0];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        plc = col.gameObject.GetComponent<PlayerControls>();

        if (plc != null && !playersToIgnore.Contains(plc))
        {
            otherPortal.disableForPlayer(plc);
            col.gameObject.transform.position = otherPortal.GetComponent<Transform>().position;
            StartCoroutine(otherPortal.ReEnable(plc));
        }
    }

    public void  disableForPlayer(PlayerControls plc)
    {
        playersToIgnore.Add(plc);
    }

    public IEnumerator ReEnable(PlayerControls plc)
    {
        yield return new WaitForSeconds(1f);
        playersToIgnore.Remove(plc);
    }
}
