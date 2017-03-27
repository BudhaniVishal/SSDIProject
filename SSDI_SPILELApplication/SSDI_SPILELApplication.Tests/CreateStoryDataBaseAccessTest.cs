using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseAccessLayer;
using NUnit.Framework;
using DataBaseAccessLayer.ConnectionClass;

namespace SSDI_SPILELApplication.Tests
{
    [TestFixture]
    public class CreateStoryDataBaseAccessTest
    {
        private string dataBaseName = "spielDBTest";
        [Test]
        public void TestEditorCreateStory()
        {
            bool result = false;
            ConnStoryTable obj = new ConnStoryTable();
            obj.Title = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(" ", "") + "Test";
            obj.Scenario = "Test";
            obj.Genre = "Test";
            obj.Type = "Test";
            obj.To = "01/01/2017";
            obj.From = "01/02/2017";
            result = DatabaseAccess.CreateStory(obj, dataBaseName);
            Assert.IsTrue(result); // value saved

        }
    }
}
