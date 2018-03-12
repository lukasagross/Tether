using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private Portal otherPortal;
    private PlayerControls plc;
    private Portal[] portals;
    private List<PlayerControls> playersToIgnore = new List<PlayerControls>();
    private AudioSource audio;



	// Use this for initialization
	void Start () {
        portals = FindObjectsOfType<Portal>();
        audio = GetComponent<AudioSource>();

        if(portals[0].gameObject == gameObject)
        {
            otherPortal = portals[1];
        }
        else
        {
            otherPortal = portals[0];
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        plc = col.gameObject.GetComponent<PlayerControls>();
        Grapple grp = col.gameObject.GetComponent<Grapple>();

        if (plc != null && !playersToIgnore.Contains(plc))
        {
            otherPortal.disableForPlayer(plc);
            col.gameObject.transform.position = otherPortal.GetComponent<Transform>().position;
            StartCoroutine(otherPortal.ReEnable(plc));
            grp.ToggleGrapple(false);
            audio.Play();
        }
    }

    public void  disableForPlayer(PlayerControls plc)
    {
        playersToIgnore.Add(plc);
    }

    public IEnumerator ReEnable(PlayerControls plc)
    {
        yield return new WaitForSeconds(0.5f);
        playersToIgnore.Remove(plc);
    }
}
