exec("./lib/extraresources.cs");
// exec("./lib/printloading.cs");
exec("./lib/autoteleport.cs");
exec("./lib/inventoryutils.cs");
exec("./lib/roundPosToStudGrid.cs");

exec("./bricks/datablocks.cs");

exec("./src/packages/packages.cs");
exec("./src/packages/packageDelivery.cs");
exec("./src/generation/debris.cs");


schedule(1000, 0, eval, "PlayerDropPoints.getObject(0).rayHeight = 1;");