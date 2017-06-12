using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bullet.UI
{
    public class GUIManager : MonoBehaviour
    {
        [SerializeField]
        private Player.PlayerController player;
        [SerializeField]
        private Text score;

        void FixedUpdate()
        {
            score.text = player.score.ToString();
        }
    }
}
