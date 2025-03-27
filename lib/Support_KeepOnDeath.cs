package AoD_KeepOnDeath
{
	function Armor::onDisabled(%this, %obj, %state)
	{
		if (%this.keepOnDeath)
		{
			return;
		}
		return parent::onDisabled(%this, %obj, %state);
	}
};
activatePackage(AoD_KeepOnDeath);