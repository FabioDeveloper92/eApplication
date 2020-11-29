using System;
using System.Linq;
using Application.Task.Queries;
using FluentAssertions;
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

        public TaskReadServiceTest()
        {
            var configBuilder = new ConfigBuilder();

            _sandbox = new Sandbox(configBuilder.BuildModule(), new Application.Ioc.Module(), new MockedDotnetCoreModuleTest());
        }

        [Fact]
        public async Tasks.Task get_all_board_return_two_board()
        {
            //ARRANGE
            var taskId1 = Guid.NewGuid();
            const string taskName1 = "board 1";
            const string taskDescription1 = "desc 1 desc 2";
            var userId1 = Guid.NewGuid();

            var taskId2 = Guid.NewGuid();
            const string taskName2 = "board2";
            const string taskDescription2 = "description2";
            var userId2 = Guid.NewGuid(); ;

            _sandbox.Scenario
                .WithTask(taskId1, taskName1, taskDescription1, userId1)
                .And()
                .WithTask(taskId2, taskName2, taskDescription2, userId2);

            //ACT
            var boards = await _sandbox.Mediator.Send(new GetTasks());

            //ASSERT
            var item1 = boards.Single(x => x.Id == taskId1);
            var item2 = boards.Single(x => x.Id == taskId2);

            item1.Id.Should().Be(taskId1);
            item1.Name.Should().Be(taskName1);
            item1.Description.Should().Be(taskDescription1);
            item1.UserId.Should().Be(userId1);

            item2.Id.Should().Be(taskId2);
            item2.Name.Should().Be(taskName2);
            item2.Description.Should().Be(taskDescription2);
            item2.UserId.Should().Be(userId2);
        }


        public void Dispose()
        {
        }
    }
}
