using Core.Models;

namespace Core.Colors;

public static class RandomColor
{
	public static CoreColor Get(bool ChangeAlpha = false) 
	{ 
		CoreColor color = new CoreColor();

        Random random = Random.Shared;

		if(ChangeAlpha) color.A = (byte)random.Next(0,256);
		color.R = (byte)random.Next(0, 256);
		color.G = (byte)random.Next(0, 256);
		color.B = (byte)random.Next(0, 256);

		return color;
	}
}
