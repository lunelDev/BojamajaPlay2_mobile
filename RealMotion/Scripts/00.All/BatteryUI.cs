using UnityEngine;
using UnityEngine.UI;


public class BatteryUI : MonoBehaviour
{
    public Text BatteryLevel;
    private void OnGUI()
    {
     //W   GetBatteryLevel1();

    }
    private void Update()
    {
        BatteryLevel.text = GetBatteryLevel().ToString()+" %";
    }
    public static float GetBatteryLevel()
    {
#if UNITY_IOS
        UIDevice device = UIDevice.CurrentDevice();
        device.batteryMonitoringEnabled = true; // need to enable this first
        Debug.Log("Battery state: " + device.batteryState);
        Debug.Log("Battery level: " + device.batteryLevel);
        return device.batteryLevel*100;
#elif UNITY_ANDROID

        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    if (null != unityPlayer)
                    {
                        using (AndroidJavaObject currActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                        {
                            if (null != currActivity)
                            {
                                using (AndroidJavaObject intentFilter = new AndroidJavaObject("android.content.IntentFilter", new object[] { "android.intent.action.BATTERY_CHANGED" }))
                                {
                                    using (AndroidJavaObject batteryIntent = currActivity.Call<AndroidJavaObject>("registerReceiver", new object[] { null, intentFilter }))
                                    {
                                        int level = batteryIntent.Call<int>("getIntExtra", new object[] { "level", -1 });
                                        int scale = batteryIntent.Call<int>("getIntExtra", new object[] { "scale", -1 });

                                        // Error checking that probably isn't needed but I added just in case.
                                        if (level == -1 || scale == -1)
                                        {
                                            return 50f;
                                        }
                                        return ((float)level / (float)scale) * 100.0f;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.Log("Failed to read battery power; " + ex.Message);
            }
        }

        return 100;
#endif
    }
    int GetBatteryLevel1()
    {
        try
        {
            string CapacityString = System.IO.File.ReadAllText("/sys/class/power_supply/battery/capacity");
            return int.Parse(CapacityString);
        }
        catch (System.Exception e)
        {
            Debug.Log("Failed to read battery power; " + e.Message);
        }

        return -1; //if no value is found
    }

}


