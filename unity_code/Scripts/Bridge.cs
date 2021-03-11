using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

/*
*
* The purpose here is to bridge calls from Unity to javascript functions predefined on the
* containing website index page.
*
*/

public class Bridge : MonoBehaviour
{
    // Per Unity, these directives are necessary to call javascript
    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]

        // The functions defined below should match the functions defined in the plugin popup.jslibs
        // They must be defined here before they can be used in any functions below.
        private static extern void CallModalOnPage(bool source, string href_filename);

    #endif
    
    // =====UNITY TO JAVASCRIPT===== 

    /* Creates a modal to display local or online web content
    *
    *  @param source - true if it is a local page/file, false if it is a URL link to an online page
    *  @param file_name - local file or online url to display
    */
    public static void CallModal(bool source, string file_name)
    {
        Cursor.lockState = CursorLockMode.None;
        #if UNITY_WEBGL && !UNITY_EDITOR
            CallModalOnPage(source, file_name);
        #endif
    }
    
    // =====JAVASCRIPT TO UNITY===== 

    public void CloseModal()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
