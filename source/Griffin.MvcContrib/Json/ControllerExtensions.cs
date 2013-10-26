﻿using System.Web.Mvc;

namespace SunshineAttack.Localization.Json
{
    /// <summary>
    /// Extension methods for working with structured JSON
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Return a structured JSON response.
        /// </summary>
        /// <param name="controller">Controller returning the result</param>
        /// <param name="success">Request was successful (false probably means that you want to return <see cref="ErrorMessage"/> or <see cref="ModelError"/>)</param>
        /// <param name="content">Content to return</param>
        /// <returns>Structured json</returns>
        public static ActionResult JsonResponse(this Controller controller, bool success, IJsonResponseContent content)
        {
            return new ContentResult
                       {
                           Content = JsonSerializer.Current.Serialize(new JsonResponse(success, content)),
                           ContentType = "application/json"
                       };
        }
    }
}