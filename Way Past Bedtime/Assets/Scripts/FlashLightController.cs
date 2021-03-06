using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashLightController : MonoBehaviour
{
    // Start is called before the first frame update

    public Light2D flashlight;

    public float batteryLife = 100f;

    public float batteryLifeDrainSpeed = 5;

    private Controls defaultControls;

    Rigidbody2D rb2d;
    Camera cam;
    Vector2 mousePos;

    public GameObject trigger;

    bool active = false;

    LightFlicker flicker;

    public AudioSource asource;

    private void Awake()
    {
        defaultControls = new Controls();
        flashlight.gameObject.SetActive(active);
        trigger.SetActive(active);
        rb2d = transform.parent.GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        defaultControls.Enable();
    }

    private void OnDisable()
    {
        defaultControls.Disable();
    }

    void Start()
    {
        defaultControls.Newactionmap.Flashlight.performed += ToggleFlashLight;
        defaultControls.Newactionmap.Flashlight.canceled += ToggleFlashLight;
        flicker = flashlight.GetComponent<LightFlicker>();
        flicker.minFlicker = 1;
    }

    private void ToggleFlashLight(InputAction.CallbackContext context)
    {
        if(batteryLife > 0)
        {
            AudioManager.PlaySoundEffect("buttonclick", asource);
            active = !active;
            flashlight.gameObject.SetActive(active);
            trigger.SetActive(active);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if(active)
        {
            if(batteryLife - (batteryLifeDrainSpeed * Time.deltaTime) <= 0)
            {
                batteryLife = 0;
                active = false;
                flashlight.gameObject.SetActive(active);
                AudioManager.PlaySoundEffect("batterydead", asource);
                trigger.SetActive(active);
            } else
            {
                batteryLife = batteryLife - (batteryLifeDrainSpeed * Time.deltaTime);
            }

            if (batteryLife < 50)
            {
                flicker.minFlicker = batteryLife / 100f;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
    }

    public void SendRayToHitObject()
    {

    }

}
