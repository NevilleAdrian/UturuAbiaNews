using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
	public static class UrlHelper
	{
	public static string AbsoluteAction(this IUrlHelper url, string actionName,	string controllerName, object routeValues = null)
	{
		string scheme = url.ActionContext.HttpContext.Request.Scheme;
		return url.Action(actionName, controllerName, routeValues, scheme);
	}
	}
}
