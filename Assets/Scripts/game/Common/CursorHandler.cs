
using UnityEngine;

namespace game.Common
{
    public class CursorHandler : MonoBehaviour
    {
        [SerializeField] public Texture2D shooting;

        public void SetCursor(Texture2D texture)
        {
            Cursor.SetCursor(texture, new Vector2(0, 0), CursorMode.Auto);
        }

        public void ClearCursor()
        {
            Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
        }

        public void SetDefaultCursor()
        {
            SetCursor(shooting);
        }
    }
}
