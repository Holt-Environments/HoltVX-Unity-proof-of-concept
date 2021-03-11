using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event System.Action onLookAt;
    public void LookAt()
    {
        if(onLookAt != null)
        {
            onLookAt();
        }
    }

    public event System.Action onLookAway;
    public void LookAway()
    {
        ClearLeftClick();
        if(onLookAway != null)
        {
            onLookAway();
        }
    }

    public event System.Action onLeftClick;
    public void LeftClick()
    {
        if(onLeftClick != null)
        {
            onLeftClick();
        }
    }

    public void ClearLeftClick(){
        onLeftClick = null;
    }
}
