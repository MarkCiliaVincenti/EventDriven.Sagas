﻿namespace TestClient.Configuration;

public class SagaConfigServiceSettings
{
    public string ServiceUri { get; set; } = null!;
    public Guid SagaConfigId { get; set; }
}