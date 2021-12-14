using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{   
    AudioSource _AudioSource;
    public float[] _samples = new float[512];
    // Start is called before the first frame update
    void Start()
    {
        _AudioSource=GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        _AudioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
}
