using System.Text.RegularExpressions;

namespace Constraints
{
    public class SalesReport : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
            {
                return false;
            }

            Regex regex = new Regex($"^(apr|jul|oct|jan)$");
            string? monthValue = (string?)values[routeKey];

            if (regex.IsMatch(monthValue))
            {
                return true; // + se for apr|jul|oct|jan
            }

            return false;
        }
    }
};