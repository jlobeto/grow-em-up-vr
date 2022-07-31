using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugWindow : MonoBehaviour
{
    [SerializeField] private InputActionReference leftThumbstickPressedInputAction;
    [SerializeField] private InputActionReference rightThumbstickPressedInputAction;
    public GameObject panel;
    public TMP_Text text;

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void OnEnable()
    {
        Application.logMessageReceived += Log;
        leftThumbstickPressedInputAction.action.performed += LeftThumbstickPressed;
        rightThumbstickPressedInputAction.action.performed += RightThumbstickPressed;
        
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= Log;
        leftThumbstickPressedInputAction.action.performed -= LeftThumbstickPressed;
        rightThumbstickPressedInputAction.action.performed -= RightThumbstickPressed;

    }

    private void RightThumbstickPressed(InputAction.CallbackContext obj)
    {
        if (panel.activeSelf)
        {
            text.text = "";
        }
    }

    private void LeftThumbstickPressed(InputAction.CallbackContext obj)
    {
        panel.SetActive(!panel.activeSelf);
    }

    private void Log(string log, string stacktrace, LogType type)
    {
        var result = "<color={0}>{1}</color> \n";
        switch (type)
        {
            case LogType.Error:
                result = string.Format(result, "red", log + "---" + stacktrace);
                break;
            case LogType.Warning:
                result = string.Format(result, "yellow", log);
                break;
            case LogType.Log:
                result = string.Format(result, "white", log);
                break;
            case LogType.Exception:
                result = string.Format(result, "red", log + "---" + stacktrace);
                break;
        }

        text.text = text.text.Insert(0, result);
    }
}
