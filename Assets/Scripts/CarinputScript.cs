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
    [SerializeField] float lowSpeed = 50f;
    [SerializeField] float lowSpeedmultiplier = 500f;
    [SerializeField] float maximumSpeed = 130f;
    [SerializeField] float overSpeedDevider = 0.3f;
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
            _source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs((Input.GetAxis("Vertical") + 3) * speed * 50 * Time.deltaTime) / flatoutSpeed, pitchSpeed);

            if (speed > 90) { maxSteeringAngle = 20; } else { maxSteeringAngle = 35; }
            //_source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(speed/2) / flatoutSpeed, pitchSpeed); //Calculate EnginePith
            //_source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs(axleInfos[0].leftWheel.motorTorque  *speed /5 * Time.deltaTime) / flatoutSpeed, pitchSpeed);

            

            float motor = maxMotorTorque *  Input.GetAxis("Vertical"); //AddTorque Normal speed from VerticalAxis
            float motorLowSpeed = maxMotorTorque * lowSpeedmultiplier * Input.GetAxis("Vertical") ; //add power on lowespeeds 
            float motoroverSpeed = maxMotorTorque * overSpeedDevider * Input.GetAxis("Vertical"); //lower power when going fast.

            float steering = maxSteeringAngle *  Input.GetAxis("Horizontal"); //Steer HorizontalAxis

            foreach (AxleInfo axleInfo in axleInfos) //get all the wheels from Inpsector 
            {

                if (axleInfo.steering) //Wheels that are marked for steering
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (speed < -50) //If reversing
                {
                    axleInfo.leftWheel.motorTorque = motoroverSpeed;
                    axleInfo.rightWheel.motorTorque = motoroverSpeed;
                  //  Debug.Log("motoroverSpeed" + motoroverSpeed);

                }

                else if ( speed < lowSpeed)//If below normal speed add extra Torque
                {
                    axleInfo.leftWheel.motorTorque = motorLowSpeed;
                    axleInfo.rightWheel.motorTorque = motorLowSpeed;
                   // Debug.Log("lowspeed" + motorLowSpeed);

                }
                 
                else if (speed > maximumSpeed ) //If overspeeding
                    {
                        axleInfo.leftWheel.motorTorque = motoroverSpeed;
                        axleInfo.rightWheel.motorTorque = motoroverSpeed;
                       // Debug.Log("motoroverSpeed" + motoroverSpeed);

                    }
                

                else //normal speed
                 
                    {
                   axleInfo.leftWheel.motorTorque = motor;
                   axleInfo.rightWheel.motorTorque = motor;
                   // Debug.Log("normalSpeed" + motor);
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
            
            _source.pitch = Mathf.Lerp(_source.pitch, minPitch + Mathf.Abs((Input.GetAxis("Vertical") + 3) * speed * 50 * Time.deltaTime) / flatoutSpeed, pitchSpeed);

            if (speed > 90) { maxSteeringAngle = 20; } else { maxSteeringAngle = 35; }
            


            float motor = maxMotorTorque * joystick2.Vertical; //AddTorque Normal speed from VerticalAxis
            float motorLowSpeed = maxMotorTorque * lowSpeedmultiplier * joystick2.Vertical; //add power on lowespeeds 
            float motoroverSpeed = maxMotorTorque * overSpeedDevider * joystick2.Vertical; //lower power when going fast.

            float steering = maxSteeringAngle * joystick.Horizontal; //Steer HorizontalAxis

            foreach (AxleInfo axleInfo in axleInfos) //get all the wheels from Inpsector 
            {

                if (axleInfo.steering) //Wheels that are marked for steering
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }
                if (speed < -50) //If reversing
                {
                    axleInfo.leftWheel.motorTorque = motoroverSpeed;
                    axleInfo.rightWheel.motorTorque = motoroverSpeed;
                    //Debug.Log("motoroverSpeed" + motoroverSpeed);

                }

                else if (speed < lowSpeed)//If below normal speed add extra Torque
                {
                    axleInfo.leftWheel.motorTorque = motorLowSpeed;
                    axleInfo.rightWheel.motorTorque = motorLowSpeed;
                   // Debug.Log("lowspeed" + motorLowSpeed);

                }

                else if (speed > maximumSpeed) //If overspeeding
                {
                    axleInfo.leftWheel.motorTorque = motoroverSpeed;
                    axleInfo.rightWheel.motorTorque = motoroverSpeed;
                   // Debug.Log("motoroverSpeed" + motoroverSpeed);

                }


                else //normal speed

                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                   // Debug.Log("normalSpeed" + motor);
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
        speedometerTxt.SetText(Mathf.Abs(speed).ToString("0") + " Km/h"); //uppdate Speedometer Text
    }
    private void Update()
    {
        
    }
}

