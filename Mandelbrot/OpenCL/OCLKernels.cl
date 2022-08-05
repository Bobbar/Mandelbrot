
typedef struct
{
	uchar R;
	uchar G;
	uchar B;

} clColor;


double Scale(double m, double Tmin, double Tmax, double Rmin, double Rmax);
double Scale(double m, double Tmin, double Tmax, double Rmin, double Rmax)
{
	double res = ((m - Rmin) / (Rmax - Rmin)) * (Tmax - Tmin) + Tmin;
	return res;
}


int GetPixel(int px, int py, int maxIters, double2 xMinMax, double2 yMinMax, int2 fieldSize, float cValue);
int GetPixel(int px, int py, int maxIters, double2 xMinMax, double2 yMinMax, int2 fieldSize, float cValue)
{
	double x0 = Scale((double)px, xMinMax.x, xMinMax.y, 0, fieldSize.x);
	double y0 = Scale((double)py, yMinMax.x, yMinMax.y, 0, fieldSize.y);

	double x = 0;
	double y = 0;

	int iters = 0;

	while (x * x + y * y <= cValue * cValue && iters < maxIters)
	{
		double xtemp = x * x - y * y + x0;
		y = cValue * x * y + y0;
		x = xtemp;
		iters++;
	}

	return iters;
}


clColor GetColor(float maxValue, float value, global clColor* pallet, int palletLen);
clColor GetColor(float maxValue, float value, global clColor* pallet, int palletLen)
{
	clColor black;
	black.R = 0;
	black.G = 0;
	black.B = 0;

	if (value >= maxValue)
		return black;
	else
		return pallet[(int)value % (palletLen - 1)];
}


__kernel void ComputePixels(global uchar* pixels, int2 dims, int maxIters, double2 xMinMax, double2 yMinMax, int2 fieldSize, int colorScale, float cValue, global clColor* pallet, int palletLen)
{
	int x = get_global_id(0);
	int y = get_global_id(1);

	if (x >= dims.x || y >= dims.y)
		return;

	int idx = y * dims.x + x;
	int pidx = idx * 4;

	pixels[pidx] = 0;
	pixels[pidx + 1] = 0;
	pixels[pidx + 2] = 0;
	pixels[pidx + 3] = 0;

	int iters = GetPixel(x, y, maxIters, xMinMax, yMinMax, fieldSize, cValue);

	clColor color = GetColor(maxIters, iters, pallet, palletLen);

	pixels[pidx] = color.B;
	pixels[pidx + 1] = color.G;
	pixels[pidx + 2] = color.R;
	pixels[pidx + 3] = (uchar)255;

	// B&W
	/*pixels[pidx] = (uchar)(iters * (255.0f / (float)maxIters));
	pixels[pidx + 1] = (uchar)(iters * (255.0f / (float)maxIters));
	pixels[pidx + 2] = (uchar)(iters * (255.0f / (float)maxIters));
	pixels[pidx + 3] = (uchar)(iters * (255.0f / (float)maxIters));*/
}