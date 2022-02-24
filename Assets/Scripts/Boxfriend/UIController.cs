using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Boxfriend.Player;

namespace Boxfriend
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private RectMask2D _healthMask;

        private float _healthPieceSize;

        private void Awake ()
        {
            PlayerStats.OnStatsChange += UpdatePlayerUI;
            _healthPieceSize = _healthMask.canvasRect.width / 5;
        }

        private void UpdatePlayerUI(PlayerStats player)
        {
            var padding = _healthMask.padding;
            padding.z = (player.BaseHealth - player.Health) * _healthPieceSize;
            _healthMask.padding = padding;
        }

    }
}