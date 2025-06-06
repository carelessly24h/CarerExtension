namespace CarerExtension.IO.Pdf;

/// <summary>
/// マージンを管理する構造体
/// </summary>
/// <param name="Top">上方向のマージン</param>
/// <param name="Left">左方向のマージン</param>
/// <param name="Right">右方向のマージン</param>
/// <param name="Bottom">下方向のマージン</param>
public readonly record struct Margin(double Top, double Left, double Right, double Bottom);
