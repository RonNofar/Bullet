using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet
{
    public class PlayerUIControl : MonoBehaviour
    {

        [SerializeField]
        private Slider healthSlider;
        [SerializeField]
        private Slider staminaSlider;

        private void FixedUpdate()
        {
            healthSlider.value = Player.PlayerController.Instance.GetHealthRatio();
            staminaSlider.value = Player.PlayerController.Instance.GetStaminaRatio();
        }
    }
}
