using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*
*   This class is for defining custom events. The meat and potatoes here
*   is that the caller is accessing the dictionary to lookup the appropriate
*   event function to use based off of the id value of the object that corresponds
*   to the event.
*
*   It is expected that some events may utilize asynchronous functions, and because of
*   this, CustomEvents must extend MonoBehavior. To allow for static functions, an instance
*   of the class is created to reference. The instance also allows for the use of
*   setting the variable values in unity for quick changes to modal url. Because these
*   values are non-static, they need to be accessed through a static class member.
*
*   It may be that the static functions do not need to be static, because they are being
*   instanced once, in that case, the static functions can be non-static and all that needs
*   to be static is the Dictionary list object so that it can be accessed directly.
*   Given that using static assumes the creation of multiple objects, maybe this isnt
*   the best implementation.
*/

public class CustomEvents : MonoBehaviour
{
    public static CustomEvents instance;

    public void Awake()
    {
        instance = this;
    }

    public static Dictionary<string, System.Action> list = new Dictionary<string, System.Action>()
    {
        { "door", DoorEvent },
        { "television", TelevisionEvent },
        { "seat", SeatEvent },
        { "bmw", BmwEvent },
        { "cabinet_door", CabinetDoorEvent }
    };

    public bool door_door_open = false;
    public static void DoorEvent(){
        GameObject main_door = GameObject.Find("door_controller");

        if(!CustomEvents.instance.door_door_open){
            Quaternion start = main_door.transform.localRotation;
            Quaternion end = Quaternion.Euler(start.x, -97f, start.z);

            Helper.Animate(2.0f, 
                ((t) => {main_door.transform.localRotation = Quaternion.Lerp(start, end, t);}), 
                (() => {main_door.transform.localRotation = end;})
            );
            Helper.PlayAudio(main_door, "door_open", 0);
            CustomEvents.instance.door_door_open = true;
        } else {
            Quaternion start = main_door.transform.localRotation;
            Quaternion end = Quaternion.Euler(start.x, 1f, start.z);

            Helper.Animate(2.0f, 
                ((t) => {main_door.transform.localRotation = Quaternion.Lerp(start, end, t);}), 
                (() => {main_door.transform.localRotation = end;})
            );
            Helper.PlayAudio(main_door, "door_closed", 2);
            CustomEvents.instance.door_door_open = false;
        }
    }

    public bool television_source_local;
    public string television_url;
    public static void TelevisionEvent(){
        Bridge.CallModal(CustomEvents.instance.television_source_local, CustomEvents.instance.television_url);
    }

    
    public bool seat_source_local;
    public string seat_url;
    public static void SeatEvent(){
        Bridge.CallModal(CustomEvents.instance.seat_source_local, CustomEvents.instance.seat_url);
    }

    public bool bmw_source_local;
    public string bmw_url;
    public static void BmwEvent(){
        Bridge.CallModal(CustomEvents.instance.bmw_source_local, CustomEvents.instance.bmw_url);
    }

    bool cabinet_door_open = false;
    public static void CabinetDoorEvent(){
        GameObject cabinet_door = GameObject.Find("cabinet_door_controller");

        if(!CustomEvents.instance.cabinet_door_open){
            Quaternion start = cabinet_door.transform.localRotation;
            Quaternion end = Quaternion.Euler(start.x, 155f, start.z);

            Helper.Animate(
                1.0f, 
                ((t) => {cabinet_door.transform.localRotation = Quaternion.Lerp(start, end, t);}), 
                (() => {cabinet_door.transform.localRotation = end;})
            );
            Helper.PlayAudio(cabinet_door, "door_open", 0);
            CustomEvents.instance.cabinet_door_open = true;
        } else {
            Quaternion start = cabinet_door.transform.localRotation;
            Quaternion end = Quaternion.Euler(start.x, 0f, start.z);

            Helper.Animate(
                1.0f, 
                ((t) => {cabinet_door.transform.localRotation = Quaternion.Lerp(start, end, t);}), 
                (() => {cabinet_door.transform.localRotation = end;})
            );
            Helper.PlayAudio(cabinet_door, "door_closed", 1);
            CustomEvents.instance.cabinet_door_open = false;
    }
    }
}
