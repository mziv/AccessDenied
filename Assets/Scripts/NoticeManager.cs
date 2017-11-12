﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeManager : MonoBehaviour {

    public GameObject collectableNotif;
    public GameObject datacardNotif;
    public GameObject alert;
    public GameObject smokeNotice;

    void Start()
    {
        alert.SetActive(true);
        smokeNotice.SetActive(false);
    }

    public void nextNotice()
    {
        alert.SetActive(false);
        smokeNotice.SetActive(true);
    }

    public void close()
    {
        smokeNotice.SetActive(false);
        collectableNotif.SetActive(false);
        datacardNotif.SetActive(false);
    }
}
