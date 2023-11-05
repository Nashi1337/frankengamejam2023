using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource meow;
    [SerializeField] AudioSource meow2;
    [SerializeField] AudioSource exerted;
    [SerializeField] AudioSource grunt;
    [SerializeField] AudioSource grunt2;
    [SerializeField] AudioSource grunt3;
    [SerializeField] AudioSource grunt4;
    [SerializeField] AudioSource grunt5;
    [SerializeField] AudioSource grunt6;
    [SerializeField] AudioSource dinoChewing;

    private static AudioManager _instance = null;

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        SetupSingleton();
    }

    /// <summary>
    /// Destroys this TileSpawningManger if there is already one registered.
    /// If not, this TileSpawningManger becomes the globally available instance.
    /// </summary>
    private void SetupSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DinoGrunt()
    {
        grunt6.Play();
    }

    public void CatMeow()
    {
        int randomValue = Random.Range(0, 2); // Generates either 0 or 1
        if (randomValue == 0)
        {
            meow.Play();
        }
        else if (randomValue == 1)
        {
            meow2.Play();
        }
    }

    public void DinoChew()
    {
        dinoChewing.Play();
    }

    public void PlayerExerted()
    {
        if (exerted.isPlaying)
        {
            exerted.Stop();
        }
        else
        {
            exerted.Play();
        }
    }
}
