using System;
using UnityEngine;

namespace Boxfriend.Player
{
    public class PlayerWinCondition : MonoBehaviour
    {
        private PlayerStats _stats;

        public static Action OnPlayerWin;
        private void Awake ()
        {
            _stats = GetComponent<PlayerStats>();
            Item.OnItemPickup += CollectCoin;
        }

        private void CollectCoin(Item item)
        {
            if(item is Coin)
            {
                _stats.Kill();
                OnPlayerWin?.Invoke();
            }
        }
    }
}
