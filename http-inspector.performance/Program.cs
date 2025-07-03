using BenchmarkDotNet.Running;

var summary = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();