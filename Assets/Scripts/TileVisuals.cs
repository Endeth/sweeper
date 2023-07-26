using UnityEngine;

namespace Boomsweeper
{
    public class TileVisuals : MonoBehaviour
    {
        static Vector3 emptyTileSize = new( 0.5f, 0.1f, 0.5f );
        private TextMesh _text;
        private string _bombSign = "B";
        private bool _empty = false;

        public void Init( bool hasMine, int neighboursWithMines )
        {
            _text = GetComponentInChildren<TextMesh>();

            if( hasMine )
                _text.text = _bombSign;
            else if( neighboursWithMines > 0 )
                _text.text = neighboursWithMines.ToString();
            else
                _empty = true;
        }

        public void Flip()
        {
            if( _empty )
                transform.localScale = emptyTileSize;

            _text.color = Color.black;
        }
    }
}
