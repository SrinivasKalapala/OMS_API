using OMS.DataAccess;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OMS_API
{
    public class APIAuthentication : AuthorizationFilterAttribute
    {
        /// <summary>
        /// OverRiding the base class AuthorizationFilterAttribute method
        /// And Register the APIAuthentication class in WebApiConfig.cs to make sure the API calls Authentication
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Checking Header has Authorization key or not. If not returning as unAuthorized
            if (actionContext.Request.Headers.Authorization == null)
            {
                UnAuthorizedUser(actionContext);
            }
            else   // If Header has the key Authorization
            {
                // Reading the key from the header Key will be of BASE64Encoding type
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                // Converting BASE64 to String
                string DecodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                // As Key has generated in a syntax of UserName:Password
                // Splitting the decoded string by :
                string[] UserNamePasswordArry = DecodedAuthenticationToken.Split(':');

                string UserName = UserNamePasswordArry[0];
                string Password = UserNamePasswordArry[1];

                if (UserName != "" && Password != "")
                {
                    CommonDataAccess _commonDataAccess = new CommonDataAccess();
                    String User = _commonDataAccess.AuthenticateUser(UserName, Password);

                    if (User != "")
                    {
                        // If the Authorized user, Adding the user to the thread to access the API
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(User), null);
                    }
                    else
                    {
                        UnAuthorizedUser(actionContext);
                    }
                }
                else
                {
                    UnAuthorizedUser(actionContext);
                }
            }
        }

        public void UnAuthorizedUser(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}