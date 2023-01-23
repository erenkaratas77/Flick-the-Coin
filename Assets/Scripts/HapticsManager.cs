using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class HapticsManager : MonoBehaviour
{
	public static void Haptic(HapticTypes type, bool defaultToRegularVibrate = false, bool alsoRumble = false, MonoBehaviour coroutineSupport = null, int controllerID = -1)
	{
		if (GameManager.Vibration == 1)
		{
			MMVibrationManager.Haptic(type, defaultToRegularVibrate, alsoRumble, coroutineSupport, controllerID);
		}
	}
	public static void ContiniousHaptic(float intensity = 0.3f,float sharpness = 0.65f,float time = 1f)
    {
		StopHaptics();
        if (GameManager.Vibration == 1)
        {
			MMVibrationManager.ContinuousHaptic(intensity, sharpness, time);
		}
	}
	public static void StopHaptics()
    {
		MMVibrationManager.StopAllHaptics();
    }
}
