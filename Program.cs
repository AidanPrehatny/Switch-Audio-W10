namespace switch_audio
{
    using AudioSwitcher.AudioApi;
    using AudioSwitcher.AudioApi.CoreAudio;
    using System;
    using System.Linq;
    class Program
    {
        private const string HEADSET = "Speakers (HyperX Cloud Flight S Game)";
        private const string SPEAKERS = "Speakers (2- Realtek(R) Audio)";

        static void Main(string[] args)
        {
            var ctlr = new CoreAudioController();
            var devices = ctlr.GetDevices(DeviceType.Playback, DeviceState.Active);

            
            //  deviceType, AudioApi.Role role
            var currentDefault = ctlr.GetDefaultDevice(DeviceType.Playback, Role.Multimedia);
            var currentDefaultName = currentDefault.FullName;
            CoreAudioDevice headset = null;
            CoreAudioDevice speakers = null;
            try {
                 headset =
                    (from d in devices
                    where d.FullName == HEADSET
                    select d).Single();

                 speakers =
                    (from d in devices
                    where d.FullName == SPEAKERS
                    select d).Single();
            } catch {
                Console.WriteLine("Single() call failed");
            }

            if (currentDefaultName == HEADSET) {
                Console.WriteLine($"Setting {HEADSET} as default device");
                speakers.SetAsDefault();
                speakers.SetAsDefaultCommunications();
            } else if (currentDefaultName == SPEAKERS) {
                Console.WriteLine($"Setting {SPEAKERS} as default device");
                headset.SetAsDefault();
                headset.SetAsDefaultCommunications();
            }
        }
    }
}
