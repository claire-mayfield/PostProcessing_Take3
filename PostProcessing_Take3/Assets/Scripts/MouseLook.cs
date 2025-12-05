using UnityEngine;

public class MouseLook : MonoBehaviour
{

    enum RotationAxes
    {

        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2,
    }


    [Header("Degrees of Freedom")]
    [SerializeField] RotationAxes _axes = RotationAxes.MouseXandY;

    [Space(5)]
    [Header("Sensitivity")]
    [SerializeField] float _sensitivityHorizontal = 9.0f;
    [SerializeField] float _sensitivityVertical = 9.0f;


    // Limits vertical movement
    [Space(5)]
    [Header("Constraints")]
    [SerializeField] float _minVerticalAngle = -45.0f;
    [SerializeField] float _maxVerticalAngle = 45.0f;

    private float _verticalRotation = 0.0f;



    void Start()
    {
        // Makes the script work with a Rigidbody componenet 
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true;
        }
        
    }

    
    void Update()
    {

        switch (_axes)
        {
            // X Axis
            case RotationAxes.MouseX:
                transform.Rotate(
                    0.0f,
                    Input.GetAxis("Mouse X") * _sensitivityHorizontal,
                    0.0f);
                break;


            // Y Axis
            case RotationAxes.MouseY:
                _verticalRotation -= Input.GetAxis("Mouse Y")
                    * _sensitivityVertical;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);

                float horizontalRotation = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(
                    _verticalRotation, horizontalRotation, 0.0f
                );

                break;


            // X and Y Axis Sheikah Slate!!!
            case RotationAxes.MouseXandY:

                _verticalRotation -= Input.GetAxis("Mouse Y") * _sensitivityVertical;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);

                float deltaX = Input.GetAxis("Mouse X") * _sensitivityHorizontal;
                horizontalRotation = transform.localEulerAngles.y + deltaX;

                transform.localEulerAngles = new Vector3(
                    _verticalRotation, horizontalRotation, 0.0f
                );

                break;
        }
        
    }
}
