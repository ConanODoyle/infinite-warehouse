//Documentation? barely counts as it, but...
//
//%bot.thinkFunction = function to call for thinking, with the sole param %bot
//	Always called before actFunction
//	Intended for targeting, logic behavior - can contain necessary action if logic is simple
//
//%bot.actFunction = function to call for action, with the sole param %bot
//	Always called after thinkFunction
//	Intended for actions that require constant checking/updates, eg "move to position" or "move to <= 10 tu from target"
//
//canBotThink/canBotAct - packagable function
//	Returns %bot.nextThinkTime > /!%bot.nextActTime > $Sim::Time, easy disables for any further action/think calls for some set period of time
//	Does not modify bot's current behavior

if (!isObject($BotSimSet))
{
	$BotSimSet = new SimSet();
}
$BotsPerLoop = 16;

package AICore_Callbacks
{
	function Armor::damage(%db, %obj, %sourceObj, %pos, %damage, %damageType)
	{
		if (isFunction(%obj.damageFunction))
		{
			damageBot(%obj, %damage, %sourceObj);
		}
		return parent::damage(%db, %obj, %sourceObj, %pos, %damage, %damageType);
	}
};
schedule(1000, 0, activatePackage, AICore_Callbacks); //top level package to get raw unmodified damage

function botLoop(%index)
{
	cancel($botLoopSchedule);
	%index = %index + 0;

	for (%i = 0; %i < $BotsPerLoop; %i++)
	{
		if (%index == $BotSimSet.getCount()) //reset pos
		{
			%index = 0;
		}

		if (%hasProcessed[%index]) //we're looping, don't re-process the AI twice in a single loop
		{
			break;
		}

		%bot = $BotSimSet.getObject(%index);

		//think: general logic, targeting or otherwise
		if (isFunction(%bot.thinkFunction) && canBotThink(%bot))
		{
			call(%bot.thinkFunction, %bot);
		}

		//act: movement/action logic that needs to run more often
		if (isFunction(%bot.actFunction) && canBotAct(%bot))
		{
			call(%bot.actFunction, %bot);
		}

		%hasProcessed[%index];
		%index++;
	}

	$botLoopSchedule = schedule(33, 0, botLoop, %i);
}

function canBotThink(%bot) //function to package if overriding
{
	return %bot.nextThinkTime < getSimTime();
}

function canBotAct(%bot) //function to package if overriding
{
	return !%bot.nextAct < getSimTime();
}

function stopAllBotActions(%bot)
{
	%bot.clearAim();
	%bot.setYaw(0);
	%bot.setPitch(0);
	%bot.setMoveY(0);
	%bot.setMoveX(0);
	%bot.setImageTrigger(0, 0);
	%bot.setImageTrigger(1, 0);
	%bot.setImageTrigger(2, 0);
	%bot.setImageTrigger(3, 0);
}


//callbacks for other scripts and AI state machines to hook onto/use
function alertBot(%bot, %alertString)
{
	if (isFunction(%bot.alertFunction))
	{
		call(%bot.alertFunction, %bot, %alertString);
	}
}

function damageBot(%bot, %damage, %sourceObject)
{
	if (isFunction(%bot.damageFunction))
	{
		return call(%bot.damageFunction, %bot, %damage, %sourceObject);
	}
}