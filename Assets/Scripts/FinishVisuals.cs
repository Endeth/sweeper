using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boomsweeper
{
    public class FinishVisuals : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text _victoryText;
        [SerializeField] private TMPro.TMP_Text _defeatText;

        public void OnFinish( bool victory )
        {
            if( victory )
                _victoryText.alpha = 255;
            else
                _defeatText.alpha = 255;
        }
    }
}
