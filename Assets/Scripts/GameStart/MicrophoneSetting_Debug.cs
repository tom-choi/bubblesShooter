using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MicrophoneSetting_Debug : MonoBehaviour
{
    public TMP_Dropdown MPdevices;
    public Microphone[] MP;
    // Start is called before the first frame update
    void Start()
    {
        MPdevices.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < Microphone.devices.Length; i++)
        {
            string option = Microphone.devices[i];
            options.Add("ChooseYourMic");
            options.Add(option);

        }
 
 
        MPdevices.AddOptions(options);
        MPdevices.RefreshShownValue();

    }

    // Update is called once per frame
    void Update()
    {
            print(MPdevices.options.Count);
            print(MPdevices.options.ToString());
        
    }
}
