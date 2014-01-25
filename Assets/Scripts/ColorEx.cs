using UnityEngine;
using System.Collections;

public static class ColorEx {



}


public struct ColorRYB {
	public ColorRYB( float red, float yellow, float blue ) {
		this.red = Mathf.Clamp01(red);
		this.yellow = Mathf.Clamp01(yellow);
		this.blue = Mathf.Clamp01(blue);
	}

	private float red;
	private float yellow;
	private float blue;

	public Color ToRGB() {
		float R = this.red*this.red*(3f-this.red-this.red);
		float Y = this.yellow*this.yellow*(3f-this.yellow-this.yellow);
		float B = this.blue*this.blue*(3f-this.blue-this.blue);

		return new Color( 1.0f + B * ( R * (0.337f + Y * -0.137f) + (-0.837f + Y * -0.163f) ),
		                 1.0f + B * ( -0.627f + Y * 0.287f) + R * (-1.0f + Y * (0.5f + B * -0.693f) - B * (-0.627f) ),
		                 1.0f + B * (-0.4f + Y * 0.6f) - Y + R * ( -1.0f + B * (0.9f + Y * -1.1f) + Y ) );
	}

	public static ColorRYB Lerp( ColorRYB from, ColorRYB to, float lerp ) {
		return new ColorRYB( Mathf.Lerp(from.red, to.red, lerp),
		                     Mathf.Lerp(from.yellow, to.yellow, lerp),
		                     Mathf.Lerp(from.blue, to.blue, lerp) );
	}

	public static ColorRYB Red {
		get { return new ColorRYB( 1f, 0f, 0f ); }
	}

	public static ColorRYB Yellow {
		get { return new ColorRYB( 0f, 1f, 0f ); }
	}

	public static ColorRYB Blue {
		get { return new ColorRYB( 0f, 0f, 1f ); }
	}

	public static ColorRYB Green {
		get { return new ColorRYB( 0f, 1f, 1f ); }
	}

	public static ColorRYB Orange {
		get { return new ColorRYB( 1f, 1f, 0f ); }
	}

	public static ColorRYB Purpule {
		get { return new ColorRYB( 1f, 0f, 1f ); }
	}
}