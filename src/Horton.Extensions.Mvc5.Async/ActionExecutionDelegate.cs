﻿using System.Threading.Tasks;
using System.Web.Mvc;

namespace Horton.Extensions.Mvc5.Async
{
    public delegate Task<ActionExecutedContext> ActionExecutionDelegate();
}
