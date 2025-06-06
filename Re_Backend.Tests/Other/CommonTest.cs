﻿using Re_Backend.Common;
using Xunit.Abstractions;

namespace Re_Backend.Tests.Other
{
    public class CommonTest
    {
        private readonly ITestOutputHelper testOutput;

        public CommonTest(ITestOutputHelper testOutput)
        {
            this.testOutput = testOutput;
        }

        [Fact]
        public void TestAES()
        {
            string password = "MySecretPassword";

            string encrypted = AESAlgorithm.EncryptString(password);
            string decrypted = AESAlgorithm.DecryptString(encrypted);

            testOutput.WriteLine($"原始密码: {password}");
            testOutput.WriteLine($"加密后的密码: {encrypted}");
            testOutput.WriteLine($"解密后的密码: {decrypted}");
        }
    }
}
