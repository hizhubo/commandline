// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLine.Core
{
    static class PreprocessorGuards
    {
        public static IEnumerable<Func<IEnumerable<string>, IEnumerable<Error>>>
            Lookup(StringComparer nameComparer, bool autoHelp, bool autoVersion)
        {
            var list = new List<Func<IEnumerable<string>, IEnumerable<Error>>>();
            if (autoHelp)
                list.Add(HelpCommand(nameComparer));
            if (autoVersion)
                list.Add(VersionCommand(nameComparer));
            return list;
        }

        public static Func<IEnumerable<string>, IEnumerable<Error>> HelpCommand(StringComparer nameComparer)
        {

            return
                arguments =>
                {
                    var firstArg = arguments.First();
                    var requestedHelp = nameComparer.Equals("--help", firstArg) || nameComparer.Equals("-h", firstArg);
                    return requestedHelp
                        ? new Error[] { new HelpRequestedError() }
                        : Enumerable.Empty<Error>();
                };
        }

        public static Func<IEnumerable<string>, IEnumerable<Error>> VersionCommand(StringComparer nameComparer)
        {
            return
                arguments =>
                {
                    var firstArg = arguments.First();
                    var requestedVersion = nameComparer.Equals("--version", firstArg) || nameComparer.Equals("-v", firstArg);
                    return requestedVersion
                        ? new Error[] { new VersionRequestedError() }
                        : Enumerable.Empty<Error>();
                };
        }
    }
}
