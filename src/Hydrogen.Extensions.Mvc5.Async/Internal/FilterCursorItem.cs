﻿namespace Hydrogen.Extensions.Mvc5.Async.Internal
{
    public struct FilterCursorItem<TFilter, TFilterAsync>
    {
        public FilterCursorItem(TFilter filter, TFilterAsync filterAsync)
        {
            Filter = filter;
            FilterAsync = filterAsync;
        }

        public TFilter Filter { get; }

        public TFilterAsync FilterAsync { get; }
    }
}
