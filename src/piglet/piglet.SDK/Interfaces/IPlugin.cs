﻿// Copyright (c) Den Delimarsky
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Piglet.SDK.Models;

namespace Piglet.SDK.Interfaces
{
    public interface IPlugin
    {
        public PluginMetadata Metadata { get; }

        public List<Type> GetSupportedCommands();
    }
}
