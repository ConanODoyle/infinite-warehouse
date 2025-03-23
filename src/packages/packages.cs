$DefaultPackageColor = 59;

package WarehousePackagesPackage
{
	function Armor::onRemove(%this, %obj)
	{
		if (isObject(%obj.packageTempBrick))
		{
			%obj.packageTempBrick.delete();
		}
		return parent::onRemove(%this, %obj);
	}

	function fxDTSBrick::onActivate(%obj, %player, %client, %pos, %vec)
	{
		if (%obj.dataBlock.isPackageBrick && isObject(%obj.dataBlock.item) && %obj.getName() $= "")
		{
			%slot = getWord(%player.getFreePackageSlots(%obj), 0);
			if (%obj.willCauseChainKill())
			{
				%player.client.centerprint("Can't pick up this box! It's supporting other bricks!", 3);
			}
			else if (%slot !$= "")
			{
				pickupPackage(%obj, %player, %slot);
				return;
			}
			else
			{
				%player.client.centerprint("Your inventory is full!", 3);
			}
		}
		parent::onActivate(%obj, %player, %client, %pos, %vec);
	}


	function ItemData::onUse(%this, %player, %invPosition)
	{
		%client = %player.client;
		%playerData = %player.getDataBlock();
		%mountPoint = %this.image.mountPoint;
		%mountedImage = %player.getMountedImage(%mountPoint);
		%image = %this.image;
		%player.updateArm (%image);
		if (%this.isPackageItem)
		{
			// talk("Equipping package with " @ %player.packageType[%player.currTool]);
			%player.mountImage(%image, %mountPoint, 0, addTaggedString(%player.packageType[%player.currTool]));
		}
		else
		{
			%player.mountImage(%image, %mountPoint);
		}
	}
};
activatePackage(WarehousePackagesPackage);

function loadBoxTextures()
{
	echo("----Forceloading box textures/prints");
	%dir = "Add-Ons/Server_Factory/resources/boxes/*.png";
	$PackageTypeCount = 0;
	%fileCount = getFileCount (%dir);
	%filename = findFirstFile (%dir);
	for (%i = 0; %i < %fileCount; %i++)
	{
		addExtraResource(%filename);
		echo("    Registered resource " @ %filename);
		%texName = fileBase(fileBase(%filename));

		$PackageType[$PackageTypeCount++ - 1] = %texName;
		$PackageName[%texName] = $PackageTypeCount - 1;
		
		// AddDamageType("boxpng" @ %i,   addTaggedString("<bitmap:" @ getSubStr(%filename, 0, strLen(%filename) - 4) @ "> %1"), '%2 %1', 1, 1);
		%filename = findNextFile (%dir);
	}
	echo("----Forceloaded " @ %i @ " box textures");
	
	%dir = "Add-Ons/Print_Warehouse_Boxes/prints/*.png";
	%fileCount = getFileCount (%dir);
	%filename = findFirstFile (%dir);
	for (%i = 0; %i < %fileCount; %i++)
	{
		addExtraResource(%filename);
		echo("    Registered resource " @ %filename);
		%filename = findNextFile (%dir);
	}
	echo("----Forceloaded " @ %i @ " box prints");
}
loadBoxTextures();

function Player::getFreePackageSlots(%this, %packageBrick)
{	
	%partialSlots = "";
	%emptySlots = "";
	%packageType = %packageBrick.getPackageType();
	
	//two lists, first to find partially filled slots, second to find empty slots
	//prioritize first list
	for(%i = 0; %i < %this.getDataBlock().maxTools ; %i++)
	{
		if(isObject(%this.tool[%i]) 
			&& %this.tool[%i].image.brick $= %packageBrick.dataBlock 
			&& %this.packageType[%i] $= %packageType 
			&& %this.toolPackageCount[%i] < %this.getMaxPackagesPerSlot(%packageBrick.dataBlock.item))
		{
			%count++;
			%partialSlots = %partialSlots SPC %i;
		}
	
		if(!isObject(%this.tool[%i]))
		{
			%count++;
			%emptySlots = %emptySlots SPC %i;
		}
	}
	
	return trim(trim(%partialSlots) SPC trim(%emptySlots));
}

function Player::getMaxPackagesPerSlot(%this, %packageItem)
{
	return %packageItem.maxPackagesPerSlot;
}

function pickupPackage(%obj, %player, %slot)
{
	messageClient(%player.client, 'MsgItemPickup', "", %slot, %obj.dataBlock.item.getID());
	%player.tool[%slot] = %obj.dataBlock.item.getID();
	%type = %obj.getPackageType();
	if (%type $= "" || !isFile("Add-ons/Server_Factory/resources/boxes/" @ %type @ ".box.png"))
	{
		%type = "base";
	}
	%player.packageType[%slot] = %type;
	%player.toolPackageCount[%slot]++;
	// serverCmdUseTool(%player.client, %slot);
	%obj.delete();
}

function fxDTSBrick::getPackageType(%brick)
{
	if (%brick.dataBlock.isPackageBrick)
	{
		return fileBase(fileBase(getPrintTexture(%brick.getPrintID())));
	}
	return "";
}









datablock ItemData(Package3x3Item : HammerItem)
{
	shapeFile = "Add-ons/Server_Factory/resources/boxes/3x3box.dts";
	iconName = "";
	uiName = "Large Package";
	image = "Package3x3Image";
	className = "ItemData";

	doColorshift = false;
	colorShiftColor = "0.521569 0.384314 0.219608 1.000000";

	isPackageItem = 1;
	maxPackagesPerSlot = 3;
	canDrop = 0;
};

datablock ItemData(Package2x3Item : HammerItem)
{
	shapeFile = "Add-ons/Server_Factory/resources/boxes/2x3box.dts";
	iconName = "";
	uiName = "Medium Package";
	image = "Package2x3Image";
	className = "ItemData";

	doColorshift = false;
	colorShiftColor = "0.521569 0.384314 0.219608 1.000000";

	isPackageItem = 1;
	maxPackagesPerSlot = 5;
	canDrop = 0;
};

datablock ItemData(Package2x2Item : HammerItem)
{
	shapeFile = "Add-ons/Server_Factory/resources/boxes/2x2box.dts";
	iconName = "";
	uiName = "Small Package";
	image = "Package2x2Image";
	className = "ItemData";

	doColorshift = false;
	colorShiftColor = "0.521569 0.384314 0.219608 1.000000";

	isPackageItem = 1;
	maxPackagesPerSlot = 8;
	canDrop = 0;
};

datablock ShapeBaseImageData(BasePackageImage)
{
	shapeFile = "Add-ons/Server_Factory/resources/boxes/3x3box.dts";
	mountPoint = 0;
	offset = "-0.532137 0 0.1";
	eyeOffset = "0 2.2 -0.6";

	doColorshift = false;
	colorShiftColor = "0.521569 0.384314 0.219608 1.000000";

	className = "PackageImage";

	armReady = 1;

	stateName[0]						= "Activate";
	stateTimeoutValue[0] 				= 0.1;
	stateTransitionOnTimeout[0]			= "Loop";

	stateName[1]						= "Loop";
	stateTransitionOnTimeout[1]			= "LoopB";
	stateScript[1]						= "onLoop";
	stateWaitForTimeout[1]				= false;
	stateTimeoutValue[1]				= 0.1;
	stateTransitionOnTriggerDown[1]		= "Fire";

	stateName[2]						= "LoopB";
	stateTransitionOnTimeout[2]			= "Loop";
	stateScript[2]						= "onLoop";
	stateWaitForTimeout[2]				= false;
	stateTimeoutValue[2]				= 0.1;
	stateTransitionOnTriggerDown[2]		= "Fire";

	stateName[3]						= "Fire";
	stateScript[3]						= "onFire";
	stateTimeoutValue[3]				= 0.2;
	stateWaitForTimeout[3]				= true;
	stateTransitionOnTriggerUp[3]		= "Loop";
};

datablock ShapeBaseImageData(Package3x3Image : BasePackageImage)
{
	offset = "-0.532137 0.35 0.55";
	shapeFile = "Add-ons/Server_Factory/resources/boxes/3x3box.dts";
	item = "Package3x3Item";

	brick = "brick3x3PackageData";
};

datablock ShapeBaseImageData(Package2x3Image : BasePackageImage)
{
	offset = "-0.532137 0 0.25";
	shapeFile = "Add-ons/Server_Factory/resources/boxes/2x3box.dts";
	item = "Package2x3Item";

	brick = "brick2x3PackageData";
};

datablock ShapeBaseImageData(Package2x2Image : BasePackageImage)
{
	shapeFile = "Add-ons/Server_Factory/resources/boxes/2x2box.dts";
	item = "Package2x2Item";

	brick = "brick2x2PackageData";
};

brick3x3PackageData.isPackageBrick = 1;
brick3x3PackageData.item = "Package3x3Item";
brick2x3PackageData.isPackageBrick = 1;
brick2x3PackageData.item = "Package2x3Item";
brick2x2PackageData.isPackageBrick = 1;
brick2x2PackageData.item = "Package2x2Item";




function PackageImage::onMount(%this, %obj, %slot)
{
	if (isObject(%obj.packageTempBrick))
	{
		%obj.packageTempBrick.delete();
	}
	%obj.playThread(1, "armReadyBoth");
	centerprintPackageCount(%this, %obj, %slot);
}

function PackageImage::onUnmount(%this, %obj, %slot)
{
	if (isObject(%obj.packageTempBrick))
	{
		%obj.packageTempBrick.delete();
	}
	%obj.client.centerprint("", 1);
}

function PackageImage::onLoop(%this, %obj, %slot)
{
	%start = %obj.getEyeTransform();
	%end = vectorAdd(%start, vectorScale(%obj.getEyeVector(), 5));
	centerprintPackageCount(%this, %obj, %slot);

	%ray = containerRaycast(%start, %end, $Typemasks::fxBrickObjectType | $Typemasks::StaticObjectType);
	if (!%ray)
	{
		if (isObject(%obj.packageTempBrick))
		{
			%obj.packageTempBrick.delete();
		}
		return;
	}

	%hit = getWord(%ray, 0);
	%pos = getWords(%ray, 1, 3);
	if (!isObject(%obj.packageTempBrick))
	{
		%obj.packageTempBrick = new fxDTSBrick()
		{
			dataBlock = %this.brick;
		};
		%obj.packageTempBrick.setPrint($printNameTable["Warehouse/" @ %obj.packageType[%obj.currTool] @ ".box"]);
		%obj.packageTempBrick.setColor($DefaultPackageColor);
	}
	%brick = %obj.packageTempBrick;
	%pos = roundPositionToBrickGrid(%pos,
		%brick.dataBlock.brickSizeX SPC %brick.dataBlock.brickSizeY SPC %brick.dataBlock.brickSizeZ,
		getAngleIDFromPlayer(%obj));
	
	%norm = getWords(%ray, 4, 6);
	if (vectorDist(%norm, "0 0 -1") < 0.01)
	{
		if (%brick.dataBlock.brickSizeZ % 2)
		{
			%shift = -0.2 * mFloor(%brick.dataBlock.brickSizeZ / 2);
		}
		else
		{
			%shift = -0.2 * mCeil(%brick.dataBlock.brickSizeZ / 2);
		}
	}
	else
	{
		if (%brick.dataBlock.brickSizeZ % 2)
		{
			%shift = 0.2 * mCeil(%brick.dataBlock.brickSizeZ / 2);
		}
		else
		{
			%shift = 0.2 * mCeil(%brick.dataBlock.brickSizeZ / 2);
		}
	}

	%pos = vectorAdd(%pos, "0 0 " SPC %shift);

	if (vectorDist(%brick.getTransform(), %pos) >= 0.1)
	{
		%brick.setTransform(%pos SPC "0 0 1 " @ (getAngleIDFromPlayer(%obj) * $piOver2));
	}
}

function PackageImage::onFire(%this, %obj, %slot)
{
	if (!isObject(%obj.packageTempBrick))
	{
		return;
	}

	%brick = %obj.packageTempBrick;
	%error = %brick.plant();
	if (%error)
	{
		%brick.delete();
		if (isObject(%obj.client))
		{
			switch (%error)
			{
				case 1: messageClient (%obj.client, 'MsgPlantError_Overlap');
				case 2: messageClient (%obj.client, 'MsgPlantError_Float');
				case 3: messageClient (%obj.client, 'MsgPlantError_Stuck');
				case 4: messageClient (%obj.client, 'MsgPlantError_Unstable');
				case 5: messageClient (%obj.client, 'MsgPlantError_Buried');
				default: messageClient (%obj.client, 'MsgPlantError_Forbidden');
			}
		}
		return;
	}
	%brick.setTrusted(1);
	%bg = %obj.client.brickgroup ? %obj.client.brickgroup : "Brickgroup_888888";
	%bg.add(%brick);
	serverPlay3d("brickPlantSound", %brick.position);

	%obj.packageTempBrick = "";

	%obj.toolPackageCount[%obj.currTool]--;
	centerprintPackageCount(%this, %obj, %slot);

	if (%obj.toolPackageCount[%obj.currTool] <= 0)
	{
		%obj.tool[%obj.currTool] = 0;
		messageClient(%obj.client, 'MsgItemPickup', "", %obj.currTool, 0);
		%obj.unmountImage(%slot);
		%obj.toolPackageCount[%obj.currTool] = "";
		%obj.packageType[%obj.currTool] = "";
	}

	for (%i = 0; %i < %brick.getNumDownBricks(); %i++)
	{
		%brick.getDownBrick(0).onPackagePlaced(%brick, %obj);
	}
}

function centerprintPackageCount(%this, %obj, %slot)
{
	%size = %obj.tool[%obj.currTool].uiName;
	%type = %obj.packageType[%obj.currTool];
	%max = %obj.getMaxPackagesPerSlot(%this.item);
	%count = %obj.toolPackageCount[%obj.currTool] + 0;
	%countColor = (%max == %count ? "\c3" : "\c0");
	%obj.client.centerprint("\n\n\n\n\n\c6[\c5" @ %type @ "\c6] \c3" @ %size @ ": " @ %count @ "/" @ %max @ " ", 5);
}