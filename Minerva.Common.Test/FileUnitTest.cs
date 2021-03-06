﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IOFile = System.IO.File;

namespace Minerva.Common.Test
{
    [TestClass]
    public class FileUnitTest
    {
        string path;
        string tmpPath;
        string[] testFiles = new [] {
            "test1.txt", 
            "test2.txt", 
            "test3.txt"
        };

        [TestInitialize()]
        public void Init()
        {
            path = Directory.GetParent(
                        Directory.GetCurrentDirectory()
                    ).Parent.Parent.FullName + "\\TestFiles";

            tmpPath = path + "\\tmp";

            if(Directory.Exists(tmpPath))
                Directory.Delete(tmpPath, true);
        }

        [TestMethod]
        public void TestFilesExists()
        {
            Assert.IsTrue(Directory.Exists(path));
            foreach(var fileName in testFiles) {
                Assert.IsTrue(
                    IOFile.Exists(
                        string.Format("{0}\\{1}", path, fileName)
                        )
                    );
            }
        }

        [TestMethod]
        public void TestNew()
        {
            var file = new File();
            Assert.IsNull(file.Name);
            Assert.IsNull(file.Content);

            try {
                new File(testFiles[0]);
                Assert.Fail("Not throw exception");
            }
            catch(Exception exc) {
                Assert.IsTrue(exc is ArgumentException);
            }

            foreach (var filename in testFiles)
            {
                file = new File(string.Format("{0}\\{1}", path, filename));

                Assert.AreEqual(file.Name, filename);
                Assert.IsNotNull(file.Content);
            }
        }

        [TestMethod]
        public void TestSave()
        {
            Directory.CreateDirectory(tmpPath);

            var count = 0;
            foreach (var filename in testFiles)
            {
                Assert.AreEqual(count++, Directory.GetFiles(tmpPath).Length);
                
                var f = new File(path, filename);
                f.Save(tmpPath);
            }

            Assert.AreEqual(count, Directory.GetFiles(tmpPath).Length);
        }
    }
}
