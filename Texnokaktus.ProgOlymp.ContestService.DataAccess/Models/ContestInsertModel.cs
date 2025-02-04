namespace Texnokaktus.ProgOlymp.ContestService.DataAccess.Models;

public record ContestInsertModel(string Name, DateTimeOffset RegistrationStart, DateTimeOffset RegistrationFinish);
