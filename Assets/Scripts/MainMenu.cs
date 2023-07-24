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

        void OnOptionsChange()
        {
            //GameOptions.Instance.SetOptions;
        }

        void Update()
        {
            if( Input.GetMouseButton( 1 ) )
                NewGame();
        }
    }
}
