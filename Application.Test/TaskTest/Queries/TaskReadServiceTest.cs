using System;
using System.Linq;
using Application.Code;
using Application.Task.Queries;
using Autofac;
using FluentAssertions;
using NSubstitute;
using Test.Common.Builders;
using Test.Infrastructure.Common;
using Xunit;
using Tasks = System.Threading.Tasks;

namespace Application.Test.TaskTest.Queries
{
    [Trait("Type", "Integration")]
    [Trait("Category", "Database")]
    [Collection("DropCreateDatabase Collection")]
    public class TaskReadServiceTest : IDisposable
    {
        private readonly Sandbox _sandbox;
        private readonly IContextProvider _contextProvider;

        public TaskReadServiceTest()
        {
            var configBuilder = new ConfigBuilder();

            _contextProvider = Substitute.For<IContextProvider>();

            _sandbox = new Sandbox(configBuilder.BuildModule(), new Application.Ioc.Module(), new MockedDotnetCoreModuleTest(), new MockModule(_contextProvider));
        }

        [Fact]
        public async Tasks.Task get_all_task_return_two_task()
        {
            //ARRANGE
            var taskId1 = Guid.NewGuid();
            const string taskName1 = "task 1";
            const string taskDescription1 = "desc 1 desc 2";
            var userId1 = Guid.NewGuid();

            var taskId2 = Guid.NewGuid();
            const string taskName2 = "task2";
            const string taskDescription2 = "description2";
            var userId2 = Guid.NewGuid(); ;

            _sandbox.Scenario
                .WithTask(taskId1, taskName1, taskDescription1, userId1)
                .And()
                .WithTask(taskId2, taskName2, taskDescription2, userId2);

            //ACT
            var tasks = await _sandbox.Mediator.Send(new GetTasks());

            //ASSERT
            var item1 = tasks.Single(x => x.Id == taskId1);
            var item2 = tasks.Single(x => x.Id == taskId2);

            item1.Id.Should().Be(taskId1);
            item1.Name.Should().Be(taskName1);
            item1.Description.Should().Be(taskDescription1);
            item1.UserId.Should().Be(userId1);

            item2.Id.Should().Be(taskId2);
            item2.Name.Should().Be(taskName2);
            item2.Description.Should().Be(taskDescription2);
            item2.UserId.Should().Be(userId2);
        }


        [Fact]
        public async Tasks.Task get_task_with_id_return_one_task()
        {
            //ARRANGE
            var taskId1 = Guid.NewGuid();
            const string taskName1 = "task 1";
            const string taskDescription1 = "desc 1 desc 2";
            var userId1 = Guid.NewGuid();

            var taskId2 = Guid.NewGuid();
            const string taskName2 = "task2";
            const string taskDescription2 = "description2";
            var userId2 = Guid.NewGuid(); ;

            _sandbox.Scenario
                .WithTask(taskId1, taskName1, taskDescription1, userId1)
                .And()
                .WithTask(taskId2, taskName2, taskDescription2, userId2);

            //ACT
            var task = await _sandbox.Mediator.Send(new GetTask(taskId1));

            //ASSERT
            task.Id.Should().Be(taskId1);
            task.Name.Should().Be(taskName1);
            task.Description.Should().Be(taskDescription1);
            task.UserId.Should().Be(userId1);
        }

        [Fact]
        public async Tasks.Task get_task_with_id_not_exist_retrn_null()
        {
            //ARRANGE
            _sandbox.Scenario
                .WithTask()
                .And()
                .WithTask();

            //ACT
            var task = await _sandbox.Mediator.Send(new GetTask(Guid.NewGuid()));

            //ASSERT
            task.Should().BeNull();
        }

        public void Dispose()
        {
            _sandbox?.Dispose();
        }

        private class MockModule : Autofac.Module
        {
            private readonly IContextProvider _contextProvider;

            public MockModule(IContextProvider contextProvider)
            {
                _contextProvider = contextProvider;
            }

            protected override void Load(ContainerBuilder builder)
            {
                builder.Register(ctx => _contextProvider).As<IContextProvider>().SingleInstance();
            }
        }
    }
}
