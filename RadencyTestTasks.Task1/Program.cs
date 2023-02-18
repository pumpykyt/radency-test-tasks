using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using RadencyTestTasks.Task1;
using RadencyTestTasks.Task1.Domain;
using RadencyTestTasks.Task1.Domain.Factories;
using RadencyTestTasks.Task1.Domain.Interfaces;
using RadencyTestTasks.Task1.Domain.Services;
using RadencyTestTasks.Task1.Global;
using RadencyTestTasks.Task1.Jobs;
using RadencyTestTasks.Task1.Presentation;
using RadencyTestTasks.Task1.Settings;

UserInterface.Menu();