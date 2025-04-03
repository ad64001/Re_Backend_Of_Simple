﻿using Re_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Re_Backend.Tests
{
    public class GeneralServiceTests : IClassFixture<TestFixture>
    {
        private readonly ITestService testService;
        private readonly ITestDbService testDbService;
        private readonly ITestRedisCacheService testRedisCache;
        private readonly ITestOutputHelper testOutput;

        public GeneralServiceTests(TestFixture fixture, ITestOutputHelper testOutput)
        {
            this.testService = fixture.TestService;
            this.testDbService = fixture.TestDbService;
            this.testRedisCache = fixture.TestRedisCache;
            this.testOutput = testOutput;
        }

        [Fact]
        public void Test1()
        {
            testOutput.WriteLine(testService.DoSomething());
            testOutput.WriteLine(testDbService.DoSomething());
            Assert.NotNull(testService);
        }

        [Fact]
        public async Task TestRedisCache()
        {
            var result = await testRedisCache.UseCacheAsync();
            Assert.NotNull(result);
            testOutput.WriteLine(result);
        }
    }
}
