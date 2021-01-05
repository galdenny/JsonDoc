using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Dennysoft.Core.JsonDoc;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Dennysoft.Core.JsonDoc.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var obj = new DerivedB() { STRING = "str1", STRING2 = "str2" };

            var result = JsonDoc.ToDocumentedJson(obj);


            //output:
            ////This is a class level doc B
            //{

            //    //This is a string2 property
            //    "STRING2": "str2",

            //    //This is a string property
            //    "STRING": "str1"
            //}


            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            var obj = new TestA()
            {
                TEST_B_2 = new TestB()
                {
                    TestCs = new ObservableCollection<TestC> { new TestC() { INT = 111, TestDs = new List<TestD> { new TestD() } }, new TestC() { INT = 112 }, new TestC() { INT = 62263, TestDs = new List<TestD> { new TestD(), new TestD(), new TestD() } } }
                },
                BOOL = true,
                DOUBLE = 4.5,
                STRING = "3343ere",
                TEST_B = new TestB()
                {
                    TestCs = new ObservableCollection<TestC> { new TestC() { INT = 111, TestDs = new List<TestD> { new TestD(), new TestD() } }, new TestC() { INT = 112 }, new TestC() { INT = 113, TestDs = new List<TestD> { new TestD(), new TestD(), new TestD(), new TestD(), new TestD() } } }
                }
            };

            var result = JsonDoc.ToDocumentedJson(obj);


            //output:
            ////This is a test class
            //{
            //  "INT_Field": 22,
            //  "INT": 44,

            //  //This is a double on A
            //  "DOUBLE": 4.5,

            //  //This is a string property on A
            //  "STRING": "3343ere",
            //  "BOOL": true,

            //  //Test B property
            //  //This is a test class B
            //  "TEST_B": {
            //    "INT": 44,
            //    "DOUBLE": 0.0,

            //    //This is a string B property
            //    "STRING": null,
            //    "BOOL": false,

            //    //all the TestCs
            //    //This is a test class C
            //    "TestCs": [
            //      {
            //        "INT": 111,
            //        "DOUBLE": 0.0,

            //        //string C
            //        "STRING": null,
            //        "BOOL": false,

            //        //all the TestDs
            //        //This is a test class D
            //        "TestDs": [
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          },
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          }
            //        ],

            //        //string C 2
            //        "STRING2": null
            //      },
            //      {
            //        "INT": 112,
            //        "DOUBLE": 0.0,

            //        //string C
            //        "STRING": null,
            //        "BOOL": false,

            //        //all the TestDs
            //        //This is a test class D
            //        "TestDs": null,
            //        "STRING2": null
            //      },
            //      {
            //        "INT": 113,
            //        "DOUBLE": 0.0,

            //        //string D
            //        "STRING": null,
            //        "BOOL": false,
            //        "TestDs": [
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          },
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          },
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          },
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          },
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          }
            //        ],

            //        //string C 2
            //        "STRING2": null
            //      }
            //    ],

            //    //This is a string B 2 property
            //    "STRING2": null
            //  },

            //  //This is a string 2 property
            //  "STRING2": null,
            //  "BOOL2": false,

            //  //Test B 2 property
            //  //This is a test class B
            //  "TEST_B_2": {
            //    "INT": 44,
            //    "DOUBLE": 0.0,

            //    //This is a string B property
            //    "STRING": null,
            //    "BOOL": false,

            //    //all the TestCs
            //    //This is a test class C
            //    "TestCs": [
            //      {
            //        "INT": 111,
            //        "DOUBLE": 0.0,

            //        //string C
            //        "STRING": null,
            //        "BOOL": false,

            //        //all the TestDs
            //        //This is a test class D
            //        "TestDs": [
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          }
            //        ],

            //        //string C 2
            //        "STRING2": null
            //      },
            //      {
            //        "INT": 112,
            //        "DOUBLE": 0.0,

            //        //string C
            //        "STRING": null,
            //        "BOOL": false,

            //        //all the TestDs
            //        //This is a test class D
            //        "TestDs": null,
            //        "STRING2": null
            //      },
            //      {
            //        "INT": 62263,
            //        "DOUBLE": 0.0,

            //        //string D
            //        "STRING": null,
            //        "BOOL": false,
            //        "TestDs": [
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          },
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          },
            //          {
            //            "INT": 44,
            //            "DOUBLE": 0.0,

            //            //string D
            //            "STRING": null,
            //            "BOOL": false
            //          }
            //        ],

            //        //string C 2
            //        "STRING2": null
            //      }
            //    ],

            //    //This is a string B 2 property
            //    "STRING2": null
            //  }
            //}


            Assert.Pass();
        }
    }

    [JsonDoc("This is a class level doc A")]
    public class BaseClassA
    {
        [JsonDoc("This is a string property")]
        public string STRING { get; set; }
    }

    [JsonDoc("This is a class level doc B")]
    public class DerivedB : BaseClassA
    {
        [JsonDoc("This is a string2 property")]
        public string STRING2 { get; set; }
    }

    [JsonDoc("This is a test class")]
    public class TestA
    {
        //[JsonDoc("This is an int field")]
        public int INT_Field = 22;
        public int INT { get; set; } = 44;

        [JsonDoc("This is a double on A")]
        public double DOUBLE { get; set; }

        [JsonDoc("This is a string property on A")]
        public string STRING { get; set; }
        public bool BOOL { get; set; }

        [JsonDoc("Test B property")]
        public TestB TEST_B { get; set; }

        [JsonDoc("This is a string 2 property")]
        public string STRING2 { get; set; }
        public bool BOOL2 { get; set; }

        [JsonDoc("Test B 2 property")]
        public TestB TEST_B_2 { get; set; }
    }

    [JsonDoc("This is a test class B")]
    public class TestB
    {
        public int INT { get; set; } = 44;

        public double DOUBLE { get; set; }

        [JsonDoc("This is a string B property")]
        public string STRING { get; set; }
        public bool BOOL { get; set; }

        [JsonDoc("all the TestCs")]
        public ObservableCollection<TestC> TestCs { get; set; }

        [JsonDoc("This is a string B 2 property")]
        public string STRING2 { get; set; }
    }

    [JsonDoc("This is a test class C")]
    public class TestC
    {
        public int INT { get; set; } = 44;

        public double DOUBLE { get; set; }

        [JsonDoc("string C")]
        public string STRING { get; set; }
        public bool BOOL { get; set; }

        [JsonDoc("all the TestDs")]
        public List<TestD> TestDs { get; set; }

        [JsonDoc("string C 2")]
        public string STRING2 { get; set; }
    }

    [JsonDoc("This is a test class D")]
    public class TestD
    {
        public int INT { get; set; } = 44;

        public double DOUBLE { get; set; }

        [JsonDoc("string D")]
        public string STRING { get; set; }
        public bool BOOL { get; set; }
    }
}