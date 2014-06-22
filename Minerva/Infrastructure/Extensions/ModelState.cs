using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minerva
{
    public static class ModelStateExtensions
    {
        public static IDictionary ToSerializedDictionary(this ModelStateDictionary modelState)
        {
            return modelState.ToDictionary(
                k => k.Key,
                v => v.Value.Errors.Select(x => x.ErrorMessage).ToArray()
            );
        }
    }
}