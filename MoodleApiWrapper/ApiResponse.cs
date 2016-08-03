using MoodleApiWrapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoodleApiWrapper
{
    /// <summary>
    /// Represents the API response.
    /// </summary>
    /// <typeparam name="T">The type of data that's going to be contained in the response.</typeparam>

    public class AuthentiactionResponse<T> where T : IDataModel
    {
        public T Data { get; private set; }

        public AuthenticationError Error { get; private set; }

        internal AuthentiactionResponse(AuthentiactionResponseRaw rawResponse)
        {
            this.Error = rawResponse.Error.ToObject<AuthenticationError>();
            this.Data = rawResponse.Data.ToObject<T>();
        }
    }

    internal class AuthentiactionResponseRaw
    {
        internal JObject Data { get; set; }
        internal JObject Error { get; set; }

        public AuthentiactionResponseRaw(JObject data)
        {
            Data = data;
            Error = data;
        }
    }


    public class ApiResponse<T> where T : IDataModel
    {
        /// <summary>
        /// Gets the API response data.
        /// </summary>
        public string Status { get; private set; }

        public T[] DataArray { get; private set; }

        public T Data { get; private set; }

        public Error Error { get; private set; }
    
        internal ApiResponse(ApiResponseRaw rawResponse)
        {
            this.Error = rawResponse.Error.ToObject<Error>();

            if (Error.errorcode == null && Error.exception == null && Error.message == null)
                Status = "Succesful";
            else
                Status = "Failed";

            if (null != rawResponse.DataArray)
                this.DataArray = rawResponse.DataArray.ToObject<T[]>();
            else
                this.Data = rawResponse.Data.ToObject<T>();
        }
    }

    internal class ApiResponseRaw
    {
        public ApiResponseRaw(JObject data)
        {
            Data = data;
            Error = data;
        }

        public ApiResponseRaw(JArray data)
        {
            DataArray = data;
            Error = new JObject();
        }

        internal JArray DataArray { get; set; }
        internal JObject Data { get; set; }
        internal JObject Error { get; set; }
    }
}