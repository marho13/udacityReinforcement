﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using SocketIO;
using UnityStandardAssets.Vehicles.Car;
using System;
using System.Security.AccessControl;

public class CommandServer : MonoBehaviour
{
	public CarRemoteControl CarRemoteControl;
	public Camera FrontFacingCamera;
	private SocketIOComponent _socket;
	private CarController _carController;
    public bool frWheel = false;
    public bool flWheel = false;
    public bool brWheel = false;
    public bool blWheel = false;
    public bool divider = false;
    public bool Collide = false;
    public bool onRoad = true;
    public bool resetEnv = false;
    public bool taskComplete = false;
    public int straightCheckpoint = 0;
    public int nonStraightCheckpoint = 0;
    public CheckWheel checky;
    public checkpoint checkpoint;
    public float reward = 0.0f;
    public int offRoadInt = 0;
    public int taskCompletion = 0;
    public timeVariance timey;

    // Use this for initialization
    void Start()
	{
		_socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
		_socket.On("open", OnOpen);
		_socket.On("steer", OnSteer);
		_socket.On("manual", onManual);
        _socket.On("ready", OnReady);
		_carController = CarRemoteControl.GetComponent<CarController>();
	}

    void FixedUpdate()
    {
        checkpoint.checkpointComplete();
    }

    void OnOpen(SocketIOEvent obj)
	{
		Debug.Log("Connection Open");
        EmitTelemetry(obj);
	}

	// 
	void onManual(SocketIOEvent obj)
	{
        EmitTelemetry (obj);
	}

	void OnSteer(SocketIOEvent obj)
	{
        JSONObject jsonObject = obj.data;
		//    print(float.Parse(jsonObject.GetField("steering_angle").str));
		CarRemoteControl.SteeringAngle = float.Parse(jsonObject.GetField("steering_angle").str);
		CarRemoteControl.Acceleration = float.Parse(jsonObject.GetField("throttle").str);
        checky.wheelRunner();
        /*taskComplete = checkpoint.taskComplete();
        if (taskComplete) 
        {
            Debug.Log("Resetting");
                taskCompletion++;
            if (taskCompletion > 30)
            {
                taskCompletion = 0;
                checkpoint.increaseTask();
            }
            resetEnv = true;
        }
        */
        Collide = checkpoint.stuckCheck();
        wheelCheck();
        EmitTelemetry(obj);
	}

	void EmitTelemetry(SocketIOEvent obj)
	{
		UnityMainThreadDispatcher.Instance().Enqueue(() =>
		{
			print("Attempting to Send...");
			// send only if it's not being manually driven
			if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S))) {
				_socket.Emit("telemetry", new JSONObject());
			}
			else {
                // Collect Data from the Car
                Dictionary<string, string> data = new Dictionary<string, string>();
                data["steering_angle"] = _carController.CurrentSteerAngle.ToString("N4");
                data["throttle"] = _carController.AccelInput.ToString("N4");
                data["speed"] = _carController.CurrentSpeed.ToString("N4");
                data["image"] = Convert.ToBase64String(CameraHelper.CaptureFrame(FrontFacingCamera));
                data["reward"] = reward.ToString();
                data["checkpointStraight"] = straightCheckpoint.ToString();
                data["checkpointNonStraight"] = nonStraightCheckpoint.ToString();
                data["onRoad"] = onRoad.ToString();
                data["resetEnv"] = resetEnv.ToString();
                _socket.Emit("telemetry", new JSONObject(data));

                

                if (resetEnv) {
                    Debug.Log("reset");
                    checkpoint.reset();
                    resetCheckpoints();
                    reward = 0.0f;
                    timey.slowDown();
                    resetEnv = false;
                    /*if (!taskComplete)
                    {
                        if (taskCompletion > 0)
                        {
                            taskCompletion--;
                        }
                    }*/
                }

            }
		});
	}

    void OnReady(SocketIOEvent obj) {
        JSONObject jsonObject = obj.data;
        resetEnv = false;
        checkpoint.reset();
        resetCheckpoints();
        offRoadInt = 0;
        timey.speedUp();
        Debug.Log("Speeding up");
    }

    public void rewardCheckpoint()
    { 
        nonStraightCheckpoint++;
        reward += 7.0f;
    }

    public void rewardStraight()
    {
        straightCheckpoint++;
        reward += 2.0f;
    }

    void resetCheckpoints()
    {
        straightCheckpoint = 0;
        nonStraightCheckpoint = 0;
    }

    void stillReward()
    {
        reward -= 0.001f;
    }

    void offRoad()
    {
        reward -= 0.01f;
    }

    void episodeReward()
    {
        reward -= 0.5f;
    }

    void wheelCheck()
    {
        if (frWheel | flWheel | blWheel | brWheel | divider)
        {
            offRoadInt++;
            onRoad = false;

            if (offRoadInt >= 300)
            {
                this.episodeReward();
                
                resetEnv = true;
                offRoadInt = 0;
            }

            else
            {
                this.offRoad();
            }
        }

        else
        {

            offRoadInt = 0;
            onRoad = true;

            if (Collide)
            {
                this.episodeReward();
                Collide = false;

                resetEnv = true;
            }

            else
            {
                this.stillReward();
            }
        }
    }
}