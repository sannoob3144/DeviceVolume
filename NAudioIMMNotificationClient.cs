using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;

namespace DeviceVolume
{
    class NAudioIMMNotificationClient : NAudio.CoreAudioApi.Interfaces.IMMNotificationClient
    {
        public void OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            MMDevice mmDevice = Vault.deviceEnum.GetDevice(deviceId);
            if (mmDevice.DataFlow != DataFlow.Render) return;
            Console.WriteLine($"[DeviceStateChanged] {mmDevice.FriendlyName} {newState}");
            if (mmDevice.DeviceFriendlyName == Vault.deviceName && newState == DeviceState.Active)
            {
                Console.WriteLine($"[NAudioIMMNotificationClient] Device Plugged, change volume to {(int)(Vault.defaultVolume * 100)}");
                mmDevice.AudioEndpointVolume.MasterVolumeLevelScalar = Vault.defaultVolume;
            }
        }
        public void OnDeviceAdded(string pwstrDeviceId)
        {
            return;
        }

        public void OnDeviceRemoved(string deviceId)
        {
            return;
        }

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId)
        {
            return;
        }

        public void OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key)
        {
            return;
        }
    }
}
