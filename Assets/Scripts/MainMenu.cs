using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boomsweeper
{
    public class MainMenu : MonoBehaviour
    {
        public void NewGame()
        {
            SceneManager.LoadScene( "GameScene" );
        }
    }
}
