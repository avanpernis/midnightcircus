using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using TokenAssist;

using NUnit.Framework;

namespace TokenAssistTest
{
    [TestFixture]
    class MonsterFixture
    {
        protected string ResourceAsFile(byte[] resource, string filename="tokenassist.tmp")
        {
            string tempPath = System.IO.Path.GetTempPath();
            string tempFile = System.IO.Path.Combine(tempPath, filename);

            using (MemoryStream inputStream = new MemoryStream(resource))
            using (FileStream outSteam = new FileStream(tempFile, FileMode.Create))
            {
                inputStream.CopyTo(outSteam);
            }

            return tempFile;
        }

        [Test]
        public void TestLoadVecna()
        {
            string filename = ResourceAsFile(TokenAssistTest.Properties.Resources.Vecna);
            Monster m = MonsterLoader.Load(filename);

            Assert.IsNotNull(m);
            Assert.IsTrue(m.Name == "Vecna of Doom");

            // verify abilities
            Assert.AreEqual(m.Abilities["Dexterity"].Value, 26);
            Assert.AreEqual(m.Abilities["Dexterity"].Modifier, 8);

            Assert.AreEqual(m.Abilities["Constitution"].Value, 28);
            Assert.AreEqual(m.Abilities["Constitution"].Modifier, 9);
            
            Assert.AreEqual(m.Abilities["Strength"].Value, 25);
            Assert.AreEqual(m.Abilities["Strength"].Modifier, 7);
            
            Assert.AreEqual(m.Abilities["Intelligence"].Value, 34);
            Assert.AreEqual(m.Abilities["Intelligence"].Modifier, 12);

            Assert.AreEqual(m.Abilities["Wisdom"].Value, 32);
            Assert.AreEqual(m.Abilities["Wisdom"].Modifier, 11);

            Assert.AreEqual(m.Abilities["Charisma"].Value, 30);
            Assert.AreEqual(m.Abilities["Charisma"].Modifier, 10);
        }
    }
}
