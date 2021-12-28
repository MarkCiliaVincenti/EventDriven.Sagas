﻿using EventDriven.Sagas.Abstractions;

namespace EventDriven.Sagas.Tests.Fakes;

public class FakeCommand : ISagaCommand<string, string>
{
    public string? Name { get; set; }

    public string ExpectedResult { get; set; } = null!;

    public string Payload { get; set; } = null!;
}