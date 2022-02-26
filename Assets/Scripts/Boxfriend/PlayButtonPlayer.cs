using Random = System.Random;
using UnityEngine;
using System.Collections.Generic;

namespace Boxfriend
{
    public class PlayButtonPlayer : MonoBehaviour
    {
        [SerializeField] private ChangeScene _sceneChanger;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private TMPro.TMP_Text _funnyText;

        [SerializeField] private int _minXPos, _maxXPos;
        [SerializeField] private int _minYPos, _maxYPos;

        [SerializeField] private List<string> _funnyPhrases;

        private Random _rng;

        private void Awake () => _rng = new Random();

        public void PlayClicked(int sceneNumber)
        {
            var rando = _rng.Next(_funnyPhrases.Count + 3);

            if(rando >= _funnyPhrases.Count)
            {
                _sceneChanger.MoveToScene(sceneNumber);
            }
            else
            {
                _rect.anchoredPosition = GetNewPosition();
                _funnyText.text = _funnyPhrases[rando];
            }
        }

        private Vector2 GetNewPosition()
        {
            var pos = _rect.anchoredPosition;
            pos.x = _rng.Next(_minXPos, _maxXPos);
            pos.y = _rng.Next(_minYPos, _maxYPos);

            return pos;
        }
    }
}
