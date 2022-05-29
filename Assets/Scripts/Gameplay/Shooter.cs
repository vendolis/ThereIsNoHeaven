using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform cannonLocation;
    
    [SerializeField] private GameObject projectileType;

    [SerializeField] private AudioCueEventChannelSO sfxAudioChannel;
    [SerializeField] private AudioCueSO shooterSound;
    [SerializeField] private AudioConfigurationSO _audioConfig;
        
    [SerializeField] private float repeatValue = 0.8f;

    private bool isShooting = false;

    private float timeSinceLastShoot = 100f; // To be able to shoot from the beginning.

    private void Start()
    {
        inputReader.AttackEvent += StartShooting;
        inputReader.AttackCanceledEvent += StopShooting;
    }

    private void OnDestroy()
    {
        inputReader.AttackEvent -= StartShooting;
        inputReader.AttackCanceledEvent -= StopShooting;
    }

    private void StopShooting()
    {
        isShooting = false;
        timeSinceLastShoot = repeatValue;
    }

    private void StartShooting()
    {
        isShooting = true;
        ShootProjectile();
    }


    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        if (timeSinceLastShoot < repeatValue)
        {
            timeSinceLastShoot += Time.deltaTime;
            return;
        }

        timeSinceLastShoot = 0f;
        sfxAudioChannel.RaisePlayEvent(shooterSound, _audioConfig);
        Instantiate(projectileType, cannonLocation.position, cannonLocation.rotation);
        
    }
}
