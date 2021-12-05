using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarinputScript : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    private Rigidbody rb;
    [SerializeField] bool UseJoystick; //Decide if you using Joysticks
    [SerializeField] public TMP_Text speedometerTxt;
    [SerializeField] public float speed;
    [SerializeField] float maximumSpeed = 130f;
    [SerializeField] float overSpeedTorque = 150f;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] Joystick joystick;
    [SerializeField] Joystick joystick2;
    [SerializeField] AudioSource _source; //EngineSound
    [Header("pitch parameter")]
    public float flatoutSpeed = 130.0f;
    [Range(0.0f, 3.0f)]
    public float minPitch = 1f;
    [Range(0.0f, 0.1f)]
    public float pitchSpeed = 0.05f;
    [Range(0.5f, 5f)]

    [SerializeField] float downforce = 1.0f;
    public float Downforce { get { return downforce; } set { downforce = Mathf.Clamp(value, 0, 5); } }

    [System.Serializable]
        public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor; // is this wheel attached to motor?
        public bool steering; // does this wheel apply steer angle?
    }
    private void Awake()
    {
        
        //DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        //Debug.Log("CarinputScriptStarted");
        UseJoystick = FindObjectOfType<GameManager>().activateUseJoystick;
        //Debug.Log(UseJoystick);
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.position;
        if (!UseJoystick)
        {
            joystick.gameObject.SetActive(false);
            joystick2.gameObject.SetActive(false);
        }
    }
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        if (!UseJoystick) //If using controls
        {
            speed = transform.InverseTransformDirection(rb.velocity).z * 3.6f; //Calculate currentSpeed
            //_source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(speed/2) / flatoutSpeed, pitchSpeed); //Calculate EnginePith
            //_source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(axleInfos[0].leftWheel.motorTorque  *speed /5 * Time.deltaTime) / flatoutSpeed, pitchSpeed);
            _source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(Input.GetAxis("Vertical") * speed *100 * Time.deltaTime) / flatoutSpeed, pitchSpeed);
            float motor = maxMotorTorque *  Input.GetAxis("Vertical"); //AddTorque from VerticalAxis
            float steering = maxSteeringAngle *  Input.GetAxis("Horizontal"); //Steer HorizontalAxis

            foreach (AxleInfo axleInfo in axleInfos)//get all the wheels from Inpsector 
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor && speed < maximumSpeed)//If below MaxSpeed add Torque
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                    
                }
                else//If above MaxSpeed apply OverspeedTorque
                {
                    axleInfo.leftWheel.motorTorque = overSpeedTorque;
                    axleInfo.rightWheel.motorTorque = overSpeedTorque;
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }

           

            rb.AddForce(-transform.up * speed * downforce); //AddDownforce 
                      
                
           // Debug.Log(motor);

        }
        else //If UsingJoystick
        {
            speed = transform.InverseTransformDirection(rb.velocity).z * 3.6f; //Calculate currentSpeed
            //_source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(axleInfos[0].leftWheel.motorTorque * speed * Time.deltaTime / 5) / flatoutSpeed, pitchSpeed);//Calculate Engine Speed
            _source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(Input.GetAxis("Vertical") * speed * 150 * Time.deltaTime) / flatoutSpeed, pitchSpeed);
            float motor = maxMotorTorque * joystick2.Vertical; // maxMotorTorque * Input.GetAxis("Vertical");
            float steering = maxSteeringAngle * joystick.Horizontal; // Input.GetAxis("Horizontal");

            foreach (AxleInfo axleInfo in axleInfos)//get all the wheels from Inpsector 
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (axleInfo.motor && speed < maximumSpeed)//If below MaxSpeed add Torque
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
                else//If above MaxSpeed apply OverspeedTorque
                {
                    axleInfo.leftWheel.motorTorque = overSpeedTorque;
                    axleInfo.rightWheel.motorTorque = overSpeedTorque;
                }
                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);
            }



            rb.AddForce(-transform.up * speed * downforce); //AddDownforce 
           

            // Debug.Log(motor);
        }
        
       
    }
    
    private void LateUpdate()
    {
        speedometerTxt.SetText(speed.ToString("0") + " Km/h"); //uppdate Speedometer Text
    }
    private void Update()
    {
        
    }
}

