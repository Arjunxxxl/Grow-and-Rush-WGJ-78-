using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{   
    [System.Serializable]
    public class InputData
    {
        public float horizontal;
        public float vertical;
        public bool jump;

        public bool isGrounded;

        public float mouseX;
        public float mouseY;
    }
    [SerializeField]
    public InputData _data;

    [System.Serializable]
    public class InputSettings
    {
        public Vector3 dxn;
        public float speed = 5f;
        public float jumpForce = 10f;

        public float lookSpeed = 20f;
        public float maxAngle = 40f;
        public float minAngle = 330f;

        public Quaternion rot;
        public Quaternion camRot;
        public float x_angle;
        public float y_angle;
        public float lookSensitivityX = 1f;
        public float lookSensitivityY = 2f;
        public float tempX;
    }
    [SerializeField]
    public InputSettings _inputSettings;

    UserInput _input;
    //CharacterController _controller;
    Rigidbody _rb;
    Camera mainCam;

    Weapon weapon;

    AudioSource s;
    

    public GameObject Gameover;
    //public 
    

    // Start is called before the first frame update
    void Start()
    {
        _input = UserInput.Instance;
        //_controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;

        weapon = Weapon.Instance;

        Gameover.SetActive(false);

        s = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Jump();

        //Debug.Log();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void GetInput()
    {
        _data.horizontal = _input._inputData.horizontal;
        _data.vertical = _input._inputData.vertical;
        _data.jump = _input._inputData.jump;

        if(_data.horizontal != 0 || _data.vertical != 0)
        {
            if (!s.isPlaying && _data.isGrounded)
            {
                s.Play();
            }
        }
        else
        {
            s.Stop();
        }

        _inputSettings.dxn = new Vector3(_data.horizontal, 0f, _data.vertical).normalized;
    }

    void MovePlayer()
    {
        transform.Translate(_inputSettings.dxn * Time.deltaTime * _inputSettings.speed, Space.Self);

        
    }

    void Jump()
    {
        if (_data.jump && _data.isGrounded)
        {
            //Debug.Log(_data.jump);
            _rb.AddForce(_inputSettings.jumpForce * Vector3.up, ForceMode.Impulse);
        }
    }

    void RotatePlayer()
    {
        _data.mouseX = Input.GetAxis("Mouse X");
        _data.mouseY = Input.GetAxis("Mouse Y");

        _inputSettings.y_angle = -_data.mouseY * 180f / 100f * _inputSettings.lookSensitivityY;
        _inputSettings.x_angle = _data.mouseX * 180f / 2f * _inputSettings.lookSensitivityX;
        
        _inputSettings.x_angle += transform.localRotation.eulerAngles.y;
        
        _inputSettings.x_angle = Mathf.Repeat(_inputSettings.x_angle, 360f);

        _inputSettings.rot = Quaternion.Euler(0f, _inputSettings.x_angle, 0f);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _inputSettings.rot, Time.deltaTime * _inputSettings.lookSpeed);

        //_inputSettings.y_angle += mainCam.transform.localRotation.eulerAngles.x;

        /*if (_inputSettings.y_angle < 0f)
        {
            _inputSettings.y_angle = 360f - _inputSettings.y_angle;

        }
        else
        {
            _inputSettings.y_angle = _inputSettings.y_angle > _inputSettings.maxAngle ? _inputSettings.maxAngle : _inputSettings.y_angle;
        }

        _inputSettings.camRot = Quaternion.Euler(_inputSettings.y_angle * _inputSettings.lookSensitivity, 
                                            mainCam.transform.localRotation.eulerAngles.y, mainCam.transform.localRotation.eulerAngles.z);
        mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, _inputSettings.camRot, Time.deltaTime * _inputSettings.lookSpeed);*/

    
        _inputSettings.camRot = Quaternion.AngleAxis(_inputSettings.y_angle, Vector3.right);
        mainCam.transform.localRotation = mainCam.transform.localRotation * _inputSettings.camRot;

        if(mainCam.transform.localRotation.eulerAngles.x > 40f && mainCam.transform.localRotation.eulerAngles.x < 300f)
        {
            _inputSettings.tempX = 40f;
        }
        else if(mainCam.transform.localRotation.eulerAngles.x > 300f && mainCam.transform.localRotation.eulerAngles.x <= 330f)
        {
            _inputSettings.tempX = 330f;
        }
        else
        {
            _inputSettings.tempX = mainCam.transform.localRotation.eulerAngles.x;
        }
        mainCam.transform.localRotation = Quaternion.Euler(_inputSettings.tempX,
                                        mainCam.transform.localRotation.eulerAngles.y, mainCam.transform.localRotation.eulerAngles.z);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _data.isGrounded = true;
        }

        if (collision.gameObject.tag == "Weapon")
        {
            weapon.settings.carryingAmmo = 45;
        }

        if (collision.gameObject.tag == "Done")
        {
            Gameover.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _data.isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _data.isGrounded = false;
        }
    }



}
