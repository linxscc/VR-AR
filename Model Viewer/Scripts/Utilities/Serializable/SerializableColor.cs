using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class SerializableColor {


    //
    // 摘要:
    //     ///
    //     Alpha component of the color.
    //     ///
    public float a;
    //
    // 摘要:
    //     ///
    //     Blue component of the color.
    //     ///
    public float b;
    //
    // 摘要:
    //     ///
    //     Green component of the color.
    //     ///
    public float g;
    //
    // 摘要:
    //     ///
    //     Red component of the color.
    //     ///
    public float r;

    public SerializableColor() : this(1.0f, 1.0f, 1.0f, 1.0f) { }

    //
    // 摘要:
    //     ///
    //     Constructs a new Color with given r,g,b components and sets a to 1.
    //     ///
    //
    // 参数:
    //   r:
    //     Red component.
    //
    //   g:
    //     Green component.
    //
    //   b:
    //     Blue component.
    public SerializableColor(float r, float g, float b) : this(r, g, b, 1.0f) { }
    //
    // 摘要:
    //     ///
    //     Constructs a new Color with given r,g,b,a components.
    //     ///
    //
    // 参数:
    //   r:
    //     Red component.
    //
    //   g:
    //     Green component.
    //
    //   b:
    //     Blue component.
    //
    //   a:
    //     Alpha component.
    public SerializableColor(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    //public float this[int index] { get; set; }

    /*//
    // 摘要:
    //     ///
    //     Solid black. RGBA is (0, 0, 0, 1).
    //     ///
    public static Color black { get; }
    //
    // 摘要:
    //     ///
    //     Solid blue. RGBA is (0, 0, 1, 1).
    //     ///
    public static Color blue { get; }
    //
    // 摘要:
    //     ///
    //     Completely transparent. RGBA is (0, 0, 0, 0).
    //     ///
    public static Color clear { get; }
    //
    // 摘要:
    //     ///
    //     Cyan. RGBA is (0, 1, 1, 1).
    //     ///
    public static Color cyan { get; }
    //
    // 摘要:
    //     ///
    //     Gray. RGBA is (0.5, 0.5, 0.5, 1).
    //     ///
    public static Color gray { get; }
    //
    // 摘要:
    //     ///
    //     Solid green. RGBA is (0, 1, 0, 1).
    //     ///
    public static Color green { get; }
    //
    // 摘要:
    //     ///
    //     English spelling for gray. RGBA is the same (0.5, 0.5, 0.5, 1).
    //     ///
    public static Color grey { get; }
    //
    // 摘要:
    //     ///
    //     Magenta. RGBA is (1, 0, 1, 1).
    //     ///
    public static Color magenta { get; }
    //
    // 摘要:
    //     ///
    //     Solid red. RGBA is (1, 0, 0, 1).
    //     ///
    public static Color red { get; }
    //
    // 摘要:
    //     ///
    //     Solid white. RGBA is (1, 1, 1, 1).
    //     ///
    public static Color white { get; }
    //
    // 摘要:
    //     ///
    //     Yellow. RGBA is (1, 0.92, 0.016, 1), but the color is nice to look at!
    //     ///
    public static Color yellow { get; }
    //
    // 摘要:
    //     ///
    //     A version of the color that has had the gamma curve applied.
    //     ///
    public Color gamma { get; }
    //
    // 摘要:
    //     ///
    //     The grayscale value of the color. (Read Only)
    //     ///
    public float grayscale { get; }
    //
    // 摘要:
    //     ///
    //     A linear value of an sRGB color.
    //     ///
    public Color linear { get; }
    //
    // 摘要:
    //     ///
    //     Returns the maximum color component value: Max(r,g,b).
    //     ///
    public float maxColorComponent { get; }*/

    //
    // 摘要:
    //     ///
    //     Creates an RGB colour from HSV input.
    //     ///
    //
    // 参数:
    //   H:
    //     Hue [0..1].
    //
    //   S:
    //     Saturation [0..1].
    //
    //   V:
    //     Value [0..1].
    //
    //   hdr:
    //     Output HDR colours. If true, the returned colour will not be clamped to [0..1].
    //
    // 返回结果:
    //     ///
    //     An opaque colour with HSV matching the input.
    //     ///
    /*public static Color HSVToRGB(float H, float S, float V);*/
    //
    // 摘要:
    //     ///
    //     Creates an RGB colour from HSV input.
    //     ///
    //
    // 参数:
    //   H:
    //     Hue [0..1].
    //
    //   S:
    //     Saturation [0..1].
    //
    //   V:
    //     Value [0..1].
    //
    //   hdr:
    //     Output HDR colours. If true, the returned colour will not be clamped to [0..1].
    //
    // 返回结果:
    //     ///
    //     An opaque colour with HSV matching the input.
    //     ///
/*    public static Color HSVToRGB(float H, float S, float V, bool hdr);*/
    //
    // 摘要:
    //     ///
    //     Linearly interpolates between colors a and b by t.
    //     ///
    //
    // 参数:
    //   a:
    //
    //   b:
    //
    //   t:
/*    public static Color Lerp(Color a, Color b, float t);*/
    //
    // 摘要:
    //     ///
    //     Linearly interpolates between colors a and b by t.
    //     ///
    //
    // 参数:
    //   a:
    //
    //   b:
    //
    //   t:
    /*public static Color LerpUnclamped(Color a, Color b, float t);
    public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V);
    public override bool Equals(object other);
    public override int GetHashCode();*/
    //
    // 摘要:
    //     ///
    //     Returns a nicely formatted string of this color.
    //     ///
    //
    // 参数:
    //   format:
    //public override string ToString();
    //
    // 摘要:
    //     ///
    //     Returns a nicely formatted string of this color.
    //     ///
    //
    // 参数:
    //   format:
    //public string ToString(string format);

    /*public static Color operator +(Color a, Color b);
    public static Color operator -(Color a, Color b);
    public static Color operator *(Color a, Color b);
    public static Color operator *(Color a, float b);
    public static Color operator *(float b, Color a);
    public static Color operator /(Color a, float b);
    public static bool operator ==(Color lhs, Color rhs);
    public static bool operator !=(Color lhs, Color rhs);*/

    public static implicit operator SerializableColor(Color c)
    {
        return new SerializableColor(c.r, c.g, c.b, c.a);
    }
    public static implicit operator Color(SerializableColor c)
    {
        return new Color(c.r, c.g, c.b, c.a);
    }

    /*public static implicit operator SerializableColor(Vector4 v)
    {
        return (SerializableColor)((Color)v);
    }
    public static implicit operator Vector4(SerializableColor c)
    {
        return new Vector4(c.r, c.g, c.b, c.a);
    }*/

}
