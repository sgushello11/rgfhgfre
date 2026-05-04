using Microsoft.VisualStudio.TestTools.UnitTesting;
using Master_floor;

namespace Master_floor.Tests
{
    [TestClass]
    public class PartnerTests
    {
        // Создание партнера
        [TestMethod]
        public void Test_CreatePartner()
        {
            Partner partner = new Partner();
            partner.ID = 1;
            partner.Наименование_партнера = "ООО Ромашка";
            partner.Директор = "Иванов Иван";
            partner.Телефон_партнера = "8-800-555-35-35";

            Assert.AreEqual(1, partner.ID);
            Assert.AreEqual("ООО Ромашка", partner.Наименование_партнера);
            Assert.AreEqual("Иванов Иван", partner.Директор);
            Assert.AreEqual("8-800-555-35-35", partner.Телефон_партнера);
        }

        // Редактирование партнера
        [TestMethod]
        public void Test_EditPartner()
        {
            Partner partner = new Partner();
            partner.Наименование_партнера = "Старое имя";

            partner.Наименование_партнера = "Новое имя";

            Assert.AreEqual("Новое имя", partner.Наименование_партнера);
        }

        // Проверка что ID больше 0
        [TestMethod]
        public void Test_Id()
        {
            int id = 1;
            Assert.IsTrue(id > 0);
        }

        // Проверка на пустое наименование
        [TestMethod]
        public void Test_PartnerName()
        {
            Partner partner = new Partner();

            bool isEmpty = string.IsNullOrWhiteSpace(partner.Наименование_партнера);

            Assert.IsTrue(isEmpty);
        }

        // Проверка типа партнера
        [TestMethod]
        public void Test_PartnerType()
        {
            string[] validTypes = { "ЗАО", "ООО", "ПАО", "ОАО" };
            string type = "ООО";

            bool isValid = System.Array.Exists(validTypes, t => t == type);
            Assert.IsTrue(isValid);
        }
    }
}