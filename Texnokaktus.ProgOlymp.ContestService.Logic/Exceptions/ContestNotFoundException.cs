namespace Texnokaktus.ProgOlymp.ContestService.Logic.Exceptions;

public class ContestNotFoundException(int contestId) : Exception($"Contest with id {contestId} was not found");
