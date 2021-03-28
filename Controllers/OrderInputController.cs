using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SalesManagementSystem.Setting;
using SalesManagementSystem.Dto;
using SalesManagementSystem.Logic;
using Newtonsoft.Json;

namespace SalesManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderInputController : ControllerBase
    {
        /// <summary>
        /// 設定記述オブジェクト
        /// </summary>
        private readonly IOptions<DBSetting> options = null;

        public OrderInputController(IOptions<DBSetting> options) 
        {
            //設定記述子
            this.options = options;

        }

        /// <summary>
        /// テーブルに全情報を表示する処理。
        /// </summary>
        /// <returns></returns>
        [HttpPost("ListData")] //←【重要Point】[HttpPost("ListData")]と記述する。
        public List<OrderInputEntity> ListData() //public string JsonData()
        {
            List<OrderInputEntity> orderInputEntities = new List<OrderInputEntity>();
            OrderInputLogic logic = new OrderInputLogic(options);
            orderInputEntities = logic.ReadList();

            string jsonData = JsonConvert.SerializeObject(orderInputEntities);

            orderInputEntities = logic.ReadList();

            return orderInputEntities;
        }

        /// <summary>
        /// 新規で登録する処理
        /// </summary>
        /// <param name="orderInputEntity"></param>
        [HttpPost("postTorokuData")]
        public void postTorokuData([FromBody]OrderInputEntity orderInputEntity) 
        {
            OrderInputToDBEntity inputToDBEntity = new OrderInputToDBEntity();

            ClientEntity clientEntity = new ClientEntity();
            clientEntity.ClientIdInputName = int.Parse(orderInputEntity.ClientIdInputName);
            clientEntity.ClientName = orderInputEntity.ClientName;
            clientEntity.Department = orderInputEntity.Department;
            clientEntity.Post = orderInputEntity.Post;
            clientEntity.PostAddress = orderInputEntity.PostAddress;
            clientEntity.Tel = orderInputEntity.Tel;
            clientEntity.Fax = orderInputEntity.Fax;
            clientEntity.Email = orderInputEntity.Email;
            clientEntity.OrderId = long.Parse(orderInputEntity.OrderId);

            OrdersEntity ordersEntity = new OrdersEntity();
            ordersEntity.ClientIdInputName = int.Parse(orderInputEntity.ClientIdInputName);
            ordersEntity.OrderId = long.Parse(orderInputEntity.OrderId);
            ordersEntity.OrderDate = DateTime.Parse(orderInputEntity.OrderDate);
            ordersEntity.DeliveryDate = DateTime.Parse(orderInputEntity.DeliveryDate);
            ordersEntity.Total = long.Parse(orderInputEntity.Total);
            ordersEntity.order1 = orderInputEntity.order1;
            ordersEntity.order1price = int.Parse(orderInputEntity.order1price);
            ordersEntity.order1Num = int.Parse(orderInputEntity.order1Num);
            ordersEntity.order2 = orderInputEntity.order2;
            ordersEntity.order2price = int.Parse(orderInputEntity.order2price);
            ordersEntity.order2Num = int.Parse(orderInputEntity.order2Num);
            ordersEntity.order3 = orderInputEntity.order3;
            ordersEntity.order3price = int.Parse(orderInputEntity.order3price);
            ordersEntity.order3Num = int.Parse(orderInputEntity.order3Num);
            ordersEntity.order4 = orderInputEntity.order4;
            ordersEntity.order4price = int.Parse(orderInputEntity.order4price);
            ordersEntity.order4Num = int.Parse(orderInputEntity.order4Num);
            ordersEntity.order5 = orderInputEntity.order5;
            ordersEntity.order5price = int.Parse(orderInputEntity.order5price);
            ordersEntity.order5Num = int.Parse(orderInputEntity.order5Num);

            OrderInputLogic logic = new OrderInputLogic(options);
            logic.InsertData(clientEntity,ordersEntity); //orderInputEntity inputToDBEntity
        }

        /// <summary>
        /// 登録情報を更新する。
        /// </summary>
        [HttpPost]
        public void torokuDataUpdate([FromBody] OrderInputEntity orderInputEntity) 
        {
        
        }
    }
}
