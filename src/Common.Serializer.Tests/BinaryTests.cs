﻿using System;
using Common.Serializer.Binary;
using FluentAssertions;
using NUnit.Framework;

namespace Common.Serializer.Tests
{
    [TestFixture]
    public class BinaryTests
    {
        [Serializable]
        private class IsSerializable
        {
            public int IntProp { get; set; }
            public string SomeString { get; set; }
        }

        [SetUp]
        public void Setup()
        {
            Serialization.Initialize(config =>
            {
                config.DefaultAdapter = new BinarySerializerAdapter();
            });
        }

        private const string Base64Encoded = 
            "AAEAAAD/////AQAAAAAAAAAMAgAAAE5Db21tb24uU2VyaWFsaXplci5UZXN0cywgVmVyc2lvbj0xLjAuMC4wLCBDdWx0dXJ" + 
            "lPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPW51bGwFAQAAADJDb21tb24uU2VyaWFsaXplci5UZXN0cy5CaW5hcnlUZXN0cy" + 
            "tJc1NlcmlhbGl6YWJsZQIAAAAYPEludFByb3A+a19fQmFja2luZ0ZpZWxkGzxTb21lU3RyaW5nPmtfX0JhY2tpbmdGaWVsZ" + 
            "AABCAIAAAABAAAABgMAAAAEVGVzdAs=";

        [Test]
        public void TestDefaultSerialize()
        {
            var result = Serialization.Serialize(new IsSerializable { IntProp = 1, SomeString = "Test" });
            result.Should().Be(Base64Encoded);
        }

        [Test]
        public void TestDefaultDeserialize()
        {
            var expected = new IsSerializable() { IntProp = 1, SomeString = "Test"};
            var result = Serialization.Deserialize<IsSerializable>(Base64Encoded);
            Assert.AreEqual(expected.IntProp, result.IntProp);
        }
    }
}