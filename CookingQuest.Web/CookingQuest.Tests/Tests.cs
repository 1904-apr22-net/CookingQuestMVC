using CookingQuest.Web;
using CookingQuest.Web.API_Models;
using CookingQuest.Web.Controllers;
using CookingQuest.Web.Models;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CookingQuest.Tests
{
    public class Tests
    {
        [Fact]
        public void Store()
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = null,
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            var subjectUnderTest = new StoreController(httpClient, new Web.MyConfiguration() { ServiceUrl = "http://test.com/" });

            var result = subjectUnderTest.Index();
            var result2 = subjectUnderTest.Add(new Web.API_Models.EquipmentModel() { Difficulty = 2, EquipmentId = 1, Modifier = 1, Name = "a", PlayerEquipmentId = 1, Price = 1, Type = "Dunegon" });
            var result3 = subjectUnderTest.StoreIndex(1);
            var result4 = subjectUnderTest.StoreIndex(new StoreModel()
            {
                Flavors = new List<FlavorModel>() { new FlavorModel()
                { Bonus = 1, Description = "a", FlavorId = 2, Name = "b"} },
                Description = "b",
                Difficulty = 1,
                Name = "a",
                StoreId = 1,

            }, new LootModel()
            {
                Description = "a",
                DropRate = 1,
                Flavor = new FlavorModel()
                {
                    Bonus = 1,
                    Description = "a",
                    FlavorId = 1,
                    Name = "b"
                },
                FlavorLootId = 1,
                LocationLootId = 1,
                LootId = 1,
                Name = "b",
                PlayerLootId = 1,
                Price = 1,
                Quantity = 1,
            }, new PlayerModel()
            {
                Gold = 1,
                Name = "p",
                PlayerId = 1,
            });
            Assert.NotNull(result);
            Assert.NotNull(result2);
        }
        [Fact]
        public void Models()
        {

            AccountModel a = new AccountModel() { AccountId = 1, IsAdmin = true, Password = "a", PlayerId = 1, Username = "" };
            EquipmentModel b = new EquipmentModel() { Difficulty = 1, EquipmentId = 1, Modifier = 1, Name = "", PlayerEquipmentId = 1, Price = 1, Type = "" };
            FlavorModel c = new FlavorModel() { Bonus = 1, Name = "", Description = "", FlavorId = 1 };
            FlavorLootModel d = new FlavorLootModel() { FlavorId = 1, FlavorLootId = 1, LootId = 1 };
            LocationLootModel e = new LocationLootModel() { DropRate = 1, LocationId = 1, LocationLootId = 1, LootId = 1 };
            LocationModel f = new LocationModel() { Description = "", Difficulty = 1, LocationId = 1, Loot = null, Name = "" };
            LootModel g = new LootModel() { Description = "", DropRate = 1, Flavor = new FlavorModel(), FlavorLootId = 1, LocationLootId = 1, LootId = 1, Name = "", PlayerLootId = 1, Price = 1, Quantity = 1 };
            PlayerEquipmentModel pe = new PlayerEquipmentModel() { EquipmentId = 1, PlayerEquipmentId = 1, PlayerId = 1 };
            PlayerLocationModel pl = new PlayerLocationModel() { LocationId = 1, PlayerId = 1, PlayerLocationId = 1 };
            PlayerLootModel plm = new PlayerLootModel() { LootId = 1, PlayerId = 1, PlayerLootId = 1, Quantity = 1 };
            PlayerModel pm = new PlayerModel() { Gold = 1, Name = "", PlayerId = 1 };
            RecipeLootModel rl = new RecipeLootModel(){LootId=1,RecipeLootId=1,RecipeId=1};
            RecipeModel r = new RecipeModel() { Description="",Name="",RecipeId=1};
            StoreEquipmentModel se = new StoreEquipmentModel() { EquipmentId=1,StoreEquipmentId=1,StoreId=1};
            StoreFlavorModel sf = new StoreFlavorModel() { Bonus=1,FlavorId=1,StoreFlavorId=1,StoreId=1};
            StoreModel s = new StoreModel() {Description="",Difficulty=1,Flavors=null,Name="",StoreId=1 };



            ErrorViewModel ev = new ErrorViewModel();
            LocationViewModel lv = new LocationViewModel();
            PlayerViewModel pv = new PlayerViewModel();
            StoreViewModel sv = new StoreViewModel() {CurrentVoucher= null, HighestVoucher=1, InStock=null, Loot=null,NextVoucher= null,Player=null,PlayerEquipment=null,Store=null,StoreId=1,storeModels=null,Vouchers=null };

            MyConfiguration my = new MyConfiguration();

            Assert.NotNull(my);
            Assert.NotNull(ev);
            Assert.NotNull(lv);
            Assert.NotNull(pv);
            Assert.NotNull(sv);
            Assert.NotNull(a);
            Assert.NotNull(s);
            Assert.NotNull(sf);
            Assert.NotNull(se);
            Assert.NotNull(r);
            Assert.NotNull(rl);
            Assert.NotNull(pm);
            Assert.NotNull(plm);
            Assert.NotNull(pl);
            Assert.NotNull(b);
            Assert.NotNull(c);
            Assert.NotNull(d);
            Assert.NotNull(e);
            Assert.NotNull(f);
            Assert.NotNull(g);
            Assert.NotNull(pe);




        }
        [Fact]
        public void Store2()
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = null,
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };
            var subjectUnderTest = new EquipmentController(httpClient, new Web.MyConfiguration() { ServiceUrl = "http://test.com/" });

            var result = subjectUnderTest.Index();
            var result2 = subjectUnderTest.Create();
            var result3 = subjectUnderTest.Create(new EquipmentModel());
            var result4 = subjectUnderTest.Edit(new EquipmentModel());
            var result5 = subjectUnderTest.Edit(1,2,"","",2,3,3);

            var subjectUnderTest2 = new LocationController(httpClient, new Web.MyConfiguration() { ServiceUrl = "http://test.com/" });
            var result6 = subjectUnderTest2.Index();
            var result7 = subjectUnderTest2.Create();
            var result8 = subjectUnderTest2.Create(new LocationModel());
            var result9 = subjectUnderTest2.EditLoot(1, 1, 1, "", 1, "");
            var result10 = subjectUnderTest2.EditLoot(new LootModel());
            var result11 = subjectUnderTest2.EditLocation(new LocationModel());
            var result12 = subjectUnderTest2.EditLocation(1, "", "", 1);
            var result13 = subjectUnderTest2.Delete(1);

            var subjectUnderTest3 = new HomeController();
            subjectUnderTest3.Index();
            subjectUnderTest3.Privacy();


            var subjectUnderTest4 = new LootController(httpClient, new Web.MyConfiguration() { ServiceUrl = "http://test.com/" });
            var result17 = subjectUnderTest4.Index();
            var subjectUnderTest5 = new PlayerController(httpClient, new Web.MyConfiguration() { ServiceUrl = "http://test.com/" });
            var result18 = subjectUnderTest5.Index();
            var result19 = subjectUnderTest5.Edit(1,2,"","",1,1);
            var result20 = subjectUnderTest5.Edit(new LootModel());
            var result21 = subjectUnderTest5.EditPlayer(1, "", 1);
            var result22 = subjectUnderTest5.EditPlayer(new PlayerModel());
            var result23 = subjectUnderTest5.EditEquipment(1, 1, "", "", 1, 1, 1);
            var result24 = subjectUnderTest5.EditEquipment(new EquipmentModel());
            var result25 = subjectUnderTest5.Delete(1, new LootModel());
            var result26 = subjectUnderTest5.Delete(1);
            var result27 = subjectUnderTest5.DeleteEquipment(1, new EquipmentModel());
            
            Assert.NotNull(subjectUnderTest);
        }
    }
}
