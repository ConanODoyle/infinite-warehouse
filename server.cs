exec("./lib/Support_ExtraResources.cs");
exec("./lib/Support_DataIDV2.cs");
exec("./lib/Support_FieldAccessors.cs");
exec("./lib/Support_KeepOnDeath.cs");
exec("./lib/Support_SetMaximumHealth.cs");
exec("./lib/Support_ExtraInventorySlots.cs");
exec("./lib/Support_ShapelinesV2/server.cs");

// exec("./lib/printloading.cs");
exec("./lib/autoteleport.cs");
exec("./lib/inventoryutils.cs");
exec("./lib/roundPosToStudGrid.cs");
exec("./lib/vectorFuncs.cs");

exec("./bricks/datablocks.cs");

exec("./src/packages/packages.cs");
exec("./src/packages/packageDelivery.cs");
exec("./src/generation/debris.cs");


schedule(1000, 0, eval, "PlayerDropPoints.getObject(0).rayHeight = 1;");