﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class P2StoreSelect : MonoBehaviour
{
    public Shop mainShop;

    public Text storeCursor_Player2;
    private const float positionX = 644f;
    private const float positionZ = 0.0f;

    private float[] leTableauDeDieu = new float[5] { 38.68f, -76.0f, -164.0f, -284.0f, -381.0f };
    private float timeBuffer = 0.3f; //delay in seconds
    private bool allowAction = true;
    private int index = 0; // 0 -> premier element

    public Sprite[] images;
    public Image presentImage;
    // Update is called once per frame
    void Start()
    {
        storeCursor_Player2.rectTransform.anchoredPosition3D = new Vector3(positionX, leTableauDeDieu[index], positionZ);
        UpdateImage();
    }

    void Update()
    {

        if (Input.GetButtonDown("Dig2")) {
            Select();
        }

        if (!allowAction)
            return;

        float verticalValue = Input.GetAxis("Vertical_Player2");
        if (verticalValue >= 0.7f)// || Input.GetAxis("DpadUP_Player1") > 0)
        {
            allowAction = false;
            MoveCursorUp();
            Invoke("UnblockCursor", timeBuffer);
        }

        if (verticalValue <= -0.7)// || Input.GetAxis("DpadUP_Player1") < 0)
        {
            allowAction = false;
            MoveCursorDown();
            Invoke("UnblockCursor", timeBuffer);
        }
    }

    void MoveCursorUp()
    {
        if (index == 0)
        {
            storeCursor_Player2.rectTransform.anchoredPosition3D = new Vector3(positionX, leTableauDeDieu[4], positionZ);
            index = 4;
        }

        else
        {
            index--;
            index = index % 5;
            storeCursor_Player2.rectTransform.anchoredPosition3D = new Vector3(positionX, leTableauDeDieu[index], positionZ);
        }

        Debug.Log(index);
        UpdateImage();
    }

    void MoveCursorDown()
    {
        index++;
        index = index % 5;
        storeCursor_Player2.rectTransform.anchoredPosition3D = new Vector3(positionX, leTableauDeDieu[index], positionZ);
        Debug.Log(index);
        UpdateImage();
    }

    void UpdateImage() {
        presentImage.sprite = images[index]; 
    }

    void UnblockCursor()
    {
        allowAction = true;
    }

    void Select() {
        var item = mainShop.items.ElementAt(index);
        
        if( mainShop.buyItem(item.name, false) ) {
            // TODO: positive feedback
        } else {
            // TODO: negative feedback
        }
    }
}
