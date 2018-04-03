using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenGameObject : MonoBehaviour {

    private Director instance;
    int onBoat;
    public int speed;
    int boat_state;
    GameObject[] objectOnBoat = new GameObject[2];
    readonly Vector3 shore_begin = new Vector3(10, 0, 0);
    readonly Vector3 shore_end = new Vector3(-10, 0, 0);
    readonly Vector3 boat_begin = new Vector3(-3, 0, 0);
    readonly Vector3 boat_end = new Vector3(3, 0, 0);
    readonly Vector3 priests_begin = new Vector3(-9f, 2, 0);
    readonly Vector3 priests_last = new Vector3(9f, 2, 0);
    readonly Vector3 devils_begin = new Vector3(-15f, 2, 0);
    readonly Vector3 devils_last = new Vector3(15f, 2, 0);
    readonly Vector3 gap = new Vector3(-1.5f, 0, 0);
    Stack<GameObject> priests_start = new Stack<GameObject>();
    Stack<GameObject> devils_start = new Stack<GameObject>();
    Stack<GameObject> priests_end = new Stack<GameObject>();
    Stack<GameObject> devils_end = new Stack<GameObject>();
    GameObject boat;

    private void Start()
    {
        speed = 10;
        onBoat = 0;
        boat_state = 1;
        instance = Director.getInstance();
        instance.controller.setModel(this);
        Load();
    }

    private void Update()
    {
        if (Check() == 3)
        {
            boatMove();
        }
    }

    private void Load()
    {
        Instantiate(Resources.Load("Prefabs/Shore"), shore_begin, Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/Shore"), shore_end, Quaternion.identity);
        boat = Instantiate(Resources.Load("Prefabs/Boat"), boat_begin, Quaternion.identity) as GameObject;
        boat.name = "boat";
        for (int i = 0; i < 3; i++)
        {
            GameObject priest = Instantiate(Resources.Load("Prefabs/Priest"), (priests_begin - gap * i), Quaternion.identity) as GameObject;
            priests_start.Push(priest);
            GameObject devil = Instantiate(Resources.Load("Prefabs/Devil"), (devils_begin - gap * i), Quaternion.identity) as GameObject;
            devils_start.Push(devil);
        }
    }


    public void priestOn()
    {
        if (priests_start.Count == 0)
            return;
        if (onBoat < 2)
        {
            GameObject temp = priests_start.Pop();
            temp.transform.parent = boat.transform;
            if (objectOnBoat[0] == null)
            {
                temp.transform.localPosition = new Vector3(0.3f, 1.5f, 0);
                objectOnBoat[0] = temp;
            }
            else
            {
                temp.transform.localPosition = new Vector3(-0.3f, 1.5f, 0);
                objectOnBoat[1] = temp;
            }
            onBoat++;
        }
    }

    public void priestOnEnd()
    {
        if (priests_end.Count == 0)
            return;
        if (onBoat < 2)
        {
            GameObject temp = priests_end.Pop();
            temp.transform.parent = boat.transform;
            if (objectOnBoat[0] == null)
            {
                temp.transform.localPosition = new Vector3(0.3f, 1.5f, 0);
                objectOnBoat[0] = temp;
            }
            else
            {
                temp.transform.localPosition = new Vector3(-0.3f, 1.5f, 0);
                objectOnBoat[1] = temp;
            }
            onBoat++;
        }
    }

    public void priestOff()
    {
        for (int i = 0; i < 2; i++)
        {
            if (objectOnBoat[i] != null && objectOnBoat[i].tag == "Priest")
            {
                if (boat.transform.position != boat_begin && boat.transform.position != boat_end)
                    return;
                GameObject pri = objectOnBoat[i];
                pri.transform.parent = null;
                if (boat.transform.position == boat_end)
                {
                    pri.transform.position = priests_last + gap * priests_end.Count;
                    priests_end.Push(pri);
                }
                else if (boat.transform.position == boat_begin)
                {
                    pri.transform.position = priests_begin - gap * priests_start.Count;
                    priests_start.Push(pri);
                }
                objectOnBoat[i] = null;
                onBoat--;
                return;
            }
        }
    }

    public void devilOn()
    {
        if (devils_start.Count == 0)
            return;
        if (onBoat < 2)
        {
            GameObject temp = devils_start.Pop();
            temp.transform.parent = boat.transform;
            if (objectOnBoat[0] == null)
            {
                temp.transform.localPosition = new Vector3(0.3f, 1.5f, 0);
                objectOnBoat[0] = temp;
            }
            else
            {
                temp.transform.localPosition = new Vector3(-0.3f, 1.5f, 0);
                objectOnBoat[1] = temp;
            }
            onBoat++;
        }
    }

    public void devilOnEnd()
    {
        if (devils_end.Count == 0)
            return;
        if (onBoat < 2)
        {
            GameObject temp = devils_end.Pop();
            temp.transform.parent = boat.transform;
            if (objectOnBoat[0] == null)
            {
                temp.transform.localPosition = new Vector3(0.3f, 1.5f, 0);
                objectOnBoat[0] = temp;
            }
            else
            {
                temp.transform.localPosition = new Vector3(-0.3f, 1.5f, 0);
                objectOnBoat[1] = temp;
            }
            onBoat++;
        }
    }

    public void devilOff()
    {
        for (int i = 0; i < 2; i++)
        {
            if (objectOnBoat[i] != null && objectOnBoat[i].tag == "Devil")
            {
                if (boat.transform.position != boat_begin && boat.transform.position != boat_end)
                    return;
                GameObject dev = objectOnBoat[i];
                dev.transform.parent = null;
                if (boat.transform.position == boat_end)
                {
                    dev.transform.position = devils_last + gap * devils_end.Count;
                    devils_end.Push(dev);
                }
                else if (boat.transform.position == boat_begin)
                {
                    dev.transform.position = devils_begin - gap * devils_start.Count;
                    devils_start.Push(dev);
                }
                objectOnBoat[i] = null;
                onBoat--;
                return;
            }
        }
    }

    public void boatMove()
    {
        if (boat_state == 0)
        {
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, boat_end, speed * Time.deltaTime);
            if (boat.transform.position == boat_end)
            {
                boat_state = 1;
            }
        }
        else if (boat_state == 2)
        {
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, boat_begin, speed * Time.deltaTime);
            if (boat.transform.position == boat_begin)
            {
                boat_state = 1;
            }
        }
    }

    public void change_state()
    {
        if (onBoat > 0)
        {
            if (boat.transform.position == boat_end)
            {
                boat_state = 2;
            }
            if (boat.transform.position == boat_begin)
            {
                boat_state = 0;
            }
        }
    }

    public int Check()
    {
        if (devils_end.Count == 3 && priests_end.Count == 3)
        {
            return 1;
        }
        if ((devils_start.Count > priests_start.Count && priests_start.Count != 0))
        {
            if (boat.transform.position == boat_end)
            {
                return 2;
            }
            if (boat.transform.position == boat_begin && onBoat == 0)
            {
                return 2;
            }
        }
        if ((devils_end.Count > priests_end.Count && priests_end.Count != 0))
        {
            if (boat.transform.position == boat_begin)
            {
                return 2;
            }
            if (boat.transform.position == boat_end && onBoat == 0)
            {
                return 2;
            }
        }

        return 3;
    }

    public void Restart()
    {
        int num = priests_end.Count;
        for (int i = 0; i < num; i++)
        {
            GameObject temp = priests_end.Pop();
            temp.transform.position = priests_begin - gap * priests_start.Count;
            priests_start.Push(temp);
        }
        num = devils_end.Count;
        for (int i = 0; i < num; i++)
        {
            GameObject temp = devils_end.Pop();
            temp.transform.position = devils_begin - gap * devils_start.Count;
            devils_start.Push(temp);
        }
        for (int i = 0; i < onBoat; i++)
        {
            if (objectOnBoat[i] == null)
            {
                i += 1;
            }
            GameObject temp = objectOnBoat[i];
            temp.transform.parent = null;
            if (temp.tag == "Priest")
            {
                temp.transform.position = priests_begin - gap * priests_start.Count;
                priests_start.Push(temp);
            }
            else
            {
                temp.transform.position = devils_begin - gap * devils_start.Count;
                priests_start.Push(temp);
            }
            objectOnBoat[i] = null;
        }
        onBoat = 0;
        boat.transform.position = boat_begin;
        boat_state = 1;
    }
}
