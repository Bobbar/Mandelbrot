
typedef struct
{
	uchar R;
	uchar G;
	uchar B;

} clColor;

constant clColor BLACK = { 0, 0, 0 };

//double Scale(double m, double Tmin, double Tmax, double Rmin, double Rmax);
//int GetPixel(int px, int py, int maxIters, double2 xMinMax, double2 yMinMax, int2 fieldSize, float cValue);
//clColor GetColor(float maxValue, float value, global clColor* pallet, int palletLen);


double Scale(double m, double Tmin, double Tmax, double Rmin, double Rmax)
{
	double res = ((m - Rmin) / (Rmax - Rmin)) * (Tmax - Tmin) + Tmin;
	return res;
}

double Norm(double2 c)
{
	return c.x * c.x + c.y * c.y;
}

double2 Mul(double2 a, double2 b) 
{
	return (double2)(a.x * b.x - a.y * b.y, a.x * b.y + a.y * b.x);
}

clColor GetColor(double zn_size, int iters, global clColor* pallet, int palletLen)
{
	double nu = iters - log2(log2(zn_size));
	int i = (int)(nu * 10.0) % palletLen;

	if (i < 0)
		return BLACK;

	return pallet[i];
}

clColor GetPixel(int px, int py, int2 fieldSize, double radius, global double2* hpPnts, int hpPntsLen, global clColor* pallet, int palletLen)
{
	double x0 = radius * (2.0 * (double)px - (double)fieldSize.x) / (double)fieldSize.x;
	double y0 = -radius * (2.0 * (double)py - (double)fieldSize.y) / (double)fieldSize.x;
	double zn_size = 0;
	double x = 0;
	double y = 0;

	int iters = 0;
	int max = hpPntsLen - 1;
	double2 d0 = (double2)(x0, y0);
	double2 dn = d0;

	do
	{
		dn = Mul(dn, hpPnts[iters] + dn);
		dn += d0;
		iters++;
		zn_size = Norm(hpPnts[iters] * 0.5 + dn);

	} while (zn_size < 256 && iters < max);

	if (iters == max)
		return BLACK;
	else
		return GetColor(zn_size, iters, pallet, palletLen);

}


__kernel void ComputePixels(global uchar* pixels, int2 dims, int maxIters, int2 fieldSize, global clColor* pallet, int palletLen, global double2* hpPnts, int hpPntsLen, double radius)
{
	int x = get_global_id(0);
	int y = get_global_id(1);

	if (x >= dims.x || y >= dims.y)
		return;

	int idx = y * dims.x + x;
	int pidx = idx * 4;

	clColor color = GetPixel(x, y, fieldSize, radius, hpPnts, hpPntsLen, pallet, palletLen);

	pixels[pidx] = color.B;
	pixels[pidx + 1] = color.G;
	pixels[pidx + 2] = color.R;
	pixels[pidx + 3] = (uchar)255;

	// B&W
	/*pixels[pidx] = (uchar)(iters * (255.0f / (float)maxIters));
	pixels[pidx + 1] = (uchar)(iters * (255.0f / (float)maxIters));
	pixels[pidx + 2] = (uchar)(iters * (255.0f / (float)maxIters));
	pixels[pidx + 3] = (uchar)255;*/
}
