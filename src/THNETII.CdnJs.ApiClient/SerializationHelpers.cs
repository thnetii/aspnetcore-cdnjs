using System;
using System.Collections.Generic;
using System.Text;

using THNETII.Common;

namespace THNETII.CdnJs.ApiClient
{
    internal static class SerializationHelpers
    {
        public static DuplexConversionTuple<string?, Uri?> CreateUriConversionTuple() =>
            new DuplexConversionTuple<string?, Uri?>(
                rawConvert: s => s is null ? null : new Uri(s),
                rawReverseConvert: uri => uri?.OriginalString
                );
    }
}
