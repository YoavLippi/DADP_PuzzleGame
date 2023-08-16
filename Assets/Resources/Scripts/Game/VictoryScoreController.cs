using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Resources.Scripts.Game
{
    public class VictoryScoreController : MonoBehaviour
    {
        [Header("Moves Required")] 
        
        [SerializeField] private int threeStars;

        [SerializeField] private int twoStars;

        [SerializeField] private int oneStar;

        [Header("Integrated Objects")] 
        
        [SerializeField] private GameObject starOne;

        [SerializeField] private GameObject starTwo;

        [SerializeField] private GameObject starThree;

        [SerializeField] private TextMeshProUGUI winText;
        
        [Header("Sundry information")]
        
        [SerializeField] private Color starWinColor;

        [SerializeField] private Color starBaseColor;

        [SerializeField] private LayerMask gearLayer;

        private void DisableColliders()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Vector2.zero, Mathf.Infinity, gearLayer);

            foreach (var coll in colliders)
            {
                coll.enabled = false;
            }
        }
        public void ShowVictory(int score)
        {
            DisableColliders();
            starOne.GetComponent<SpriteRenderer>().color = starBaseColor;
            starTwo.GetComponent<SpriteRenderer>().color = starBaseColor;
            starThree.GetComponent<SpriteRenderer>().color = starBaseColor;
            //Based on the number of moves, we display a different number of stars
            //if there are lower moves, we show more stars
            int scoreRating = 0;
            if (score <= oneStar)
            {
                starOne.GetComponent<SpriteRenderer>().color = starWinColor;
                scoreRating++;
            }
            
            if (score <= twoStars)
            {
                starTwo.GetComponent<SpriteRenderer>().color = starWinColor;
                scoreRating++;
            }
            
            if (score <= threeStars)
            {
                starThree.GetComponent<SpriteRenderer>().color = starWinColor;
                scoreRating++;
            }

            switch (scoreRating)
            {
                case 3 :
                    winText.text = "";
                    break;
                case 2 :
                    winText.text = $"Moves needed for next star: {threeStars}";
                    break;
                case 1 :
                    winText.text = $"Moves needed for next star: {twoStars}";
                    break;
                case 0 :
                    winText.text = $"Moves needed for next star: {oneStar}";
                    break;
            }
        }

        private void OnDisable()
        {
            Destroy(this.gameObject);
        }
    }
}
