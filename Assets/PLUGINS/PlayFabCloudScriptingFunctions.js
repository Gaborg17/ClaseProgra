handlers.setUserData = function (args, context)
{
	var data = {};
	
	data[args.key] = args.value.toString;

	server.UpdateUserData(
	{
		PlayFabID: currentPlayerID,
		Data: data
	});
	return  {
		Success: true
		};
};