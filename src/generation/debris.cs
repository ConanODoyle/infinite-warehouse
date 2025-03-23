if (!isObject($DebrisSimSet))
{
	$DebrisSimSet = new SimSet();
}

function generateDebrisStacks(%position, %brickDatablocks, %heights, %totalTowers)
{
	//pre-parser
	%fieldCount = getFieldCount(%brickDatablocks);
	for (%i = 0; %i < %fieldCount; %i++)
	{
		%field = getField(%brickDatablocks, %i);
		%brick[%i] = getWord(%field, 0);
		%color[%i] = getWord(%field, 1);
		%print[%i] = getWord(%field, 2);
		%nudgeFactor = 1;
		if (getWord(%field, 3) !$= "")
		{
			%nudgeFactor = getWord(%field, 3);
		}
		%plantNudgeSize[%i] = (%brick[%i].brickSizeX * 0.5 * %nudgeFactor) SPC (%brick[%i].brickSizeY * 0.5 * %nudgeFactor) SPC 0;
		}

	%nextPosition = %position;
	for (%i = 0; %i < %totalTowers; %i++)
	{
		%randomStartRotation = getRandom(0, 3);
		%randomIdx = getRandom(0, %fieldCount - 1);
		%randomBrickDatablock = %brick[%randomIdx];
		%color = %color[%randomIdx];
		%print = %print[%randomIdx];
		%randomBrickNudgeSize = %plantNudgeSize[%randomIdx];

		if (!isObject(%randomBrickDatablock) || %randomBrickDatablock.getClassName() !$= "fxDTSBrickData")
		{
			talk("Not a datablock! " @ %randomBrickDatablock);
			echo("Not a datablock! " @ %randomBrickDatablock);
			return;
		}

		//find a valid rotation for the position
		%nextBrickPosition = vectorAdd(%nextPosition, "0 0 " @ %randomBrickDatablock.brickSizeZ * 0.1);
		for (%j = 0; %j < 2; %j++)
		{
			%brick = new fxDTSBrick()
			{
				dataBlock = %randomBrickDatablock;
			};

			if (!isObject(%brick))
			{
				talk("Failed to instantiate! " @ %brick SPC %randomBrickDatablock);
				echo("Failed to instantiate! " @ %brick SPC %randomBrickDatablock);
				continue;
			}
			%brick.setColor(%color);
			if (%print !$= "")
			{
				%brick.setPrint($printNameTable[%print]);
			}

			%angle = (%randomStartRotation + %j) % 4 * (3.14159265/2);
			%brick.setTransform(%nextBrickPosition SPC "0 0 1" SPC %angle);

			if (!%brick.plant()) //no error
			{
				Brickgroup_888888.add(%brick);
				$DebrisSimSet.add(%brick);
				%brick.setTrusted(1);
				break;
			}
			%brick.delete();
		}

		if (!isObject(%brick)) //did not generate a valid brick, nudge and try again
		{
			if (%safety++ > 10000)
			{
				talk("Safety broken");
				return;
			}
			%nextPosition = nudgePosition(%nextPosition, %randomBrickNudgeSize);
			%i--;
			continue;
		}

		//stack the brick
		%randomHeight = getWord(%heights, getRandom(0, getWordCount(%heights) - 1));
		for (%k = 1; %k < %randomHeight; %k++)
		{
			%nextBrickPosition = vectorAdd(%brick.position, "0 0 " @ %randomBrickDatablock.brickSizeZ * 0.2);
			%brick = new fxDTSBrick()
			{
				dataBlock = %randomBrickDatablock;
			};
			%brick.setColor(%color);
			if (%print !$= "")
			{
				%brick.setPrint($printNameTable[%print]);
			}

			%brick.setTransform(%nextBrickPosition SPC "0 0 1" SPC %angle);
			if (%brick.plant()) //error
			{
				%brick.delete();
				break;
			}
			Brickgroup_888888.add(%brick);
			$DebrisSimSet.add(%brick);
			%brick.setTrusted(1);
		}

		%nextPosition = nudgePosition(%nextPosition, %randomBrickNudgeSize);
	}
}


function nudgePosition(%position, %maxRange)
{
	%x = getWord(%maxRange, 0);
	%y = getWord(%maxRange, 1);

	%min = getMin(%x, %y);

	return vectorAdd(%position, getRandom(-1 * %min, %min) SPC getRandom(-1 * %min, %min));
}

$shelf = "brickWarehouseShelfUnitData 55";
$shelfSide = "brickWarehouseShelfSideData 55";

$shelfScat = "brickWarehouseShelfUnitData 55  2";
$shelfSideScat = "brickWarehouseShelfSideData 55  2";
$box = "brick3x3PackageData 0 Warehouse/base.box 4";
$medBox = "brick2x3PackageData 0 Warehouse/base.box 8";
$smallBox = "brick2x2PackageData 0 Warehouse/base.box 10";

$blockers = trim($shelf TAB $shelf TAB $shelf TAB $shelf TAB $shelf TAB $shelf TAB $shelfSide TAB $shelfSide);
$scattered = trim($box TAB $medBox TAB $smallBox TAB $smallBox TAB $shelfScat TAB $shelfSideScat);


function testGen(%hitloc)
{
	exec("add-ons/server_factory/src/generation/debris.cs");
	$DebrisSimSet.deleteAll();
	generateDebrisStacks(%hitloc, $blockers, "4 4 5 5 6", 200);
	generateDebrisStacks(%hitloc, $scattered, "1 1 1 2 2 3", 100);
}