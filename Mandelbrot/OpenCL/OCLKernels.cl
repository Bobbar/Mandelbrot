﻿
typedef struct
{
	uchar B;
	uchar G;
	uchar R;
	uchar A;

} clColor;

constant clColor BLACK = { 0, 0, 0, 255 };


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

clColor GetColor(double zn_size, int iters, const global clColor* pallet, int palletLen)
{
	double nu = iters - log2(log2(zn_size));
	int i = (int)(nu * 10.0) % palletLen;

	i = clamp(i, 0, palletLen);

	return pallet[i];
}

clColor GetPixel(int px, int py, int2 fieldSize, double radius, const global double2* hpPnts, int hpPntsLen, const global clColor* pallet, int palletLen)
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

__kernel void ComputePixels(global clColor* pixels, int2 dims, const global clColor* pallet, int palletLen, const global double2* hpPnts, int hpPntsLen, double radius)
{
	int x = get_global_id(0);
	int y = get_global_id(1);

	if (x >= dims.x || y >= dims.y)
		return;

	int idx = y * dims.x + x;

	clColor color = GetPixel(x, y, dims, radius, hpPnts, hpPntsLen, pallet, palletLen);

	pixels[idx] = color;
}

