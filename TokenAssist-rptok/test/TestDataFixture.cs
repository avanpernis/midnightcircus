using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using TokenAssist;

namespace TokenAssistTest
{
    [TestFixture]
    public class TestDataFixture
    {
        private string CharacterFolder
        {
            get
            {
                return Path.Combine(Dropbox.Folder, @"D&D\Characters");
            }
        }

        private void TestCharacter(string pathSuffix)
        {
            string source = Path.Combine(CharacterFolder, pathSuffix);
            string destination = Path.GetTempFileName();

            Character character = CharacterLoader.Load(source);

            CharacterTokenBuilder.WriteToken(character, destination, null, null);
        }

        [Test]
        public void TestBuli()
        {
            TestCharacter(@"Buli - Fighter\Buli.dnd4e");
        }

        [Test]
        public void TestNard()
        {
            TestCharacter(@"Nard - Devoted Cleric\Nard.dnd4e");
        }

        [Test]
        public void TestGrish()
        {
            TestCharacter(@"Grish\Grish.dnd4e");
        }

        [Test]
        public void TestKumi()
        {
            TestCharacter(@"Kumi - Beastmaster Ranger\Kumi.dnd4e");
        }
    }
}
