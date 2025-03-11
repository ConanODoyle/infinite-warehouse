
function teleportIfTooFarLoop()
{
	cancel($teleportIfTooFarLoopSchedule);

	for (%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%pl = ClientGroup.getObject(%i).player;
		while(isObject(%pl) && isObject(%o = %pl.getObjectMount()))
		{
			%pl = %o;
		}
		%x = getWord(%pl.position, 0);
		%y = getWord(%pl.position, 1);
		%threshold = 64 * 4;
		if (mAbs(%x) >= %threshold || mAbs(%y) >= %threshold)
		{
			%xAmt = mFloor(mAbs(%x) / 32);
			%yAmt = mFloor(mAbs(%y) / 32);
			%x = %x + (%x > 0 ? %xAmt * -64 : %xAmt * 64);
			%y = %y + (%y > 0 ? %yAmt * -64 : %yAmt * 64);

			%oldpos = %pl.getTransform();
			%pl.setTransform((%x) SPC (%y) SPC getWord(%pl.position, 2) SPC getWords(%pl.getTransform(), 3, 6));
		}
	}

	$teleportIfTooFarLoopSchedule = schedule(40, 0, teleportIfTooFarLoop);
}