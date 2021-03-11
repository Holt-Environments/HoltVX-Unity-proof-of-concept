using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleAnimate : MonoBehaviour
{
    public Image reticle_select_icon;

    private Vector3 velocity = Vector3.zero;
    // private bool animating = false;

    public Color reticle_color = Color.black;
    private byte r, g, b;
    public float animation_length = .5f;
    public float reticle_size = 1.0f;

    private void Start()
    {
        r = (byte)reticle_color.r;
        g = (byte)reticle_color.g;
        b = (byte)reticle_color.b;

        ResetReticle();

        GameEvents.current.onLookAt += OnLookAt;
        GameEvents.current.onLookAway += OnLookAway;
    }

    void ResetReticle()
    {
        reticle_select_icon.color = new Color32(r, g, b, 0);
        reticle_select_icon.transform.localScale = new Vector3(0f, 0f, 0f);
        reticle_select_icon.transform.localRotation = Quaternion.Euler(0, 0, 45f);
    }

    void ReticleOn()
    {
        Helper.Animate(
            animation_length, 
            ((t) => {
                reticle_select_icon.color = Color.Lerp(reticle_select_icon.color, reticle_color, t);
                reticle_select_icon.transform.localRotation = Quaternion.Euler(Vector3.Lerp(reticle_select_icon.transform.localRotation.eulerAngles , new Vector3(0, 0, 225f), t));
                reticle_select_icon.transform.localScale = Vector3.Lerp( reticle_select_icon.transform.localScale , new Vector3(reticle_size, reticle_size, reticle_size), t);
                }), 
            (() => {
                reticle_select_icon.color = reticle_color;
                reticle_select_icon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 225));
                reticle_select_icon.transform.localScale = new Vector3(reticle_size, reticle_size, reticle_size);
                })
        );
    }

    void ReticleOff()
    {
        Helper.Animate(
            animation_length, 
            ((t) => {
                reticle_select_icon.transform.localRotation = Quaternion.Euler(Vector3.Lerp(reticle_select_icon.transform.localRotation.eulerAngles , new Vector3(0, 0, 45f), t));
                reticle_select_icon.color = Color.Lerp(reticle_select_icon.color, new Color32(r, g, b, 0) , t);
                reticle_select_icon.transform.localScale = Vector3.Lerp( reticle_select_icon.transform.localScale , new Vector3(0, 0, 0), t);
                }), 
            (() => {
                reticle_select_icon.color = new Color32(r, g, b, 0);
                reticle_select_icon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 45));
                reticle_select_icon.transform.localScale = new Vector3(0, 0, 0);
                })
        );
    }

    private void OnLookAt()
    {
        //Debug.Log("OnLookAt ran");
        ReticleOn();
    }

    private void OnLookAway()
    {
        //Debug.Log("OnLookAway ran");
        ReticleOff();
    }
}
