function roundPositionToBrickGrid(%pos, %brickDimensions, %angleID)
{
	%x = getWord(%pos, 0);
	%y = getWord(%pos, 1);
	%z = getWord(%pos, 2);

	if (%angleID % 2)
	{
		%xEven = getWord(%brickDimensions, 1) % 2 ? 0 : 1;
		%yEven = getWord(%brickDimensions, 0) % 2 ? 0 : 1;
	}
	else
	{
		%xEven = getWord(%brickDimensions, 0) % 2 ? 0 : 1;
		%yEven = getWord(%brickDimensions, 1) % 2 ? 0 : 1;
	}

	if (!%xEven)
	{
		%x += 0.25;
	}
	if (!%yEven)
	{
		%y += 0.25;
	}

	%x = mFloor(%x * 2 + 0.5) / 2;
	%y = mFloor(%y * 2 + 0.5) / 2;

	if (!%xEven)
	{
		%x -= 0.25;
	}
	if (!%yEven)
	{
		%y -= 0.25;
	}

	//%z needs to be rounded to nearest 0.1
	%z = mFloor(%z * 10 + 0.5) / 10;

	return %x SPC %y SPC %z;
}