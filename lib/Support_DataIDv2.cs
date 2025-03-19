if (!isObject($LoadedDataIDs))
{
	$LoadedDataIDs = new SimSet(LoadedDataIDs);
}

//loads or creates a fresh data ID object
//@params %dataid - name for data id object
function DataID(%dataID)
{
	if (%dataID $= "")
	{
		return 0;
	}

	%objectName = "DataIDObj_" @ getSafeVariableName(%dataID);
	if (!isObject(%object))
	{
		%obj = new SimSet(%objectName){
			isDataIDObject = 1;
		};
		%LoadedDataIDs.add(%obj);
	}
	return %objectName.getID();
}



//array functions
//populates a List-like array
function DataID_Set(%obj, %idx, %value)
{
	if (!%obj.isDataIDObject) return;

	%idx = %idx + 0;
	if (%value !$= "")
	{
		%obj.array_[%idx] = %value;
		if (%obj.arrayCount < %idx)
		{
			%obj.arrayCount = %idx + 1;
		}
	}
	else
	{
		%obj.array_[%idx] = "";
		if (%obj.arrayCount == %idx + 1)
		{
			%obj.arrayCount--;
		}
	}
}

function DataID_Add(%obj, %value)
{
	if (!%obj.isDataIDObject) return;

	%idx = %obj.arrayCount + 1;
	%obj.array_[%idx - 1] = %value;
}

function DataID_Delete(%obj, %idx)
{
	DataID_Set(%obj, %idx, "");
}

//Reduces empty values in list
function DataID_CompressList(%obj)
{
	if (!%obj.isDataIDObject) return;

	for (%i = %obj.arrayCount - 1; %i > 0; %i--)
	{
		%value = %obj.array_[%i];
		if (%value $= "")
		{
			%obj.arrayCount--;
		}
		else
		{
			return;
		}
	}
}



//dict functions
function DataID_SetKey(%obj, %key, %value)
{
	if (!%obj.isDataIDObject || %key $= "") return;

	if (%value !$= "")
	{

		%obj.dict_[%key] = %value;

		//update key list
		for (%i = 0; %i < %obj.keyCount; %i++)
		{
			if (%obj.key_[%i] $= "")
			{
				%obj.key_[%i] = %key;
				break;
			}
			else if (%obj.key_[%i] $= %key)
			{
				//already in the list, don't need to add it
				return;
			}
		}

		if (%i >= %obj.keyCount)
		{
			%obj.keyCount = %i + 1;
			%obj.key_[%obj.keyCount - 1] = %key;
		}
	}
	else
	{
		DataID_DeleteKey(%obj, %key);
	}
}

function DataID_DeleteKey(%obj, %key)
{
	if (!%obj.isDataIDObject || $key $= "") return;

	for (%i = 0; %i < %obj.keyCount; %i++)
	{
		if (%obj.key_[%i] $= %key)
		{
			%obj.key_[%i] = "";
			%obj.dict_[%key] = "";
		}
	}
}

function DataID_HasKey(%obj, %key)
{
	if (!%obj.isDataIDObject || $key $= "") return;

	for (%i = 0; %i < %obj.keyCount; %i++)
	{
		if (%obj.key_[%i] $= %key)
		{
			return 1;
		}
	}
	return 0;
}

//Reduces empty values in key list
function DataID_CompressKeyList(%obj)
{
	if (!%obj.isDataIDObject) return;

	// echo("Compressing " @ %obj @ " key array");
	//find empty slots and fill with end of list items
	for (%i = 0; %i < %obj.keyCount; %i++)
	{
		if (%obj.key_[%i] !$= "")
		{
			continue;
		}
		// echo("Found slot " @ %i @ " is empty");

		//find the end of list item
		for (%j = %obj.keyCount - 1; %j > %i; %j--)
		{
			if (%obj.key_[%j] !$= "")
			{
				// echo("   Found slot " @ %j @ " is occupied");
				break;
			}
		}

		//end of list is our current index, exit
		if (%j == %i)
		{
			// echo("Did not find occupied slot past empty slot, exiting...");
			%obj.keyCount = %i;
			return;
		}

		//move item, then loop
		%obj.key_[%i] = %obj.key_[%j];
		%obj.key_[%j] = "";
		%obj.keyCount = %j;
	}
	// echo("Completed compression of " @ %obj);
}



//print functions
function DataID_EchoList(%obj)
{
	echo(%obj.getID() @ "'s array (" @ %obj.getName() @ "):");
	echo("Size " @ %obj.arrayCount @ ":");
	for (%i = 0; %i < %obj.arrayCount; %i++)
	{
		if (%obj.array_[%i] !$= "")
		{
			echo("  " @ %i @ ": " @ %obj.array_[%i]);
		}
	}
}

function DataID_EchoDict(%obj)
{
	echo(%obj.getID() @ "'s keys (" @ %obj.getName() @ "):");
	echo("Size " @ %obj.keyCount @ ":");
	for (%i = 0; %i < %obj.keyCount; %i++)
	{
		if (%obj.key_[%i] !$= "")
		{
			%key = %obj.key_[%i];
			echo("  " @ %key @ ": " @ %obj.dict_[%key]);
		}
	}
}



//tests
exec("config/compressKeyTest.cs");