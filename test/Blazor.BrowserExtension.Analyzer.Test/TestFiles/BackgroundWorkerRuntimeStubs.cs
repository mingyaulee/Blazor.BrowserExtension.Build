using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebExtensions.Net;

namespace Blazor.BrowserExtension
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BackgroundWorkerMainAttribute : Attribute
    {
    }

    public abstract class BackgroundWorkerBase
    {
        protected virtual Dictionary<string, object> JsInstance { get; set; }
        public IWebExtensionsApi WebExtensions { get; set; }
        public ILogger<BackgroundWorkerBase> Logger { get; set; }

        public abstract void Main();
    }
}
