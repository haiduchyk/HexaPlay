using UnityEngine;

public class DrawHex
{
    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    static readonly float radius = 0.5f;
    static public Vector3 Position(int x, int y) => new Vector3(
            HexVerticalSpacing() * y,
            HexHorizontalSpacing() * (x + y / 2f),
            1
        );
    static float HexHeight() => radius * 2;
    static float HexWidth() => WIDTH_MULTIPLIER * HexHeight();
    static float HexVerticalSpacing() => HexHeight() * 0.75f;
    static float HexHorizontalSpacing() => HexWidth();

}
