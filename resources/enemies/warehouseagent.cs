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
	
	maxForwardSpeed = 7;
	maxBackwardSpeed = 4;
	maxSideSpeed = 6;
	
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
		yawSpeed = 2;
		pitchSpeed = 2;
	};
	%bot.hideNode("HeadSkin");
	%bot.hideNode("lHand");
	%bot.hideNode("rHand");

	%bot.mountImage(LargeHandRImage, 0);
	%bot.mountImage(LargeHandLImage, 1);
	%bot.mountImage(LargeHeadImage, 2);

	return %bot;
}