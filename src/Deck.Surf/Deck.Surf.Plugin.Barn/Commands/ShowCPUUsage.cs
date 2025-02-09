﻿using Deck.Surf.Plugin.Barn.Helpers;
using Deck.Surf.SDK.Core;
using Deck.Surf.SDK.Interfaces;
using Deck.Surf.SDK.Models;
using Deck.Surf.SDK.Util;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace Deck.Surf.Plugin.Barn.Commands
{
    [CompatibleWith(DeviceModel.XL)]
    class ShowCPUUsage : IDSCommand
    {
        private const string CategoryName = "Processor";
        private const string CounterName = "% Processor Time";
        private const string InstanceName = "_Total";

        public string Name => "Launch Application";
        public string Description => "Launches an application on the machine.";

        public void ExecuteOnAction(CommandMapping mappedCommand, ConnectedDevice mappedDevice, int activatingButton = -1)
        {
            
        }

        public void ExecuteOnActivation(CommandMapping mappedCommand, ConnectedDevice mappedDevice)
        {
            var cpuUsageTimer = new System.Timers.Timer(2000);
            cpuUsageTimer.Elapsed += (s, e) =>
            {
                var randomIconFromText = IconGenerator.GenerateTestImageFromText(GetCPUUsage().ToString() + "%", new Font("Bahnschrift", 94), Color.Red, Color.Black);
                var resizeImage = ImageHelpers.ResizeImage(ImageHelpers.GetImageBuffer(randomIconFromText), DeviceConstants.XLButtonSize, DeviceConstants.XLButtonSize);

                DeviceManager.SetKey(mappedDevice, mappedCommand.ButtonIndex, resizeImage);
            };
            cpuUsageTimer.Start();
        }

        private static int GetCPUUsage()
        {
            PerformanceCounter perfCounter = new(CategoryName, CounterName, InstanceName);
            // Dummy call because PerformanceCounter will always start with zero.
            perfCounter.NextValue();
            Thread.Sleep(1000);
            var targetCPUUsage = (int)Math.Round(perfCounter.NextValue());
            return targetCPUUsage;
        }
    }
}
