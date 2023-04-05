using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarManager : MonoBehaviour
{
    //public Image powerBarImage;
    public PlayerController player;
    public Slider slider;
    public void UpdatePowerBar()
    {
        slider.value = player.currentJump / player.maxJump;
    }
}