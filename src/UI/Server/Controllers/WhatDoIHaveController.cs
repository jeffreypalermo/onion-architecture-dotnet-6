﻿using Lamar;
using Microsoft.AspNetCore.Mvc;

namespace UI.Server.Controllers;

public class WhatDoIHaveController : ControllerBase
{
    [HttpGet("/_lamar/services")]
    public string Services([FromServices] IContainer container)
    {
        return container.WhatDoIHave();
    }

    [HttpGet("/_lamar/scanning")]
    public string Scanning([FromServices] IContainer container)
    {
        return container.WhatDidIScan();
    }
}