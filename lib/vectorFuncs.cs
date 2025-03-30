function getRandomUnitSpherePoint()
{
	%randXY = getRandom() * 3.14159265 * 2;
	%randZ = getRandom() * 3.14159265;
	%x = mSin(%randXY) * mSin(%randZ);
	%y = mCos(%randXY) * mSin(%randZ);
	%z = mCos(%randZ);
	return = %x SPC %y SPC %z;
}

function getVectorAngle(%vec1, %vec2)
{
   return mACos(vectorDot(%vec1, %vec2));
}