using SOAP.WebService.Core.Entities;
using SOAP.WebService.Models.Requests;
using SOAP.WebService.Models.Responses;

namespace SOAP.WebService.Core.Mappers;

public static class FindingMapper
{
    public static IEnumerable<Finding> MapFindingRequestsToEntities(IEnumerable<FindingRequest> findingRequests, Guid scanResultId)
    {
        return findingRequests.Select(findingRequest => new Finding(findingRequest, scanResultId));
    }

    public static IEnumerable<FindingResponse> MapFindingEntitiesToResponses(IEnumerable<Finding> findings)
    {
        return findings.Select(MapEntityToResponse);
    }

    private static FindingResponse MapEntityToResponse(Finding finding)
    {
        return new FindingResponse
        {
            Title = finding.Title,
            Description = finding.Description,
            File = finding.File,
            LineStart = finding.LineStart,
            LineEnd = finding.LineEnd
        };
    }
}