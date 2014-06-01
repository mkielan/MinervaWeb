using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace Minerva.Common.Test
{
    [TestClass]
    public class FileStorageUnitTest
    {
        string _path;
        string _tmpPath;
        string[] _testFiles = new[] {
            "test1.txt", 
            "test2.txt", 
            "test3.txt"
        };
        FileStorage _fs;
        FileStorage _fsTmp;

        [TestInitialize()]
        public void Init()
        {
            _path = Directory.GetParent(
                        Directory.GetCurrentDirectory()
                    ).Parent.Parent.FullName + "\\TestFiles";

            _tmpPath = _path + "\\tmp";

            if (Directory.Exists(_tmpPath))
                Directory.Delete(_tmpPath, true);

            _fs = new FileStorage(_path);
            _fsTmp = new FileStorage(_tmpPath);
        }

        [TestMethod]
        public void TestGet()
        {
            var file = _fs.Get(_testFiles[0]);

            Assert.AreNotEqual(null, file);
            Assert.AreNotEqual(null, file.Name);
            Assert.AreNotEqual(null, file.Content);
            Assert.AreNotEqual(0, file.Content.Length);

            var files = _fs.Get(_testFiles);
            Assert.AreEqual(_testFiles.Length, files.Count);

            try {
                _fsTmp.Get(_testFiles);
                Assert.Fail("Files should not be found");
            }
            catch (Exception exc)
            {
            }
        }

        [TestMethod]
        public void TestSave()
        {
            var count = 0;

            foreach (var filename in _testFiles)
            {
                Assert.AreEqual(count++, _fsTmp.Count);

                var file = _fs.Get(filename);

                _fsTmp.Save(file);
            }

            Assert.AreEqual(count, _fsTmp.Count);
        }

        [TestMethod]
        public void TestDelete()
        {
            _fsTmp.Save(new List<File>(_fs.Get(_testFiles)).ToArray());

            var count = _testFiles.Length;

            foreach (var filename in _testFiles)
            {
                Assert.AreEqual(count--, _fsTmp.Count);

                var file = _fs.Get(filename);

                _fsTmp.Delete(file);
            }

            Assert.AreEqual(count, _fsTmp.Count);
        }
    }
}
