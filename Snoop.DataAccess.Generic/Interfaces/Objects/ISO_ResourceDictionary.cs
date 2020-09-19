namespace Snoop.DataAccess.Interfaces {
    using System.Collections;
    using System.Collections.Generic;

    public interface ISO_ResourceDictionary : ISnoopObject {
        ICollection GetKeys();
        ICollection GetValues();
        object GetValue(object key);
        ICollection<ISO_ResourceDictionary> GetMergedDictionaries();
    }
}