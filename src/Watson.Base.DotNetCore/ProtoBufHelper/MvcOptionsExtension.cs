﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Watson.Base.DotNetCore.ProtoBufHelper
{
    public static class MvcOptionsExtension
    {
        public static void AddProtobufSupport(this MvcOptions options, string contentType = "application/x-protobuf",
            string format = "protobuf")
        {
            if (string.IsNullOrWhiteSpace(contentType)) throw new ArgumentNullException(nameof(contentType));
            if (string.IsNullOrWhiteSpace(format)) throw new ArgumentNullException(nameof(format));

            options.InputFormatters.Add(new ProtobufInputFormatter());
            options.OutputFormatters.Add(new ProtobufOutputFormatter(contentType));
            options.FormatterMappings.SetMediaTypeMappingForFormat(format, MediaTypeHeaderValue.Parse(contentType));
        }
    }
}
