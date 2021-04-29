using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Calendar {
    // The class responsible for validating the city name.
    class CityNameHandler {
        // Function that checks if there is an error in communication with the API or if the city name is correct.
        public bool checkUrl(string url) {
            // boolean variable used to verify the correct city name etc.
            bool isValid;
            try {
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse()) {
                    using (var responseStream = response.GetResponseStream()) {
                        isValid = false;
                    }
                }
            } catch (WebException exception) {
                if (exception.Status == WebExceptionStatus.ProtocolError && exception.Response != null) {
                    var response = (HttpWebResponse)exception.Response;
                    if (response.StatusCode == HttpStatusCode.NotFound) {
                        isValid = true;
                    } else {
                        isValid = true;
                    }
                } else {
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}
