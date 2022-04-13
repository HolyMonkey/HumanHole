using UnityEngine;

public class CursorService : ICursorService
{
    private Texture2D _cursor;

    public CursorService()
    {
        _cursor = new Texture2D(0, 0);
    }

    public void Enable()
    {
        Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.Auto);
    }

    public void Disable()
    {
        Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}