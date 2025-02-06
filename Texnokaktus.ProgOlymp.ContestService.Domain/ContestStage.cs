namespace Texnokaktus.ProgOlymp.ContestService.Domain;

public record ContestStage(long Id,
                           string Name,
                           DateTimeOffset ContestStart,
                           DateTimeOffset? ContestFinish,
                           TimeSpan Duration);
