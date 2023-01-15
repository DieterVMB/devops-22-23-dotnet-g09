using System.Net;
using System.Text.Json;

namespace VirtualIT.Server.Middleware {
    public class ErrorDetails {
        /// <summary>
        /// The HTTP statuscode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The custom message of the error, most of the time the one from the exception.
        /// </summary>
        public string? Message { get; set; }

        public ErrorDetails(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) {
            StatusCode = (int)statusCode;
            Message = message;
        }
        public override string ToString() {
            return JsonSerializer.Serialize(this);
        }
    }
}
