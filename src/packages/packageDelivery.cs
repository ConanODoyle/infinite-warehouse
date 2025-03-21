function fxDTSBrick::onPackagePlaced(%this, %brick, %player)
{
	%client = %player.client;

	$InputTarget_["Self"] = %this;
	$InputTarget_["Player"] = %player;
	$InputTarget_["Client"] = %client;
	$InputTarget_["MiniGame"] = getMiniGameFromObject(%this);

	%this.processInputEvent("onPackagePlaced", %cl);
	if (%this.isAcceptingDeliveries)
	{
		checkDelivery(%this, %brick, %player);
	}
}
registerInputEvent("fxDTSBrick", "onPackagePlaced", "Self fxDTSBrick" TAB "Player Player" TAB "Client GameConnection" TAB "MiniGame MiniGame");



function checkDelivery(%receiver, %package, %player)
{
	if (%receiver.receivingPackageType !$= %package.getPackageType())
	{
		%slot = getWord(%player.getFreeInventorySlots(), 0);
		pickupPackage(%package, %player, %slot);
		return 0;
	}
	%package.schedule(1000, spawnExplosion, "deathProjectile", 0.5);
	return 1;
}