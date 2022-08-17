using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace DeviceVolume
{
    static class Vault {
        public static NAudio.CoreAudioApi.MMDeviceEnumerator deviceEnum = new NAudio.CoreAudioApi.MMDeviceEnumerator();
        public static string deviceName = "Moondrop Nekocake";
        public static float defaultVolume = 0.2f;
    }
    static class Program
    {
        private static async Task Main(string[] args)
        {
            MMDevice defaultDevice = Vault.deviceEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            //while (true)
            //{
            //    int readableVolume = (int)(defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            //    Console.WriteLine($"{defaultDevice.DeviceFriendlyName} | {readableVolume}% => {(int)(Vault.defaultVolume * 100)}%");
            //    defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = Vault.defaultVolume;
            //}
            Console.WriteLine("---------- All devices ----------");
            foreach (MMDevice device in Vault.deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active))
            {
                int readableVolume = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
                Console.WriteLine($"{readableVolume}% | {device.DeviceFriendlyName}");
            }
            Console.WriteLine("---------------------------------");
            NAudioIMMNotificationClient client = new NAudioIMMNotificationClient();
            Vault.deviceEnum.RegisterEndpointNotificationCallback(client);
            await Task.Delay(-1);
        }
    }
}
