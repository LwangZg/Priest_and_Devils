using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    Director instance;
    IUserActions action;

    void Start()
    {
        instance = Director.getInstance();
        action = instance.controller as IUserActions;
    }

    private void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.fontSize = 40;
        fontStyle.normal.textColor = new Color(255, 255, 255);

        if (action.Check() == 1)
        {
            GUI.Label(new Rect(440, 200, 100, 100), "Win", fontStyle);
            if (GUI.Button(new Rect(435, 360, 80, 50), "Restart"))
            {
                action.Restart();
            }
        }
        if (action.Check() == 2)
        {
            GUI.Label(new Rect(390, 200, 100, 100), "GameOver", fontStyle);
            if (GUI.Button(new Rect(435, 360, 80, 50), "Restart"))
            {
                action.Restart();
            }
        }
        if (GUI.Button(new Rect(130, 300, 50, 50), "On"))
        {
            action.devilOn();
        }
        if (GUI.Button(new Rect(270, 300, 50, 50), "On"))
        {
            action.priestOn();
        }
        if (GUI.Button(new Rect(600, 300, 50, 50), "On"))
        {
            action.priestOnEnd();
        }
        if (GUI.Button(new Rect(750, 300, 50, 50), "On"))
        {
            action.devilOnEnd();
        }
        if (GUI.Button(new Rect(450, 300, 50, 50), "Move"))
        {
            action.changeState();
        }
        if (GUI.Button(new Rect(450, 500, 50, 50), "POff"))
        {
            action.priestOff();
        }
        if (GUI.Button(new Rect(450, 600, 50, 50), "DOff"))
        {
            action.devilOff();
        }
    }
}
