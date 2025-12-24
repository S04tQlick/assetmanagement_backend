using AssetManagement.API.Extentions.ProgramExtention; 

DotNetEnv  .Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.AddDependencyInjection();

builder.AppBuilderAddUsings();

public abstract partial class Program;