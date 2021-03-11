using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Helper class that provides static functions that make animating and playing audio easier.
 *
 * Author: Anthony Mesa
 * Date: 11/18/2020
 */

public class Helper : MonoBehaviour
{
    // Since we need MonoBehavior to call any Coroutine, 'this' is saved to a variable when the
    // game loads so that we can access StartCoroutine from the static functions.
    static public Helper instance;
    void Awake()
    {
        instance = this;
    }
    
    // These helper function are needed to call the coroutine from a MonoBehavior 'instance'.

    /* Begins the animation subroutine
     *
     * time: time provided in seconds
     * animation: callback to use for each frame of time in the animation
     * snap: callback to employ after animation is finished (this is generally best in cases of using lerp, or if you want something
     *          to only fire specifically after the animation is complete)
     */
    public static void Animate(float time, System.Action<float> animation, System.Action snap){
        instance.StartCoroutine(RunInterpolation(time, animation, snap));
    }

    /* Begins the audio playing subroutine
     *
     * time: time provided in seconds
     * audio_source: GameObject with audio source component on it
     * clip_name: name of clip to be played
     */
    public static void PlayAudio(GameObject audio_source, string clip_name, float time){
        instance.StartCoroutine(DoAudio(audio_source, clip_name, time));
    }

    /* The interpolation used in this function is cubic (may include other types of interpolations later).
     * 
     * Animation and Snap are user defined callbacks, where Animation passes the current time of the animation
     * to the callback function. Calling Animate() properly should look like:
     *
     * Helper.Animate(float time in seconds, ((t) => { function body that uses the 't' provided (such as a lerp function) }), (() => { function body... }));
     */
    public static IEnumerator RunInterpolation(float time, System.Action<float> animation, System.Action snap){

        float elapsed_time = 0;

        while(elapsed_time < time){
            float t = elapsed_time / time;
            t = t*t * (3f - 2f*t);
            animation(t);
            elapsed_time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        snap();

        yield return null;
    }

    public static IEnumerator DoAudio(GameObject audio_source, string clip_name, float t){
        Object[] audioClips =  Resources.FindObjectsOfTypeAll(typeof(AudioClip));

        Object audioClip = Resources.Load<AudioClip>("Audio/" + clip_name);
        AudioSource audioData = audio_source.GetComponent<AudioSource>();

        if(audioClip != null)
        {
            audioData.clip = (AudioClip)audioClip; 
            audioData.PlayDelayed(t); 
        } else {
            Debug.Log("Helper: DoAudio - Audio clip " + clip_name + " was not loaded");
        }

        yield return null;
    }
}
