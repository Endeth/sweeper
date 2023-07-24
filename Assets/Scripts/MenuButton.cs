using UnityEngine;

namespace Boomsweeper
{
    public class MenuButton : MonoBehaviour
    {
        public void OnClick()
        {
            if( GameState.Instance.active )
                GameState.Instance.LoadMainMenu();
        }
    }
}
