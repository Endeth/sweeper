using UnityEngine;

namespace Boomsweeper
{
    public class UIOptions : MonoBehaviour
    {
        public void SetBoardWidth( string w )
        {
            GameOptions.Instance.SetBoardWidth( w );
        }

        public void SetBoardHeight( string h )
        {
            GameOptions.Instance.SetBoardHeight( h );
        }

        public void SetMinesCount( string count )
        {
            GameOptions.Instance.SetMinesCount( count );
        }
    }
}
