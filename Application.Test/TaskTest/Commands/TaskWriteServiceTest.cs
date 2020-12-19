﻿using System;
using Application.Code;
using Autofac;
using Domain.Exceptions;
using NSubstitute;
using Test.Common.Builders;
using Test.Common.Builders.Commands;
using Test.Infrastructure.Common;
using Xunit;
using Tasks = System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.Write.Task;
using Application.Task.Commads;

namespace Application.Test.TaskTest.Commands
{
    [Trait("Type", "Integration")]
    [Trait("Category", "Database")]
    [Collection("DropCreateDatabase Collection")]
    public class TaskWriteServiceTest : IDisposable
    {
        private readonly Sandbox _sandbox;
        private readonly IContextProvider _contextProvider;

        public TaskWriteServiceTest()
        {
            var configBuilder = new ConfigBuilder();

            _contextProvider = Substitute.For<IContextProvider>();

            _sandbox = new Sandbox(configBuilder.BuildModule(), new Application.Ioc.Module(), new MockedDotnetCoreModuleTest(),
                new MockModule(_contextProvider));
        }

        [Fact]
        public async Tasks.Task create_task_should_create_a_new_task()
        {
            //ARRANGE
            var createTask = new CreateTaskBuilder().WithDefaults().Build();

            //ACT
            await _sandbox.Mediator.Send(createTask);
        }

        [Fact]
        public void create_task_with_name_is_void_should_exception()
        {
            //ARRANGE
            var createTask = new CreateTaskBuilder()
                .WithName("")
                .WithDescription("the name is void")
                .WithUserId(Guid.NewGuid())
                .Build();

            //ACT 
            Func<Tasks.Task> fn = async () => { await _sandbox.Mediator.Send(createTask); };

            //ASSERT
            fn.Should().Throw<EmptyFieldException>();
        }

        [Fact]
        public void create_task_with_description_is_void_should_exception()
        {
            //ARRANGE
            var createTask = new CreateTaskBuilder()
                .WithName("Name")
                .WithDescription("")
                .WithUserId(Guid.NewGuid())
                .Build();

            //ACT 
            Func<Tasks.Task> fn = async () => { await _sandbox.Mediator.Send(createTask); };

            //ASSERT
            fn.Should().Throw<EmptyFieldException>();
        }

        [Fact]
        public async void update_task_name()
        {
            //ARRANGE
            const string newTaskName = "new task name";
            var taskId = Guid.NewGuid();

            _sandbox.Scenario.WithTask(taskId, "old name", "desc", Guid.NewGuid());

            //ACT
            await _sandbox.Mediator.Send(new UpdateTaskName(taskId, newTaskName));

            //ASSERT
            var item = await _sandbox.Db.Get<TaskWriteDto>("tasks", taskId);

            item.Name.Should().Be(newTaskName);
        }

        [Fact]
        public async void update_task_description()
        {
            //ARRANGE
            const string newTaskDescription = "new task name";
            var taskId = Guid.NewGuid();

            _sandbox.Scenario.WithTask(taskId, "old name", "desc", Guid.NewGuid());

            //ACT
            await _sandbox.Mediator.Send(new UpdateTaskDescription(taskId, newTaskDescription));

            //ASSERT
            var item = await _sandbox.Db.Get<TaskWriteDto>("tasks", taskId);

            item.Description.Should().Be(newTaskDescription);
        }

        [Fact]
        public async void update_task_userId()
        {
            //ARRANGE
            var taskId = Guid.NewGuid();
            var newUserId = Guid.NewGuid();

            _sandbox.Scenario.WithTask(taskId, "old name", "desc", Guid.NewGuid());

            //ACT
            await _sandbox.Mediator.Send(new UpdateTaskUserId(taskId, newUserId));

            //ASSERT
            var item = await _sandbox.Db.Get<TaskWriteDto>("tasks", taskId);

            item.UserId.Should().Be(newUserId);
        }

        [Fact]
        public void update_task_name_with_void_should_excpetion()
        {
            //ARRANGE
            const string newTaskName = "";
            var taskId = Guid.NewGuid();

            _sandbox.Scenario.WithTask(taskId, "old name", "desc", Guid.NewGuid());

            //ACT 
            Func<Tasks.Task> fn = async () => { await _sandbox.Mediator.Send(new UpdateTaskName(taskId, newTaskName)); };

            //ASSERT
            fn.Should().Throw<EmptyFieldException>();
        }

        [Fact]
        public void update_task_description_with_void_should_excpetion()
        {
            //ARRANGE
            const string newTaskDescription = "";
            var taskId = Guid.NewGuid();

            _sandbox.Scenario.WithTask(taskId, "old name", "desc", Guid.NewGuid());

            //ACT 
            Func<Tasks.Task> fn = async () => { await _sandbox.Mediator.Send(new UpdateTaskDescription(taskId, newTaskDescription)); };

            //ASSERT
            fn.Should().Throw<EmptyFieldException>();
        }

        [Fact]
        public void update_task_not_exist_should_excpetion()
        {
            //ARRANGE
            _sandbox.Scenario.WithTask(Guid.NewGuid(), "old name", "desc", Guid.NewGuid());

            //ACT 
            Func<Tasks.Task> fn = async () => { await _sandbox.Mediator.Send(new UpdateTaskName(Guid.NewGuid(), "test")); };

            //ASSERT
            fn.Should().Throw<NotFoundItemException>();
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
