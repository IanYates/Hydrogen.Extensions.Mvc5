Hydrogen Extensions for ASP.NET MVC 5
=====================================

Hydrogen.Extensions.Mvc5.Async
------------------------------

**Get it on [NuGet](https://www.nuget.org/packages/Hydrogen.Extensions.Mvc5.Async).**

Adds support for async filters (i.e. action filters, etc.) by implementing a custom `IAsyncActionInvoker`.

Read the [blog article](http://josephdaigle.me/2017/03/20/Hydrogen.Extensions.Mvc5-alpha-release.html) about the alpha release.

**How to use:**

Swap out the default implementation `IAsyncActionInvoker` with `Hydrogen.Extensions.Mvc5.Async.ControllerActionInvokerEx`. There are two ways to do this:

1. Set the `ActionInvoker` property of a controller when it's constructed.


    public class HomeController : Controller
    {
        public HomeController()
        {
            ActionInvoker = ControllerActionInvokerEx.Instance;
        }
    }

2. Using your DI container of choice, register `Hydrogen.Extensions.Mvc5.Async.ControllerActionInvokerEx` as an implementation of `IAsyncActionInvoker`. You may register `ControllerActionInvokerEx.Instance` as a singleton.

For example (using Autofac):

    builder.RegisterInstance(ControllerActionInvokerEx.Instance)
            .As<IAsyncActionInvoker>()
            .SingleInstance();

**Creating an async filter**

You may create a subclass of `AsyncActionFilterAttribute` or `AsyncExceptionFilterAttribute` and override the async methods.

For example:

    public class MyAsyncActionFilterAttribute : AsyncActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Execute code before the action is invoked.
            // You can "short-circuit" the action by setting 'context.Result'
            // and returning before calling 'next()'.
            
            // The implementation is responsible for calling 'next()'.
            var actionExecutedContext = await next().ConfigureAwait(false);

            // Execute code after the action is invoked
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Execute code before the result is invoked.
            // You can "short-circuit" the action by setting 'context.Canceled = true'
            // and returning before calling 'next()'.
            
            // The implementation is responsible for calling 'next()'.
            var resultExecutedContext = await next().ConfigureAwait(false);

            // Execute code after the result is invoked
        }
    }

Alternately you may implement any of the async filters:

* `IAsyncActionFilter`
* `IAsyncResultFilter`
* `IAsyncExceptionFilter`
* `IAsyncAuthorizationFilter`

Unfortunately, for compatibility reasons, you must also implement the non-async version of these filters (e.g. `void OnActionExecuting(ActionExecutingContext filterContext)`). However, these methods can be a NOOP since the code is never executed by `ControllerActionInvokerEx`.

This library, currently, does not implement as async version of `IAuthenticationFilter`.
