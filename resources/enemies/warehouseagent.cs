$WHA::PassiveActivateRadius = 5;

datablock ShapeBaseImageData(LargeHeadImage)
{
	shapeFile = "./largehead.dts";
	emap = true;

	colorShiftColor = "1 0.878431 0.611765 1";
	doColorshift = true;

	mountPoint = 5;
};

datablock ShapeBaseImageData(LargeHandLImage)
{
	shapeFile = "./largehandl.dts";
	emap = true;

	colorShiftColor = "1 0.878431 0.611765 1";
	doColorshift = true;

	mountPoint = 1;
};

datablock ShapeBaseImageData(LargeHandRImage)
{
	shapeFile = "./largehandr.dts";
	emap = true;

	colorShiftColor = "1 0.878431 0.611765 1";
	doColorshift = true;

	mountPoint = 1;
};

datablock PlayerData(AgentBotArmor : PlayerStandardArmor)
{
	uiName = "";
	aiAvoidThis = false;

	airControl = 0.8;
	
	maxForwardSpeed = 6.5;
	maxBackwardSpeed = 3.5;
	maxSideSpeed = 3;
	
	maxForwardCrouchSpeed = 3;
	maxBackwardCrouchSpeed = 2;
	maxSideCrouchSpeed = 2;
	
	maxUnderwaterForwardSpeed = 8.4;
	maxUnderwaterBackwardSpeed = 7.8;
	maxUnderwaterSideSpeed = 7.8;
};

function WarehouseAgentBot()
{
	%bot = new AIPlayer()
	{
		dataBlock = "PlayerStandardArmor";
		yawSpeed = 10;
		pitchSpeed = 10;
	};
	%bot.hideNode("HeadSkin");
	%bot.hideNode("lHand");
	%bot.hideNode("rHand");

	%bot.mountImage(LargeHandRImage, 0);
	%bot.mountImage(LargeHandLImage, 1);
	%bot.mountImage(LargeHeadImage, 2);

	%bot.setScale("1.2 1.2 1.2");

	return %bot;
}

function spawnWarehouseAgentBot(%position)
{
	%bot = WarehouseAgentBot();
	$BotSimSet.add(%bot);

	%bot.thinkFunction = "WarehouseAgent_PassiveThink";
	%bot.alertFunction = "WarehouseAgent_Alert";
	%bot.actFunction = "";
}

function WarehouseAgent_PassiveThink(%bot)
{
	%bot.nextThinkTime = getSimTime() + 4000;

	if (!%bot.lastLookVector)
	{
		%bot.lastLookVector = getRandomUnitSpherePoint();
		%bot.setAimVector(%bot.lastLookVector);
	}
	//10 stud radius search
	initContainerRadiusSearch(%bot.position, $WHA::PassiveActivateRadius, $Typemasks::PlayerObjectType);
	for (%next = containerSearchNext())
	{
		if (%next.getClassName() $= "Player" && vectorDist(%bot.position, %next.position) < $WHA::PassiveActivateRadius)
		{
			%bot.thinkFunction = "WarehouseAgent_ActiveThink";
		}
	}
}

function WarehouseAgent_ActiveThink(%bot)
{

}