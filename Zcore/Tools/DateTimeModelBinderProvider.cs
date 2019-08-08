using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbCore.Models;
using LibCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zcore.Tools
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime) ||
                context.Metadata.ModelType == typeof(DateTime?) ||
                typeof(IPrimaryKeyContainer).IsAssignableFrom(context.Metadata.ModelType))
            {
                return new DateTimeModelBinder();
            }

            return null;
        }
    }
}
