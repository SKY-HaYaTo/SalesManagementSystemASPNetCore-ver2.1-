using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagementSystem.Dto
{
    public class ClientEntity
    {
        public int ClientIdInputName { get; set; } //クライアントID int
        public string ClientName { get; set; } //会社名
        public string Department { get; set; } //部署名
        public string Post { get; set; } //郵便番号
        public string PostAddress { get; set; } //住所
        public string Tel { get; set; } //TEL
        public string Fax { get; set; } //Fax
        public string Email { get; set; } //email
        public long OrderId { get; set; } //注文ID long
    }
}
