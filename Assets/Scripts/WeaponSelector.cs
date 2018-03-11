﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour {

    private AudioSource click;
    private GameObject Weapon;
    private RectTransform WeaponRT;
    private RectTransform RT;
    private GameObject WeaponText;
    private RectTransform WeaponTextRT;
    private GameObject[] Weapons;
    private string[] WeaponNames;
    private int currentmap;
    private int selected = 0;
    private int numWeapons = 4;
    private float moveDistance = 4000f;

    public float scrollTime;

	void Start () {
        click = GetComponent<AudioSource>();
        Weapon = transform.GetChild(0).gameObject;
        WeaponRT = Weapon.GetComponent<RectTransform>();
        RT = GetComponent<RectTransform>();
        WeaponText = transform.GetChild(1).gameObject;
        WeaponTextRT = WeaponText.GetComponent<RectTransform>();

        Weapons = new GameObject[numWeapons];
        Weapons[0] = Weapon.transform.GetChild(0).gameObject;
        Weapons[1] = Weapon.transform.GetChild(1).gameObject;
        Weapons[2] = Weapon.transform.GetChild(2).gameObject;
        Weapons[3] = Weapon.transform.GetChild(3).gameObject;

        WeaponNames = new string[numWeapons];
        WeaponNames[0] = "Wallbouncer";
        WeaponNames[1] = "Spear";
        WeaponNames[2] = "Axe";
        WeaponNames[3] = "Sword";

        currentmap = PlayerPrefs.GetInt("CurrentMap");

        WeaponRT.position = new Vector2(moveDistance, WeaponRT.position.y);
        WeaponTextRT.position = new Vector2(moveDistance, WeaponTextRT.position.y);
    }
	
	void Update () {
        if (WeaponRT.position.x != RT.position.x)
            return;

        int oldselected = selected;
        HandleInput();

        Weapons[oldselected].GetComponent<RectTransform>().localScale = new Vector2(0.2f, 0.2f);

        Weapons[selected].GetComponent<RectTransform>().localScale = new Vector2(0.14f, 0.14f);

        if (oldselected != selected)
        {
            click.Play();
            WeaponText.GetComponent<SettingsText>().UpdateText(WeaponNames[selected], selected);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selected = Mathf.Min(selected + 1, numWeapons - 1);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selected = Mathf.Max(selected - 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleSelect();
        }
    }

    private void HandleSelect()
    {
        IEnumerator moveWeapon = MoveRectTo(WeaponRT, WeaponRT.position.x, WeaponRT.position.x - moveDistance, scrollTime);
        IEnumerator moveWeaponText = MoveRectTo(WeaponTextRT, WeaponTextRT.position.x, WeaponTextRT.position.x - moveDistance, scrollTime);
        StartCoroutine(moveWeapon);
        StartCoroutine(moveWeaponText);
    }

    public void Activate()
    {
        IEnumerator moveWeapon = MoveRectTo(WeaponRT, WeaponRT.position.x, RT.position.x, scrollTime);
        IEnumerator moveWeaponText = MoveRectTo(WeaponTextRT, WeaponTextRT.position.x, RT.position.x, scrollTime);

        StartCoroutine(moveWeapon);
        StartCoroutine(moveWeaponText);
    }

    IEnumerator MoveRectTo(RectTransform rect, float oldx, float newx, float time)
    {
        float offset;
        float tempx;
        float starttime = Time.time;
        bool isPositiveDiff = newx < oldx;
        while (Time.time < starttime + time)
        {
            offset = (oldx - newx) * ((Time.time - starttime) / time);
            if (isPositiveDiff)
                tempx = Mathf.Max(newx, oldx - offset);
            else
                tempx = Mathf.Min(newx, oldx + offset);
            rect.position = new Vector2(tempx, rect.position.y);
            yield return null;
        }
        rect.position = new Vector2(newx, rect.position.y);
    }
}