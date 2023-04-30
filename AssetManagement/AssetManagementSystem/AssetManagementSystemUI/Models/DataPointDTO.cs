using System.Runtime.Serialization;
using System.Xml.Linq;

namespace AssetManagementSystemUI.Models
{
    [DataContract]
    public class DataPointDTO
    {
        
        public DataPointDTO(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

        [DataMember(Name = "label")]
        public string Label = "";

        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }
}
