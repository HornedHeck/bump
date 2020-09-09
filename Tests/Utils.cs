using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests
{
    public static class Utils
    {
        public static ControllerContext GetAuthContext()
        {
            var providerMock = new Mock<IServiceProvider>();
            providerMock
                .Setup(it => it.GetService(typeof(IAuthenticationService)))
                .Returns(new Mock<IAuthenticationService>().Object);
            var httpCtxMock = new Mock<HttpContext>();
            httpCtxMock
                .Setup(it => it.RequestServices)
                .Returns(providerMock.Object);
            var controllerCtx = new ControllerContext()
            {
                HttpContext = httpCtxMock.Object
            };
            return controllerCtx;
        }

        public static string GenerateUrl(string controller, string action) => $"/{controller}/{action}";
    }
}