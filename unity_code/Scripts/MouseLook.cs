using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{

    private static GameObject[] target_objects;

    public float mouseSensitivity = 100f;

    // This player body is provided by the user, so that the script can affect its Transform object.
    public Transform playerBody;
    float xRotation = 0f;
    public Camera tradeshowCam;
    public Image reticle_select_icon;
    public Text hint_text;
    private Vector3 velocity = Vector3.zero;
    private GameObject target = null;
    private bool was_looking_at_target = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hint_text.text = "";
        target_objects = GameObject.FindGameObjectsWithTag("Interactive");
        //Application.targetFrameRate = 24;
    }

    // Update is called once per frame
    void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked){
            PlayerLook();
            RaycastSelect();
        }

        HandleClick();        
    }

    void HandleClick(){
        if(Input.GetAxis("Fire1") == 1){
            GameEvents.current.LeftClick();
            GameEvents.current.ClearLeftClick();
        }
    }

    void PlayerLook(){
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime; // Time.deltaTime is the amount of time that has passed since the last Update() function was called.
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY * 0.1f;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * (mouseX * 0.1f));
    }

    bool NowLookingAtTarget(RaycastHit hit)
    {
        for(int i = 0; i < target_objects.Length; i++){
            if(hit.transform.name == target_objects[i].transform.name){
                return true;
            }
        }

        return false;
    }

    void RaycastSelect()
    {
        RaycastHit hit;

        if(Physics.Raycast(tradeshowCam.transform.position, tradeshowCam.transform.forward, out hit))
        {
            if(!was_looking_at_target && NowLookingAtTarget(hit)){  
                GameEvents.current.LookAt();
                target = GameObject.Find(hit.transform.name);

                hint_text.text = hit.transform.name;
                GameEvents.current.onLeftClick += AddFromEventList(hit.transform.name);

                was_looking_at_target = true;
            } else if (was_looking_at_target && !NowLookingAtTarget(hit)){
                hint_text.text = "";
                GameEvents.current.LookAway();
                was_looking_at_target = false;
            }
            
        } else {
            if(was_looking_at_target){
                hint_text.text = "";
                GameEvents.current.LookAway();
                was_looking_at_target = false;
            }
        }
    }

    public static System.Action AddFromEventList(string name)
    {
        if(CustomEvents.list[name] != null)
        {
            return CustomEvents.list[name];
        } else {
            return (() => {});
        }
    }
}                  