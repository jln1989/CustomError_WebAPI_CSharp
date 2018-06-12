using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPITest.CustomError
{
    /// <summary>
    /// Custom Error Result Class by Lakshmi
    /// </summary>
    public class CustomErrorResult : IHttpActionResult
    {
        CustomErrorClass _customError;
        HttpRequestMessage _request;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomErrorResult"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="request">The request.</param>
        public CustomErrorResult(CustomErrorClass error, HttpRequestMessage request)
        {
            _customError = error;
            _request = request;
        }

        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>HttpResponseMessage</returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {

            List<CustomErrorClass> _customErrorList = new List<CustomErrorClass>
            {
                _customError
            };

            CustomErrorList err = new CustomErrorList()
            {
                CustomErrors = _customErrorList
            };

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<CustomErrorList>(err, new JsonMediaTypeFormatter()),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}