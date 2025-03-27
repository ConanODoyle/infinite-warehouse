// Chrono or Space Guy made this?
package Support_ExtraInventorySlots
{
	function Armor::onNewDatablock(%data,%this)
	{
		Parent::onNewDatablock(%data,%this);
		if(isObject(%this.client) && %data.maxTools != %this.client.lastMaxTools)
		{
			%this.client.lastMaxTools = %data.maxTools;
			commandToClient(%this.client,'PlayGui_CreateToolHud',%data.maxTools);
			%this.client.disableUnUseToolUntil = $sim::time + 0.5;
			for(%i=0;%i<%data.maxTools;%i++)
			{
				if(isObject(%this.tool[%i]))
					messageClient(%this.client,'MsgItemPickup',"",%i,%this.tool[%i].getID(),1);
				else
					messageClient(%this.client,'MsgItemPickup',"",%i,0,1);
			}
		}
	}
	function GameConnection::setControlObject(%this,%obj)
	{
		Parent::setControlObject(%this,%obj);
		if(%obj == %this.player && %obj.getDatablock().maxTools != %this.lastMaxTools)
		{
			%this.lastMaxTools = %obj.getDatablock().maxTools;
			%this.client.disableUnUseToolUntil = $sim::time + 0.5;
			commandToClient(%this,'PlayGui_CreateToolHud',%obj.getDatablock().maxTools);
		}
	}
	function Player::changeDatablock(%this,%data,%client)
	{
		if(%data != %this.getDatablock() && %data.maxTools != %this.client.lastMaxTools)
		{
			%this.client.lastMaxTools = %data.maxTools;
			%this.client.disableUnUseToolUntil = $sim::time + 0.5;
			commandToClient(%this.client,'PlayGui_CreateToolHud',%data.maxTools);
		}
		Parent::changeDatablock(%this,%data,%client);
	}

	// New: Don't let the player unequip items for half a second after getting their inventory slots adjusted. Prevents adjustment from unequipping their tool automatically.
	function servercmdUnUseTool(%client)
	{
		if(%client.disableUnUseToolUntil > $sim::time)
			return;

		return parent::servercmdUnUseTool(%client);
	}
};
activatePackage(Support_ExtraInventorySlots);
