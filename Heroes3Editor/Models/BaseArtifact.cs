using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes3Editor.Models
{
    public class BaseArtifact
    {
        internal Dictionary<byte, string> _namesByCode { get; set; }

        internal Dictionary<byte, string> _HOTANamesByCode { get; set; }

        internal Dictionary<string, byte> _codesByName => _namesByCode?.ToDictionary(i => i.Value, i => i.Key);

        public Dictionary<byte, string> GetArtifacts => _namesByCode.Where(x => x.Value != "None").ToDictionary(x => x.Key, x => x.Value);
        public BaseArtifact()
        {
            _namesByCode = new Dictionary<byte, string>();
            _HOTANamesByCode = new Dictionary<byte, string>();
        }

        public string[] Names => _namesByCode?.Values.ToArray();

        public string this[byte key] => _namesByCode[key];

        public byte this[string key] => _codesByName[key];

        public void LoadHotaReferenceCodes()
        {
            foreach (var code in _HOTANamesByCode)
            {
                _namesByCode.Add(code.Key, code.Value);
            }
        }

        public void RemoveHotaReferenceCodes()
        {
            foreach (var kvp in _HOTANamesByCode)
            {
                if (_namesByCode.ContainsKey(kvp.Key)) {
                    _namesByCode.Remove(kvp.Key);
                }
                
            }
            
        }
    }
}
