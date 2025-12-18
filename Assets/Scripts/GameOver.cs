using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void OnEnable()
    {
        Gameplay _gamePlayScript = FindObjectOfType<Gameplay>();
        _gamePlayScript._applicationPause = false;
        GoogleMobileAdsDemoScript _googleAdsDemo = FindObjectOfType<GoogleMobileAdsDemoScript>();
        _googleAdsDemo.ClaimReward_star.SetActive(false);
    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
