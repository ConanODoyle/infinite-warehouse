// Support_SetMaximumHealth by Jetz
package DamageResistance
{
	function Player::applyDamage(%this, %value)
	{
		if(%this.DamageResistance > 0 && %this.getClassName() !$= "AIPlayer")
			%value /= %this.DamageResistance;
		Parent::applyDamage(%this, %value);
	}
	
	function Player::applyRepair(%this, %value) //Can't think of where this function is used, but may as well get it.
	{
		if(%this.DamageResistance > 0)
			%value /= %this.DamageResistance;
		Parent::applyRepair(%this, %value);
	}
	
	function Player::addHealth(%this, %value)
	{
		if(%this.DamageResistance > 0 && %value > 0) //Apparently negative values are run through the damage function instead, so we don't want our effect applied twice.
			%value /= %this.DamageResistance;
		Parent::addHealth(%this, %value);
	}
	
	//Intentionally leaving out the set health values for now.
	
	function Player::setDatablock(%this, %db) //Make sure the value is applied when we change datablocks.
	{
		%this.DamageResistance *= %this.getDatablock().maxDamage;
		Parent::setDatablock(%this, %db);
		%this.DamageResistance /= %this.getDatablock().maxDamage;
	}

	function AiPlayer::applyDamage(%this, %value)
	{
		if(%this.DamageResistance > 0)
			%value /= %this.DamageResistance;
		Parent::applyDamage(%this, %value);
	}
	
	function AiPlayer::addHealth(%this, %value)
	{
		if(%this.DamageResistance > 0 && %value > 0) //Apparently negative values are run through the damage function instead, so we don't want our effect applied twice.
			%value /= %this.DamageResistance;
		Parent::addHealth(%this, %value);
	}
};
activatePackage(DamageResistance);

function Player::SetMaximumHealth(%this, %value)
{
	if(%value <= 0)
	{
		%this.DamageResistance = 0;
		return;
	}
	%baseHealth = %this.getDatablock().maxDamage;
	%this.DamageResistance = %value / %baseHealth;
}

function AiPlayer::SetMaximumHealth(%this, %value)
{
	if(%value <= 0)
	{
		%this.DamageResistance = 0;
		return;
	}
	%baseHealth = %this.getDatablock().maxDamage;
	%this.DamageResistance = %value / %baseHealth;
}