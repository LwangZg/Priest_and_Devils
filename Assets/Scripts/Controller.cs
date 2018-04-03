using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IUserActions
{

    private GenGameObject model;


    private void Awake()
    {
        Director director = Director.getInstance();
        director.controller = this;
    }

    public void setModel(GenGameObject Model)
    {
        model = Model;
    }

    public void priestOn()
    {
        model.priestOn();
    }

    public void devilOn()
    {
        model.devilOn();
    }

    public void changeState()
    {
        model.change_state();
    }

    public void priestOnEnd()
    {
        model.priestOnEnd();
    }

    public void devilOnEnd()
    {
        model.devilOnEnd();
    }

    public int Check()
    {
        return model.Check();
    }

    public void priestOff()
    {
        model.priestOff();
    }

    public void devilOff()
    {
        model.devilOff();
    }

    public void Restart()
    {
        model.Restart();
    }
}
