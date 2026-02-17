using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    public Transform wheelFL;
    public Transform wheelFR;
    public Transform wheelRL;
    public Transform wheelRR;

    public float motorForce = 800f;
    public float steerForce = 30f;
    public float breakForce = 5000f;

    float horizontalInput;
    float verticalInput;
    bool isBreaking;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.8f, 0);
        rb.angularDrag = 2f;  
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);

        updateWheelMeshes();
    }

    private void FixedUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void Move()
    {
        rearLeft.motorTorque = verticalInput * motorForce;
        rearRight.motorTorque = verticalInput * motorForce;
    }   

    void Steer()
    {
        float steerAngle = steerForce * horizontalInput;
        frontLeft.steerAngle = steerAngle;
        frontRight.steerAngle = steerAngle;
    }

    void Brake()
    {
        float brake = isBreaking ? breakForce : 0f;
        frontLeft.brakeTorque = brake;
        frontRight.brakeTorque = brake;
        rearLeft.brakeTorque = brake;
        rearRight.brakeTorque = brake;
    }

    void updateWheelMeshes()
    {
        updateWheel(frontLeft, wheelFL);
        updateWheel(frontRight, wheelFR);
        updateWheel(rearLeft, wheelRL);
        updateWheel(rearRight, wheelRR);
    }

    void updateWheel(WheelCollider col,Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;

        col.GetWorldPose(out pos, out rot);
        wheel.position = pos;
        wheel.rotation = rot;
    }

}
