using examenporject_Frameworks_zenodesp.Areas.Identity.Data;

namespace examenporject_Frameworks_zenodesp.Services
{
	public class RequestTracker
	{
		readonly RequestDelegate _requestDelegate;
		readonly IMyUser _myUser;

		class requesttrack
		{
			public EmployeeUser user;
			public int requestcount;
			public string milestone;
		}

		static Dictionary<string, requesttrack> StatDictionary;

		public RequestTracker(RequestDelegate requestDelegate)
		{
			StatDictionary = new Dictionary<string, requesttrack>();
			_requestDelegate = requestDelegate;
			
		}

		public async Task Invoke(HttpContext context, IMyUser myUser)
		{
			
				
				try
				{
					requesttrack request = StatDictionary[myUser.User().UserName];
					request.requestcount++;
					switch(request.requestcount)
				{
						case 1:
							request.milestone = "You made one request!";
							break;
						case 10:
							request.milestone = "You made 10 requests!";
							break;
						case 50:
							request.milestone = "Wow! You made 50 requests!";
							break;
						default:
							request.milestone = "You made " + request.requestcount.ToString() + " requests!";
							break;
					}
					StatDictionary[myUser.User().UserName] = request;
				}
				catch
				{
					requesttrack request = new requesttrack();
					
					request.user = myUser.User();
					request.requestcount = 1;
					request.milestone = "You made one request!";
					StatDictionary.Add(myUser.User().UserName, request);
				}
			
			await _requestDelegate(context);
		}

		public static string Getmilestone(string username)
		{
			try
			{
				return StatDictionary[username].milestone;
			}
			catch
			{
				return "please log in to track your requests.";
			}
		}
	}
}
