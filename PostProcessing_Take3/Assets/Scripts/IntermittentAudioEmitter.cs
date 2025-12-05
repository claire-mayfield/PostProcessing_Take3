using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]

public class IntermittentAudioEmitter : MonoBehaviour
{
    [Header("Audio Clips")]

    //array of audio files
    [SerializeField] private AudioClip[] _clips;
    int _currentClipIndex;

    [Space(5)]
    [Header("Randomization Attributes")]

    //Randomize amplitude
    [SerializeField] private bool _randomizeAmplitude;
    [Range(0.75f, 1.0f)] float _minAmplitude;
    [Range(0.75f, 1.0f)] float _maxAmplitude;

    // Randomize pitch
    [SerializeField] private bool _randomizePitch;
    [Range(0.5f, 1.0f)] float _minPitch;
    [Range(1.0f, 2.0f)] float _maxPitch;

    // Randomize LPF frequency
    [SerializeField] private bool _randomizeLpfFrequency;
    [Range(100.0f, 1000.0f)] float _minLpfFreq;
    [Range(1000.0f, 2000.0f)] float _maxLpfFreq;

    // Randomize time between clips
    [Range(0.0f, 5.0f)] [SerializeField] float _maxGapBetweenClips = 2.0f;

    [Space(5)]
    [Header("Position Attributes")]
    [Range(20.0f, 100.0f)] [SerializeField] float _maxDistance;

    [Space(5)]
    [Header("Player Info")]
    [SerializeField] Transform _playerTransform;

    // References to componenets
    AudioSource _source;
    AudioLowPassFilter _lpf;

   

    void Start()
    {
        _source = GetComponent<AudioSource>();
        _lpf = GetComponent<AudioLowPassFilter>();

        // Init audio properties
        _source.loop = false;
        _source.spatialBlend = 1.0f;
        _source.maxDistance = _maxDistance;

        SetAudioProperties();
        
    }

    void SetAudioProperties()
    {
        // load sound
        _currentClipIntex = LoadRandomIndex(_clips.Length, _currentClipIndex);
        _source.clip = _clips[_currentClipIndex];

        // set random attributes

        if (_randomizeAmplitude)
            _source.volume = Random.Range(_minAmplitude, _maxAmplitude);

        if (_randomizePitch)
            _source.pitch = Random.Range(_minPitch, _maxPitch);

        if (_randomizeLpfFrequency)
            _lpf.cutoffFrequency = Random.Range(_minLpfFreq, _maxLpfFreq);

        transform.position = GenerateRelativeRandomPosition
            (_playerTransform);

        //Play!

        StartCoroutine(PlaySound)
    }

    int LoadRandomIndex(int arrayLength, int prevIndex)
    {
        int currentIndex;

        do
        {
            currentIndex = Random.Range(0, arrayLength);
        } while (prevIndex == currentIndex);

        return currentIndex;
    }

    Vector3 GenerateRelativeRandomPosition(Transform playerTransform)
    {
        float angleX = Random.Range(0.0f, 2.0f * Mathf.PI);
        float angleY;
        float angleZ = Random.Range(0.0f, 2.0f * Mathf.PI);

        float raduis = Random.Range(1.0f, _maxDistance);

        return new Vector3(
            playerTransform.position.x + Mathf.Cos(angleX) * radius,
            0,
            playerTransform.position.y + Mathf.Sin(angleY) * radius, //Y MUST use Sine for overhead sounds
            playerTransofmr.position.z + Mathf.Sin(angleZ) * radius

            );

    }


    IEnumerator PlaySound()
    {
        _source.Play();


        yield return new WaitForSeconds(
            Random.Range(_source.clip.length, _source.clip.length + _maxGapBetweenClips

            );
    }
}
