﻿namespace XAuth.Application.DTOs;

public record UserDto(Guid Id, string Email, bool EmailConfirmed);