using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserActions
{
    void priestOn();
    void priestOnEnd();
    void devilOn();
    void devilOnEnd();
    void changeState();
    int Check();
    void priestOff();
    void devilOff();
    void Restart();
}
