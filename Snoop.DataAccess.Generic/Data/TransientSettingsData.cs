// ReSharper disable once CheckNamespace
namespace Snoop.Data
{
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Newtonsoft.Json;

    public sealed class TransientSettingsData
    {
        public TransientSettingsData()
        {
            this.MultipleAppDomainMode = MultipleAppDomainMode.Ask;
            this.MultipleDispatcherMode = MultipleDispatcherMode.Ask;
            this.SetWindowOwner = true;
        }

        public static TransientSettingsData Current { get; private set; }

        public SnoopStartTarget StartTarget { get; set; } = SnoopStartTarget.SnoopUI;

        public MultipleAppDomainMode MultipleAppDomainMode { get; set; }

        public MultipleDispatcherMode MultipleDispatcherMode { get; set; }

        public bool SetWindowOwner { get; set; }

        public long TargetWindowHandle { get; set; }
        public string PathToSnoop { get; set; }

        public string WriteToFile()
        {
            var settingsFile = Path.GetTempFileName();

            Trace.WriteLine($"Writing transient settings file to \"{settingsFile}\"");

            using (var stream = new FileStream(settingsFile, FileMode.Create)) {
                var buff = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
                stream.Write(buff, 0, buff.Length);
            }

            return settingsFile;
        }

        public static TransientSettingsData LoadCurrentIfRequired(string settingsFile)
        {
            if (Current != null)
            {
                return Current;
            }

            return LoadCurrent(settingsFile);
        }

        public static TransientSettingsData LoadCurrent(string settingsFile)
        {
            Trace.WriteLine($"Loading transient settings file from \"{settingsFile}\"");

            using (var stream = new FileStream(settingsFile, FileMode.Open)) {
                var buff = new byte[stream.Length];
                stream.Read(buff, 0, buff.Length);
                var str = Encoding.UTF8.GetString(buff);
                return Current = JsonConvert.DeserializeObject<TransientSettingsData>(str);
            }
        }
    }

    public enum MultipleAppDomainMode
    {
        Ask = 0,
        AlwaysUse = 1,
        NeverUse = 2
    }

    public enum MultipleDispatcherMode
    {
        Ask = 0,
        AlwaysUse = 1,
        NeverUse = 2
    }

    public enum SnoopStartTarget
    {
        SnoopUI = 0,
        Zoomer = 1
    }
}