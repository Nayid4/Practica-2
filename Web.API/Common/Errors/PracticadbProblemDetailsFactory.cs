using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using System.Xml.Serialization;
using Web.API.Common.Http;

namespace Web.API.Common.Errors
{
    public class PracticadbProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;

        public PracticadbProblemDetailsFactory(ApiBehaviorOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null, 
            string? detail = null, 
            string? instance = null)
        {
            statusCode ??= 500;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            this.ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;

        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext, 
            ModelStateDictionary modelStateDictionary, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null, 
            string? detail = null, 
            string? instance = null)
        {
            if(modelStateDictionary == null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }

            statusCode ??= 400;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance
            };

            if(title != null)
            {
                problemDetails.Title = title;
            }

            this.ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;

        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode) 
        {
            problemDetails.Status ??= statusCode;

            if(_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            if(traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            var errors = httpContext?.Items[HttpContextItemsKeys.Errors] as List<Error>;

            if(errors != null)
            {
                problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
            }

        }

    }
}
