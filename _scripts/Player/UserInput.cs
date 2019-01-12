using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{   
    [System.Serializable]
    public class InputData
    {
        public float horizontal;
        public float vertical;
        public bool jump;
        public bool shoot;
    }
    [SerializeField]
    public InputData _inputData;

    #region SingleTon
    public static UserInput Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        _inputData.horizontal = Input.GetAxisRaw("Horizontal");
        _inputData.vertical = Input.GetAxisRaw("Vertical");

        _inputData.jump = false;

        if (Input.GetButtonDown("Jump"))
        {
            _inputData.jump = true;
        }

        _inputData.shoot = false;

        if (Input.GetButtonDown("Fire1"))
        {
            _inputData.shoot = true;
        }

        //Debug.Log(_inputData.jump);
    }

    

}
