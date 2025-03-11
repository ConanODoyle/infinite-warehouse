// package FactoryPrintLoading
// {
// 	function loadPrintedBrickTexture(%aspectRatio)
// 	{
// 		if (%aspectRatio $= "Warehouse")
// 		{
// 			loadBoxPrintedBrickTexture();
// 			return;
// 		}
// 		else
// 		{
// 			return parent::loadPrintedBrickTexture(%aspectRatio);
// 		}
// 	}
// };
// activatePackage(FactoryPrintLoading);

// function loadBoxPrintedBrickTexture()
// {
// 	echo("----Loading box prints");
// 	%aspectRatio = "Warehouse";
// 	$printARStart[%aspectRatio] = $globalPrintCount;
// 	%localPrintCount = 0;
// 	%dir = "Add-Ons/Server_Factory/resources/boxes1/*.png";
// 	%fileCount = getFileCount (%dir);
// 	%filename = findFirstFile (%dir);
// 	%i = 0;
// 	while (%i < %fileCount)
// 	{
// 		%path = filePath (%filename);
// 		addExtraResource(%filename);
// 		%fileBase = fileBase (%filename);
// 		if (strpos (%fileBase, " ") != -1)
// 		{
// 			warn ("WARNING: loadPrintedBrickTexture() - Bad print file name \"" @ %filename @ "\" - Cannot have spaces");
// 			%filename = findNextFile (%dir);
// 		}
// 		else 
// 		{
// 			%iconFileName = "Add-Ons/Server_Factory/resources/icons/" @ %fileBase @ ".png";
// 			if (!isFile (%iconFileName))
// 			{
// 				warn ("WARNING: loadPrintedBrickTexture() - Print \"" @ %filename @ "\" has no icon - skipping");
// 				%filename = findNextFile (%dir);
// 			}
// 			else 
// 			{
// 				%idString = %aspectRatio @ "/" @ %fileBase;
// 				if ($printNameTable[%idString] !$= "")
// 				{
// 					warn ("WARNING: loadPrintedBrickTexture() - Print \"" @ %filename @ "\" - " @ %idString @ " already exists - skipping");
// 					%filename = findNextFile (%dir);
// 				}
// 				else 
// 				{
// 					echo("    Loaded " @ %idstring @ " with " @ %filename);
// 					$printNameTable[%idString] = $globalPrintCount;
// 					setPrintTexture ($globalPrintCount, %filename);
// 					echo("    GetPrintTexture " @ $globalPrintCount @ ": " @ getPrintTexture($globalPrintCount));
// 					$globalPrintCount += 1;
// 					%localPrintCount += 1;
// 					%filename = findNextFile (%dir);
// 				}
// 			}
// 		}
// 		%i += 1;
// 	}
// 	echo("----Loaded " @ %i @ " box prints");
// 	$printARNumPrints[%aspectRatio] = %localPrintCount;
// 	$printAREnd[%aspectRatio] = $globalPrintCount;
// }