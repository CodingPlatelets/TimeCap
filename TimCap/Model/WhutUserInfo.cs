using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TimCap.Model
{
    public class WhutUserInfo
    {
        [JsonPropertyName("sno")]
        public string Sno { get; set; }
        [JsonPropertyName("cardno")]
        public string CardNo { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("birth")]
        public string Birth { get; set; }
        [JsonPropertyName("sex")]
        public string Sex { get; set; }
        [JsonPropertyName("sexname")]
        public string SexName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("deptcode")]
        public string DepartmentCode { get; set; }
        [JsonPropertyName("deptcodename")]
        public string DepartmentName { get; set; }
        [JsonPropertyName("bj")]
        public string Class { get; set; }
        [JsonPropertyName("zydm")]
        public string MajorCode { get; set; }
        [JsonPropertyName("zydmname")]
        public string MajorName { get; set; }
        [JsonPropertyName("nj")]
        public string Grade { get; set; }
        [JsonPropertyName("uclass")]
        public string UserTypeCode { get; set; }
        [JsonPropertyName("uclassname")]
        public string UserType { get; set; }

    }
}
